using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
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
            _output = Substitute.For<IOutput>();
            //_Output = new Output();
            _uut = new PowerTube(_output);
        }



        [TestCase(50)]
        public void Test_if_TurnOn_is_correct_input(int power)
        {
            //SetUp
            //Act
            _uut.TurnOn(power);
            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x => x.Contains($"PowerTube works with {power} Watt")));
        }


        [TestCase(50)]
        public void Test_if_TurnOf_works(int power)
        {
            //SetUp
            _uut.TurnOn(power);
            //Act
            _uut.TurnOff();
            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x => x.Contains("PowerTube turned off")));
        }
    }
}

