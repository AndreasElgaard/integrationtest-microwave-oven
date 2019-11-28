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
            _output = Substitute.For<IOutput>();
            _utt = new Light(_output);
        }

        [Test]
        public void Output_LightIsOn()
        {
            //Act
            _output.OutputLine("Light is turned on");

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Light is turned on")));
        }

        [Test]
        public void Output_LightIsOff()
        {
            //Act
            _utt.TurnOn();
            _utt.TurnOff();

            //Assert
            _output.Received().OutputLine(Arg.Is<string>(str => str.Contains("Light is turned off")));

        }
    }
}
