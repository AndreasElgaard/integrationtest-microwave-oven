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
            string output;

            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                _uut.StartCooking(power, time);
                
                output = sw.ToString();
            }

            //Assert
            Assert.That(output, Is.EqualTo($"PowerTube works with {power} %\r\n"));
            _Timer.Received(1).Start(time);
        }

        [Test]
        public void StartCooking_Outputs_TurnOnIsAlreadyOn()
        {
            //Act
            _uut.StartCooking(20, 20);

            Assert.Throws<InvalidOperationException>(() => _uut.StartCooking(20, 20));
        }


        [Test]
        [TestCase(1000, 30)]
        [TestCase(0, 30)]
        public void StartCooking_PowerThrowsError_AndCorrectRemainTime(int power, int time)
        {
            Assert.Throws<ArgumentException>(() => _uut.StartCooking(power,time));
        }

        [Test]
        public void StopCooking_OutpotsTurnedOff_TimerFalse()
        {
            //Act
            string output;
            

            using (StringWriter sw = new StringWriter())
            {
                _uut.StartCooking(10, 20);

                Console.SetOut(sw);

                _uut.Stop();

                output = sw.ToString();
            }

            //Assert
            Assert.That(output, Is.EqualTo("PowerTube turned off\r\n"));
            _Timer.Received(1).Stop();
        }

        [Test]
        public void OnTimerExpired_OutputPower_UICookingisdone()
        {
            //Act
            string output;
           

            using (StringWriter sw = new StringWriter())
            {
                _uut.StartCooking(10, 10);
                Console.SetOut(sw);

                _Timer.Expired += Raise.Event();

                output = sw.ToString();
            }

            //Assert
            Assert.That(output, Is.EqualTo("PowerTube turned off\r\n"));
            _Userinterface.Received(1).CookingIsDone();
        }

        [Test]
        public void OnTimerTick_DisplayTime()
        {
            //Act
            string output;
            
            using (StringWriter sw = new StringWriter())
            {
                _uut.StartCooking(20, 30);
                Console.SetOut(sw);

                _Timer.TimerTick += Raise.Event();

                output = sw.ToString();
            }

            var time = _Timer.TimeRemaining;

            Assert.That(output, Is.EqualTo($"Display shows: {time/60:D2}:{time&60:D2}\r\n"));

        }
    }
}
