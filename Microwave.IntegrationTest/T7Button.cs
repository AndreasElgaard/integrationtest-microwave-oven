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
    class T7Button
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
            //_Output = Substitute.For<IOutput>();
            _Output = new Output(); 

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
            string output;
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                _PowerButton.Press();
                output = stringWriter.ToString();
            }

            Assert.That(output, Is.EqualTo("Display shows: 50 W\r\n"));
        }

        [Test]
        public void TimeButtonPressed_outputs_shows_1Minute()
        {
            string output;
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                _PowerButton.Press();
                _TimeButton.Press();
                output = stringWriter.ToString();
            }

            Assert.That(output, Is.EqualTo("Display shows: 50 W\r\nDisplay shows: 01:00\r\n"));
        }

        [Test]
        public void StartCancelButtonPressed_outputs_shows_PowerTurnedOn()
        {
            string output;
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                _PowerButton.Press();
                _TimeButton.Press();
                _StartCancelButton.Press();
                output = stringWriter.ToString();
            }

            Assert.That(output, Is.EqualTo("Display shows: 50 W\r\nDisplay shows: 01:00\r\n" +
                                           "Display cleared\r\nLight is turned on\r\nPowerTube works with 50 Watt\r\n"));
        }
    }
}
