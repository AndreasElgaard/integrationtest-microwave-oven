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
    public class T4CookController
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
            _Output = Substitute.For<IOutput>();
            _Timer = Substitute.For<ITimer>();
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
            _uut.StartCooking(power, time);

            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"PowerTube works with {power} Watt")));
            _Timer.Received(1).Start(time);
        }

        [Test]
        public void StartCooking_Outputs_TurnOnIsAlreadyOn()
        {
            //Act
            _uut.StartCooking(70, 55);

            Assert.Throws<ApplicationException>(() => _uut.StartCooking(70, 55));
        }


        [Test]
        [TestCase(1000, 30)]
        [TestCase(0, 30)]
        public void StartCooking_PowerThrowsError_AndCorrectRemainTime(int power, int time)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _uut.StartCooking(power,time));
        }

        [Test]
        public void StopCooking_OutpotsTurnedOff_TimerFalse()
        {
            //Act
            _uut.StartCooking(80, 20);
            _uut.Stop();


            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(str => str.Contains("PowerTube turned off")));
            _Timer.Received(1).Stop();
        }

        [Test]
        public void OnTimerExpired_OutputPower_UICookingisdone()
        {
            //Act
            _uut.StartCooking(80, 20);
            _Timer.Expired += Raise.Event();


            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(str => str.Contains("PowerTube turned off")));
            _Userinterface.Received(1).CookingIsDone();
        }

        [Test]
        public void OnTimerTick_DisplayTime()
        {
            //Act
            _uut.StartCooking(80, 30);
            _Timer.TimerTick += Raise.Event();

            //Assert
            var time = _Timer.TimeRemaining;
            _Output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"Display shows: {time / 60:D2}:{time & 60:D2}")));
        }
    }
}
