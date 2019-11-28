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
    class T7Door
    {
        private IOutput _Output;

        private IDisplay _Display;
        private ILight _Light;
        private IPowerTube _PowerTube;
        private ITimer _Timer;
        //
        private IButton _PowerButton;
        private IButton _TimeButton;
        private IButton _StartCancelButton;
        private IDoor _uut;

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
            _uut = new Door();


            _CookController = new CookController(_Timer, _Display, _PowerTube);
            _UserInterface = new UserInterface(_PowerButton, _TimeButton, _StartCancelButton, _uut,
                _Display, _Light, _CookController);
        }
        
        [Test]
        public void DoorClosed_Opens_output_shows_LightIsturnedOn()
        {

            //SetUp
            _uut.Close();
            //Act
            _uut.Open();
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == "Light is turned on"));
        }

        [Test]
        public void DoorOpnend_Closes_output_shows_LightIsturnedOff()
        {
            //SetUp
            _uut.Open();
            //Act
            _uut.Close();
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == "Light is turned on"));
            _Output.Received().OutputLine(Arg.Is<string>(x => x == "Light is turned off"));
        }
    }
}
