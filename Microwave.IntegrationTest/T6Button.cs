using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.IntegrationTest
{
    class T6Button
    {
        private IOutput _Output;

        private IDisplay _Display;
        private ILight _Light;
        private IPowerTube _PowerTube;
        private ITimer _Timer;

        private IButton _PowerButton;
        private IButton _TimeButton;
        private IButton _StartCancelButton;
        private IDoor _Door;

        private CookController _CookController;
        private UserInterface _UserInterface;

        [SetUp]
        public void Setup()
        {
            _Output = Substitute.For<IOutput>();
            //_Output = new Output(); 

            _Display = new Display(_Output);
            _Light = new Light(_Output);
            _PowerTube = new PowerTube(_Output);
            _Timer = Substitute.For<ITimer>();

            _PowerButton = new Button();
            _TimeButton = new Button();
            _StartCancelButton = new Button();
            _Door = Substitute.For<IDoor>();


            _CookController = new CookController(_Timer, _Display, _PowerTube);
            _UserInterface = new UserInterface(_PowerButton, _TimeButton, _StartCancelButton, _Door,
                _Display, _Light, _CookController);
        }

        [Test]
        public void PowerButtonPressed_outputs_shows_50W()
        {
            //SetUp
            //Act
            _PowerButton.Press();
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == "Display shows: 50 W"));
        }

        [Test]
        public void TimeButtonPressed_outputs_shows_1Minute()
        {
            //SetUp
            _PowerButton.Press();
            //Act
            _TimeButton.Press();
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == "Display shows: 50 W"));
            _Output.Received().OutputLine(Arg.Is<string>(x => x == "Display shows: 01:00"));
        }

        [Test]
        public void StartCancelButtonPressed_outputs_shows_PowerTurnedOn()
        {
            //SetUp
            _PowerButton.Press();
            _TimeButton.Press();
            //Act
            _StartCancelButton.Press();
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == "Display shows: 50 W"));
            _Output.Received().OutputLine(Arg.Is<string>(x => x == "Display shows: 01:00"));
            _Output.Received().OutputLine(Arg.Is<string>(x => x == "Display cleared"));
            _Output.Received().OutputLine(Arg.Is<string>(x => x == "Light is turned on"));
            _Output.Received().OutputLine(Arg.Is<string>(x => x == "PowerTube works with 50 Watt"));
        }
    }
}
