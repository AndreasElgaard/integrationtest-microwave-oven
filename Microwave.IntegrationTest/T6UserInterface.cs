using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.IntegrationTest
{
    [TestFixture]
    class T6UserInterface
    {
        private IOutput _Output;

        private IDisplay _Display;
        private ILight _Light;
        private IPowerTube _PowerTube;
        private ITimer _Timer;

        private IButton _PowerButton;
        private IButton _TimeButton;
        private IButton _StartCancelButton;
        private IDoor _Door;

        private CookController _CookController;
        private UserInterface _uut;

        [SetUp]
        public void Setup()
        {
            _Output = Substitute.For<IOutput>();
            //_Output = new Output();

            _Display = new Display(_Output);
            _Light = new Light(_Output);
            _PowerTube = new PowerTube(_Output);
            _Timer = Substitute.For<ITimer>();

            _PowerButton = Substitute.For<IButton>();
            _TimeButton = Substitute.For<IButton>();
            _StartCancelButton = Substitute.For<IButton>();
            _Door = Substitute.For<IDoor>();


            //_CookController = Substitute.For<CookController>();
            _CookController = new CookController(_Timer, _Display, _PowerTube);
            _uut = new UserInterface(_PowerButton, _TimeButton, _StartCancelButton, _Door,
                _Display, _Light, _CookController);
        }

        #region OnPowerPressed

        [Test]
        public void OnPowerPressed_1Time_Output_shows_50W()
        {
            //SetUp
            _Door.Closed += Raise.Event();
            //Act
            _PowerButton.Pressed += Raise.Event();
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == "Display shows: 50 W"));
        }

        //[Test]
        //public void OnPowerPressed_1Time_Output_shows_50W()
        //{
        //    string output;
        //    //SetUp
        //    _Door.Closed += Raise.Event();
        //    using (StringWriter stringWriter = new StringWriter())
        //    {
        //        Console.SetOut(stringWriter);
        //        //Act
        //        _PowerButton.Pressed += Raise.Event();
        //        output = stringWriter.ToString();
        //    }

        //    Assert.That(output, Is.EqualTo($"Display shows: 50 W\r\n"));
        //}

        [Test]
        public void OnPowerPressed_15Times_Output_shows_50W()
        {
            //SetUp
            _Door.Closed += Raise.Event();
            //Act
            for (int i = 0; i < 15; i++)
            {
                _PowerButton.Pressed += Raise.Event();
            }

            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == "Display shows: 50 W"));
        }

        //[Test]
        //public void OnPowerPressed_15Times_Output_shows_50W()
        //{
        //    string output;
        //    using (StringWriter stringWriter = new StringWriter())
        //    {
        //        //SetUp
        //        _Door.Closed += Raise.Event();
        //        for (int i = 0; i < 14; i++)
        //        {
        //            _PowerButton.Pressed += Raise.Event();
        //        }
        //        Console.SetOut(stringWriter);
        //        //Act
        //        _PowerButton.Pressed += Raise.Event();
        //        output = stringWriter.ToString();
        //    }

        //    Assert.That(output, Is.EqualTo($"Display shows: 50 W\r\n"));
        //}

        [Test]
        public void OnPowerPressed_Times_Output_shows_700W()
        {
            //SetUp
            _Door.Closed += Raise.Event();
            //Act
            for (int i = 0; i < 14; i++)
            {
                _PowerButton.Pressed += Raise.Event();
            }

            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == "Display shows: 700 W"));
        }

        //[Test]
        //public void OnPowerPressed_Times_Output_shows_700W()
        //{
        //    string output;
        //    using (StringWriter stringWriter = new StringWriter())
        //    {
        //        //SetUp
        //        _Door.Closed += Raise.Event();
        //        for (int i = 0; i < 13; i++)
        //        {
        //            _PowerButton.Pressed += Raise.Event();
        //        }
        //        Console.SetOut(stringWriter);
        //        //Act
        //        _PowerButton.Pressed += Raise.Event();
        //        output = stringWriter.ToString();
        //    }

        //    Assert.That(output, Is.EqualTo($"Display shows: 700 W\r\n"));
        //}

        #endregion

        #region OnTimePressed

        [Test]
        public void OnTimePressed_1Time_Output_shows_1minute()
        {
            //Setup
            _Door.Closed += Raise.Event();
            _PowerButton.Pressed += Raise.Event();
            //Act
            _TimeButton.Pressed += Raise.Event();
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"Display shows: 01:00"));
        }

        //[Test]
        //public void OnTimePressed_1Time_Output_shows_1minute()
        //{
        //    string output;
        //    using (StringWriter stringWriter = new StringWriter())
        //    {
        //        //Setup
        //        _Door.Closed += Raise.Event();
        //        _PowerButton.Pressed += Raise.Event();
        //        Console.SetOut(stringWriter);
        //        //Act
        //        _TimeButton.Pressed += Raise.Event();
        //        output = stringWriter.ToString();
        //    }

        //    Assert.That(output, Is.EqualTo($"Display shows: 01:00\r\n"));
        //}

        [Test]
        public void OnTimePressed_200Times_Output_shows_100minutes()
        {
            //Setup
            _Door.Closed += Raise.Event();
            _PowerButton.Pressed += Raise.Event();
            //Act
            for (int i = 0; i < 100; i++)
            {
                _TimeButton.Pressed += Raise.Event();
            }

            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"Display shows: 100:00"));
        }

        //[Test]
        //public void OnTimePressed_200Times_Output_shows_100minutes()
        //{
        //    string output;
        //    using (StringWriter stringWriter = new StringWriter())
        //    {
        //        //Setup
        //        _Door.Closed += Raise.Event();
        //        _PowerButton.Pressed += Raise.Event();
        //        for (int i = 0; i < 100; i++)
        //        {
        //            _TimeButton.Pressed += Raise.EventWith(EventArgs.Empty);
        //        }
        //        Console.SetOut(stringWriter);
        //        //Act
        //        _TimeButton.Pressed += Raise.Event();
        //        output = stringWriter.ToString();
        //    }

        //    Assert.That(output, Is.EqualTo($"Display shows: 101:00\r\n"));
        //}

        #endregion

        #region OnStartCanselPressed

        [Test]
        public void OnStarCancelPressed_1Time_Output_shows_50()
        {
            //Setup
            _Door.Closed += Raise.Event();
            _PowerButton.Pressed += Raise.Event();
            _TimeButton.Pressed += Raise.Event();
            //Act
            _StartCancelButton.Pressed += Raise.Event();
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"PowerTube works with 50 %"));
        }

        //[Test]
        //public void OnStarCancelPressed_1Time_Output_shows_50()
        //{
        //    string output;
        //    using (StringWriter stringWriter = new StringWriter())
        //    {
        //        //Setup
        //        _Door.Closed += Raise.Event();
        //        _PowerButton.Pressed += Raise.Event();
        //        _TimeButton.Pressed += Raise.Event();
        //        Console.SetOut(stringWriter);
        //        //Act
        //        _StartCancelButton.Pressed += Raise.Event();
        //        output = stringWriter.ToString();
        //    }

        //    Assert.That(output, Is.EqualTo($"Display cleared\r\nLight is turned on\r\nPowerTube works with 50 %\r\n"));
        //}

        [Test]
        public void OnStarCancelPressed_2Times_Output_shows_PowerTubeTurnedOff()
        {
            //Setup
            _Door.Closed += Raise.Event();
            _PowerButton.Pressed += Raise.Event();
            _TimeButton.Pressed += Raise.Event();
            //Act
            _StartCancelButton.Pressed += Raise.Event();
            _StartCancelButton.Pressed += Raise.Event();
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"PowerTube turned off"));
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"Light is turned off"));
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"Display cleared"));
        }

        //[Test]
        //public void OnStarCancelPressed_2Times_Output_shows_PowerTubeTurnedOff()
        //{
        //    string output;
        //    using (StringWriter stringWriter = new StringWriter())
        //    {
        //        //Setup
        //        _Door.Closed += Raise.Event();
        //        _PowerButton.Pressed += Raise.Event();
        //        _TimeButton.Pressed += Raise.Event();
        //        _StartCancelButton.Pressed += Raise.Event();
        //        Console.SetOut(stringWriter);
        //        //Act
        //        _StartCancelButton.Pressed += Raise.Event();
        //        output = stringWriter.ToString();
        //    }

        //    Assert.That(output, Is.EqualTo($"PowerTube turned off\r\nLight is turned off\r\nDisplay cleared\r\n"));
        //}

        #endregion

        #region OnDoorOpened

        [Test]
        public void OnDoorOpened_1Time_Output_shows_PowerTubeTurnedOFf()
        {
            //Setup
            _Door.Closed += Raise.Event();
            //Act
            _Door.Opened += Raise.Event();
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"Light is turned on"));
        }
        //[Test]
        //public void OnDoorOpened_1Time_Output_shows_PowerTubeTurnedOFf()
        //{
        //    string output;
        //    using (StringWriter stringWriter = new StringWriter())
        //    {
        //        //Setup
        //        _Door.Closed += Raise.Event();
        //        Console.SetOut(stringWriter);
        //        //Act
        //        _Door.Opened += Raise.Event();
        //        output = stringWriter.ToString();
        //    }

        //    Assert.That(output, Is.EqualTo($"Light is turned on\r\n"));
        //}

        #endregion

        #region OnDoorClosed

        [Test]
        public void OnDoorOpened_1Time_Output_shows_50()
        {
            //Setup
            _Door.Opened += Raise.Event();
            //Act
            _Door.Closed += Raise.Event();
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"Light is turned on"));
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"Light is turned off"));
        }

        //[Test]
        //public void OnDoorOpened_1Time_output_shows_LightIsTurnedOff()
        //{
        //    string output;
        //    //Setup
        //    _Door.Opened += Raise.Event();
        //    using (StringWriter stringWriter = new StringWriter())
        //    {
        //        Console.SetOut(stringWriter);
        //        //Act
        //        _Door.Closed += Raise.Event();
        //        output = stringWriter.ToString();
        //    }

        //    Assert.That(output, Is.EqualTo($"Light is turned off\r\n"));
        //}
        #endregion

        #region OnCookingIsDone
        [Test]
        public void OnCookingIsDone_output_show_()
        {
            //Setup
            _Door.Closed += Raise.Event();
            _PowerButton.Pressed += Raise.Event();
            _TimeButton.Pressed += Raise.Event();
            _StartCancelButton.Pressed += Raise.Event();
            //Act
            _Timer.Expired += Raise.Event();
            //_uut.Received(1).CookingIsDone();
            //Assert
            _Output.Received().OutputLine(Arg.Is<string>(x => x == $"PowerTube turned off"));
        }

        //[Test]
        //public void OnCookingIsDone_output_show_()
        //{
        //    string output;
        //    using (StringWriter stringWriter = new StringWriter())
        //    {
        //        //Setup
        //        _Door.Closed += Raise.Event();
        //        _PowerButton.Pressed += Raise.Event();
        //        _TimeButton.Pressed += Raise.Event();
        //        _StartCancelButton.Pressed += Raise.Event();
        //        Console.SetOut(stringWriter);
        //        //Act
        //        _Timer.Expired += Raise.Event();
        //        output = stringWriter.ToString();
        //    }

        //    Assert.That(output, Is.EqualTo($"PowerTube turned off\r\n"));
        //}
        #endregion
    }
}
