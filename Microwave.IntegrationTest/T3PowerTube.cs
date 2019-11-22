using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using MicrowaveOvenClasses.Interfaces;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using System.IO;


namespace Microwave.IntegrationTest
{
    [TestFixture]
    public class T3PowerTube
    {
        private IOutput _Output;
        private PowerTube _uut;

        [SetUp]
        public void Setup()
        {
            _Output = new Output();
            _uut = new PowerTube(_Output);
        }

        [Test]
        [TestCase(10)]
        [TestCase(80)]
        public void TurnON_Shows_CorrectPower(int power)
        {

            //Act
            string Output;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                _uut.TurnOn(power);

                Output = sw.ToString();
            }

            //Assert
            Assert.That(Output, Is.EqualTo($"PowerTube works with {power} %\r\n"));
        }

        [Test]
        public void TurnOff_showsCorrect_Output()
        {
            //Act
            _uut.TurnOn(50);
            string Output;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                _uut.TurnOff();

                Output = sw.ToString();
            }

            //Assert
            Assert.That(Output, Is.EqualTo($"PowerTube turned off\r\n"));
        }
    }
}
