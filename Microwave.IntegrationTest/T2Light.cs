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
            _output = new Output();
            _utt = new Light(_output);
        }


    }
}