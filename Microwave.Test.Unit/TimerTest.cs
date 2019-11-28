using System.Threading;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class TimerTest
    {
        private Timer uut;

        [SetUp]
        public void Setup()
        {
            uut = new Timer();
        }

        //Tid ændret i denne test
        [Test]
        public void Start_TimerTick_ShortEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.TimerTick += (sender, args) => pause.Set();
            uut.Start(2);

            // wait for a tick, but no longer
            Assert.That(pause.WaitOne(2000));

            uut.Stop();
        }

        //Tid ændret i test
        [Test]
        public void Start_TimerTick_LongEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.TimerTick += (sender, args) => pause.Set();
            uut.Start(20);

            // wait shorter than a tick, shouldn't come
            Assert.That(!pause.WaitOne(800));

            uut.Stop();
        }

        //Tid ændret i test
        [Test]
        public void Start_TimerExpires_ShortEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.Expired += (sender, args) => pause.Set();
            uut.Start(2);

            // wait for expiration, but not much longer, should come
            Assert.That(pause.WaitOne(2100));

            uut.Stop();
        }

        //Tid ændret i test
        [Test]
        public void Start_TimerExpires_LongEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.Expired += (sender, args) => pause.Set();
            uut.Start(5);

            // wait shorter than expiration, shouldn't come
            Assert.That(!pause.WaitOne(4800));

            uut.Stop();
        }

        //Starttid ændret
        [Test]
        public void Start_TimerTick_CorrectNumber()
        {
            ManualResetEvent pause = new ManualResetEvent(false);
            int notifications = 0;

            uut.Expired += (sender, args) => pause.Set();
            uut.TimerTick += (sender, args) => notifications++;

            uut.Start(2);

            // wait longer than expiration
            Assert.That(pause.WaitOne(2200));
            uut.Stop();

            Assert.That(notifications, Is.EqualTo(2));
        }

    }
}