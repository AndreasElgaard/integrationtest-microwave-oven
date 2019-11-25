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
    [TestFixture]
    class T6UserInterface
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
        private UserInterface _uut;

        [SetUp]
        public void Setup()
        {
            _Output = Substitute.For<IOutput>();
            //_Output = new Output();

            _Display = new Display(_Output);
            _Light = new Light(_Output);
            _PowerTube = new PowerTube(_Output);
            _Timer = Substitute.For<ITimer>();

            _PowerButton = Substitute.For<IButton>();
            _TimeButton = Substitute.For<IButton>();
            _StartCancelButton = Substitute.For<IButton>();
            _Door = Substitute.For<IDoor>();


            //_CookController = Substitute.For<CookController>();
            _CookController = new CookController(_Timer, _Display, _PowerTube);
            _uut = new UserInterface(_PowerButton, _TimeButton, _StartCancelButton, _Door,
                _Display, _Light, _CookController);
        }

        #region OnPowerPressed

        [Test]
        public void OnPowerPressed_1Time_Output_shows_50W()
        {
            //SetUp
            _Door.Closed += Raise.Event();
            //Act
            _PowerButton.Pressed += Raise.Event();
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == "Display shows: 50 W"));
        }

        [Test]
        public void OnPowerPressed_15Times_Output_shows_50W()
        {
            //SetUp
            _Door.Closed += Raise.Event();
            //Act
            for (int i = 0; i < 15; i++)
            {
                _PowerButton.Pressed += Raise.Event();
            }

            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == "Display shows: 50 W"));
        }

        [Test]
        public void OnPowerPressed_Times_Output_shows_700W()
        {
            //SetUp
            _Door.Closed += Raise.Event();
            //Act
            for (int i = 0; i < 14; i++)
            {
                _PowerButton.Pressed += Raise.Event();
            }

            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == "Display shows: 700 W"));
        }

        #endregion

        #region OnTimePressed

        [Test]
        public void OnTimePressed_1Time_Output_shows_1minute()
        {
            //Setup
            _Door.Closed += Raise.Event();
            _PowerButton.Pressed += Raise.Event();
            //Act
            _TimeButton.Pressed += Raise.Event();
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"Display shows: 01:00"));
        }

        [Test]
        public void OnTimePressed_200Times_Output_shows_100minutes()
        {
            //Setup
            _Door.Closed += Raise.Event();
            _PowerButton.Pressed += Raise.Event();
            //Act
            for (int i = 0; i < 100; i++)
            {
                _TimeButton.Pressed += Raise.EventWith(EventArgs.Empty);
            }

            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"Display shows: 100:00"));
        }

        #endregion

        #region OnStartCanselPressed

        [Test]
        public void OnStarCancelPressed_1Time_Output_shows_50()
        {
            //Setup
            _Door.Closed += Raise.Event();
            _PowerButton.Pressed += Raise.Event();
            _TimeButton.Pressed += Raise.Event();
            //Act
            _StartCancelButton.Pressed += Raise.Event();
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"PowerTube works with 50 %"));
        }

        [Test]
        public void OnStarCancelPressed_2Times_Output_shows_PowerTubeTurnedOff()
        {
            //Setup
            _Door.Closed += Raise.Event();
            _PowerButton.Pressed += Raise.Event();
            _TimeButton.Pressed += Raise.Event();
            //Act
            _StartCancelButton.Pressed += Raise.Event();
            _StartCancelButton.Pressed += Raise.Event();
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"PowerTube turned off"));
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"Light is turned off"));
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"Display cleared"));
        }

        #endregion

        #region OnDoorOpened

        [Test]
        public void OnDoorOpened_1Time_Output_shows_PowerTubeTurnedOFf()
        {
            //Setup
            _Door.Closed += Raise.Event();
            //Act
            _Door.Opened += Raise.Event();
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"Light is turned on"));
        }

        #endregion

        #region OnDoorClosed

        [Test]
        public void OnDoorOpened_1Time_Output_shows_50()
        {
            //Setup
            _Door.Opened += Raise.Event();
            //Act
            _Door.Closed += Raise.Event();
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"Light is turned on"));
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"Light is turned off"));
        }

        #endregion

        #region OnCookingIsDone
        [Test]
        public void OnCookingIsDone_output_show_()
        {
            //Setup
            _Door.Closed += Raise.Event();
            _PowerButton.Pressed += Raise.Event();
            _TimeButton.Pressed += Raise.Event();
            _StartCancelButton.Pressed += Raise.Event();
            //Act
            _Timer.Expired += Raise.Event();
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"PowerTube turned off"));
        }
        #endregion
    }
}
