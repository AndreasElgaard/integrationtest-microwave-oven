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
using System.Security.Cryptography.X509Certificates;

namespace Microwave.IntegrationTest
{
    [TestFixture]
    public class T5CookController
    {

        private ITimer _Timer;
        private IDisplay _Display;
        private IPowerTube _PowerTube;
        private IUserInterface _Userinterface;
        private IOutput _Output;
        private CookController _uut;


        [SetUp]
        public void Setup()
        {
            _Output = new Output();
            _Timer = new Timer();
            _Display = new Display(_Output);
            _PowerTube = new PowerTube(_Output);
            _Userinterface = Substitute.For<IUserInterface>();
            _uut = new CookController(_Timer, _Display, _PowerTube, _Userinterface);
        }

        [Test]
        [TestCase(80,55)]
        public void StartCooking_OutputsPower_AndCorrectRemainTime(int power, int time)
        {
            //Act
            string Output;
            _Output.OutputLine($"PowerTube works with {power} %");

            //Assert

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                _uut.StartCooking(power, time);

                Output = sw.ToString();
            }

            Assert.That(Output, Is.EqualTo($"PowerTube works with {power} %\r\n"));
            Assert.That(_Timer.TimeRemaining, Is.EqualTo(time));
        }

        [Test]
        public void StartCooking_PowerThrowsError_AndCorrectRemainTime()
        {
            //Act
            _uut.StartCooking(1000,20);


            //var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _PowerTube.TurnOn(power));
            //Assert.That(ex.Message, Is.EqualTo($"power, {power}, Must be between 1 and 100 % (incl.)"));

            //Assert.That(() => _PowerTube.TurnOn(power), Throws.Exception
            //    .TypeOf<ArgumentOutOfRangeException>()
            //    .With.Property("power").EqualTo("1000"));


            Assert.Throws<ArgumentOutOfRangeException>(() => _PowerTube.TurnOn(1000));
            //Assert.That(_Timer.TimeRemaining, Is.EqualTo(time));
        }
    }
}
