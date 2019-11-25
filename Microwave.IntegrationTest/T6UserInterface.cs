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
        private IDisplay _Display;
        private ILight _Light;

        private ICookController _CookController;
        private IPowerTube _PowerTube;
        private IOutput _Output;
        private ITimer _Timer;

        private UserInterface _uut;

        private IButton _PowerButton;
        private IButton _TimeButton;
        private IButton _StartCancelButton;

        private IDoor _Door;

        [SetUp]
        public void setup()
        {
            _Display = new Display(_Output);
            _Light = new Light(_Output);

            _CookController = Substitute.For<ICookController>();
            _PowerTube = new PowerTube(_Output);
            _Output = new Output();
            _Timer = Substitute.For<ITimer>(); 

            _uut = new UserInterface(_PowerButton, _TimeButton, _StartCancelButton, _Door, 
                _Display, _Light, _CookController);

            _PowerButton = Substitute.For<IButton>();
            _TimeButton = Substitute.For<IButton>();
            _StartCancelButton = Substitute.For<IButton>();

            _Door = Substitute.For<IDoor>();
        }
        //[Test]
        //public void Test_OnPower_Pressed_states()
        //{
        //    //Act
        //    string output;
        //    _uut.OnPowerPressed();

        //    using (var StringWriter = new StringWriter())
        //    {
        //        Console.SetOut(StringWriter);
        //        _Timer.Expired += Raise.Event();
        //        output = StringWriter.ToString();
        //    }

        //    //Assert
        //    Assert.That(output, Is.EqualTo("PowerTube turned off\r\n"));
        //    _CookController.Received(1).StartCooking(10,10);
        //}

        [Test]
        public void Test_On_door_closed_check_Event()
        {
            //Act
            _Door.Opened += Raise.EventWith(EventArgs.Empty);

            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x =>
                x == "Light is turned on"));
        }
    }

}
