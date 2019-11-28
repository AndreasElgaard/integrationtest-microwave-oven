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
        private IOutput _Output;
        private Display _uut;

        [SetUp]
        public void Setup()
        {
            _Output = Substitute.For<IOutput>();
            _uut = new Display(_Output);
        }

        [TestCase(59, 59)]
        //[Test]
        public void Test_ShowTimeIsWhatExpected(int min, int sec)
        {
            //SetUp
            //Act
            _uut.ShowTime(min, sec);
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"Display shows: {min}:{sec}"));
        }

        [TestCase(2)]
        //[Test]
        public void Test_ShowPower_Is_what_expected(int power)
        {
            //SetUp
            //Act
            _uut.ShowPower(power);
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"Display shows: {power} W"));
        }

        [Test]
        public void Test_Clear_Is_what_expected()
        {

            //SetUp
            //Act
            _uut.Clear();
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"Display cleared"));
        }
    }
}