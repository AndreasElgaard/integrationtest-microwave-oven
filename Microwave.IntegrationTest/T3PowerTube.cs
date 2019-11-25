using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;

namespace Microwave.IntegrationTest
{
    class T3PowerTube
    {
        private IOutput _output;
        private PowerTube _uut;

        [SetUp]
        public void Setup()
        {
            _output = new Output();
            _uut = new PowerTube(_output);
        }

        [TestCase(5)]
        //[Test]
        public void Test_Test_turnOn(int power)
        {
            string output;
            _uut.TurnOn(power);
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                _uut.TurnOn(power);
                output = stringWriter.ToString();
            }

            Assert.That(output, Is.EqualTo($"PowerTube.TurnOn: is already on\r\n"));

        }
    }
}
