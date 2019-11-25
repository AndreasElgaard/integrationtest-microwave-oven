using System;
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
    public class T1Display
    {
        private IOutput _output;
        private Display _uut;

        [SetUp]
        public void Setup()
        {
            _output = new Output();
            //_output = Substitute.For<IOutput>();
            _uut = new Display(_output);
        }

        [TestCase(59, 59)]
        //[Test]
        public void Test_ShowTimeIsWhatExpected(int min, int sec)
        {
            string output;
            //int min = 59;
            //int sec = 59; 
            //_output.OutputLine($"Display shows: {min:D2}:{sec:D2}");

            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                _uut.ShowTime(min, sec);
                output = stringWriter.ToString();
            }

            Assert.That(output, Is.EqualTo($"Display shows: {min}:{sec}\r\n"));

            //_uut.ShowTime(min, sec);
            ////Assert
            //_output.Received().OutputLine(Arg.Is<string>(x =>
            //    x == $"Display shows: {min}:{sec}"));
        }

        [TestCase(2)]
        //[Test]
        public void Test_ShowPower_Is_what_expected(int power)
        {
            string output;
            //int power = 2; 
            //_output.OutputLine($"Display shows: {power} W");

            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                _uut.ShowPower(power);
                output = stringWriter.ToString();
            }

            Assert.That(output, Is.EqualTo($"Display shows: {power} W\r\n"));
        }

        [Test]
        public void Test_Clear_Is_what_expected()
        {
            string output;
            //_output.OutputLine($"Display cleared");

            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                _uut.Clear();
                output = stringWriter.ToString();
            }

            Assert.That(output, Is.EqualTo($"Display cleared\r\n"));
        }
    }
}