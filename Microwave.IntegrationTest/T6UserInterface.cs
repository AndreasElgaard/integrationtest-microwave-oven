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
        private Display _Display;
        private Light _Light;

        private CookController _CookController;
        private PowerTube _PowerTube;
        private IOutput _Output;
        private ITimer _Timer;

        private UserInterface _uut;

        private IButton _PowerButton;
        private IButton _TimeButton;
        private IButton _StartCancelButton;

        private IDoor _Door;

        [SetUp]
        public void Setup()
        {
            _Display = new Display(_Output);
            _Light = new Light(_Output);

            //_CookController = Substitute.For<ICookController>();
            _PowerTube = new PowerTube(_Output);
            _Output = Substitute.For<IOutput>();
            _Timer = Substitute.For<ITimer>(); 


            _PowerButton = Substitute.For<IButton>();
            _TimeButton = Substitute.For<IButton>();
            _StartCancelButton = Substitute.For<IButton>();

            _Door = Substitute.For<IDoor>();

            _CookController = new CookController(_Timer, _Display, _PowerTube);
            _uut = new UserInterface(_PowerButton, _TimeButton, _StartCancelButton, _Door,
                _Display, _Light, _CookController);
        }
        [Test]
        public void Test_OnPower_Pressed_states()
        {
            //Act
            string output;
            _uut.OnPowerPressed(_PowerButton, EventArgs.Empty);

            using (var StringWriter = new StringWriter())
            {
                Console.SetOut(StringWriter);
                _Door.Opened += Raise.Event();
                output = StringWriter.ToString();
            }

            //Assert
            Assert.That(output, Is.EqualTo("PowerTube turned off\r\n"));
            _CookController.Received(1).StartCooking(10, 10);
        }

        //[Test]
        //public void Test_On_door_closed_check_Event()
        //{
        //    //Act
        //    _Door.Opened += Raise.EventWith(EventArgs.Empty);

        //    //Assert
        //    _Output.Received().OutputLine(Arg.Is<string>(x =>
        //        x == "Light is turned on"));
        //}
        [Test]
        public void CookingIsDone_test_Output()
        {
            //Setup
            //The following sequence of events changes the state of UserInterface to COOKING
            _PowerButton.Pressed += Raise.EventWith(EventArgs.Empty);
            _TimeButton.Pressed += Raise.EventWith(EventArgs.Empty);
            _StartCancelButton.Pressed += Raise.EventWith(EventArgs.Empty);

            //Act
            _uut.CookingIsDone();

            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x =>
                x == "Display cleared"));
        }

        [Test]
        public void CookController_OnDoorOpenedEvent_OutputShowsTurnedOff_OK()
        {
            //Setup
            //The following sequence of events changes the state of UserInterface to COOKING
            _PowerButton.Pressed += Raise.EventWith(EventArgs.Empty);
            _TimeButton.Pressed += Raise.EventWith(EventArgs.Empty);
            _StartCancelButton.Pressed += Raise.EventWith(EventArgs.Empty);

            //Act
            _Door.Opened += Raise.EventWith(EventArgs.Empty);

            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x =>
                x == "PowerTube turned off"));
        }
    }

}
