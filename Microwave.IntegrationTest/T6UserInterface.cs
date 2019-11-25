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
        private Display _Display;
        private Light _Light;

        private CookController _CookController;
        private PowerTube _PowerTube;
        private IOutput _Output;
        private ITimer _Timer;

        private UserInterface _uut;

        private IButton _PowerButton;
        private IButton _TimeButton;
        private IButton _StartCancelButton;

        private IDoor _Door;

        [SetUp]
        public void Setup()
        {
            _Output = Substitute.For<IOutput>();
            _Display = new Display(_Output);
            _Light = new Light(_Output);

            //_CookController = Substitute.For<ICookController>();
            _PowerTube = new PowerTube(_Output);
            _Timer = Substitute.For<ITimer>(); 


            _PowerButton = Substitute.For<IButton>();
            _TimeButton = Substitute.For<IButton>();
            _StartCancelButton = Substitute.For<IButton>();

            _Door = Substitute.For<IDoor>();

            _CookController = new CookController(_Timer, _Display, _PowerTube);
            _uut = new UserInterface(_PowerButton, _TimeButton, _StartCancelButton, _Door,
                _Display, _Light, _CookController);
        }
    }

}
