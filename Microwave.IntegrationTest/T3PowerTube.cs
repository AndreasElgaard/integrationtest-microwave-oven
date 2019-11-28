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

        [TestCase(10)]
        public void Test_if_TunOn_is_correct_input(int power)
        {
            string output;
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                _uut.TurnOn(power);
                output = stringWriter.ToString();
            }

            Assert.That(output, Is.EqualTo("PowerTube works with 10 %\r\n"));
        }

        [TestCase(101)]
        [TestCase(0)]
        [TestCase(10101)]
        public void Test_if_TunOn_is_Not_correct_input(int power)
        {
            Assert.That(() => _uut.TurnOn(power), Throws.ArgumentException);
        }

        [TestCase(1)]
        [TestCase(99)]
        [TestCase(50)]
        public void Test_if_TunOn_is_already_on(int power)
        {
            _uut.TurnOn(power);
            Assert.That(() => _uut.TurnOn(power), Throws.InvalidOperationException);
        }

        [TestCase(10)]
        public void Test_if_TurnOf_works(int power)
        {
            string output;
            _uut.TurnOn(power);
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                _uut.TurnOff();
                output = stringWriter.ToString();
            }

            Assert.That(output, Is.EqualTo("PowerTube turned off\r\n"));
        }
    }
}
