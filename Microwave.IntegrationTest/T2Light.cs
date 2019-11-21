using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;

namespace IntegrationTest
{
    [TestFixture]
    public class T2Ligh
    {
        private IOutput _output;
        private Light _utt;

        [SetUp]
        public void Setup()
        {
            _output = new Output();
            _utt = new Light(_output);
        }

        [Test]
        public void Output_LightIsOn()
        {
            //Act
            string Output;
            _output.OutputLine("Light is turned on");

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                _utt.TurnOn();

                Output = sw.ToString();
            }

            //Assert
            Assert.That(Output, Is.EqualTo("Light is turned on\r\n"));
        }

        [Test]
        public void Output_LightIsOff()
        {
            //Act
            _utt.TurnOn();
            string Output;
            _output.OutputLine("Light is turned off");

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                _utt.TurnOff();

                Output = sw.ToString();
            }

            //Assert
            Assert.That(Output, Is.EqualTo("Light is turned off\r\n"));

        }
    }
}
