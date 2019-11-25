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
    class T8Door
    {
        private IOutput _Output;

        private IDisplay _Display;
        private ILight _Light;
        private IPowerTube _PowerTube;
        private ITimer _Timer;

        private IButton _PowerButton;
        private IButton _TimeButton;
        private IButton _StartCancelButton;
        private IDoor _uut;

        private CookController _CookController;
        private UserInterface _UserInterface;

        [SetUp]
        public void Setup()
        {
            //_Output = Substitute.For<IOutput>();
            _Output = new Output();

            _Display = new Display(_Output);
            _Light = new Light(_Output);
            _PowerTube = new PowerTube(_Output);
            _Timer = Substitute.For<ITimer>();

            _PowerButton = Substitute.For<IButton>();
            _TimeButton = Substitute.For<IButton>();
            _StartCancelButton = Substitute.For<IButton>();
            _uut = new Door();


            _CookController = new CookController(_Timer, _Display, _PowerTube);
            _UserInterface = new UserInterface(_PowerButton, _TimeButton, _StartCancelButton, _uut,
                _Display, _Light, _CookController);
        }
        
        [Test]
        public void DoorClosed_Opens_output_shows_LightIsturnedOn()
        {
            string output;
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                _uut.Close();
                _uut.Open();
                output = stringWriter.ToString();
            }

            Assert.That(output, Is.EqualTo($"Light is turned on\r\n"));
        }

        [Test]
        public void DoorOpnend_Closes_output_shows_LightIsturnedOff()
        {
            string output;
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                _uut.Open();
                _uut.Close();
                output = stringWriter.ToString();
            }

            Assert.That(output, Is.EqualTo($"Light is turned on\r\nLight is turned off\r\n"));
        }
    }
}
