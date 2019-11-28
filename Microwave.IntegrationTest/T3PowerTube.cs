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

        //[TestCase(10)]
        ////[Test]
        //public void Test_if_TunOn_is_correct_input(int power)
        //{
        //    string output;
        //    //_uut.TurnOn(power);
        //    using (StringWriter stringWriter = new StringWriter())
        //    {
        //        Console.SetOut(stringWriter);
        //        _uut.TurnOn(power);
        //        output = stringWriter.ToString();
        //    }

        //    Assert.That(output, Is.EqualTo("PowerTube works with 10 %\r\n"));
        //}

        [TestCase(10)]
        public void Test_if_TurnOn_is_correct_input(int power)
        {
            //SetUp
            //Act
            _uut.TurnOn(power);
            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x => x == "PowerTube works with 10 %"));
        }


        [TestCase(10)]
        public void Test_if_TurnOf_works(int power)
        {
            //SetUp
            _uut.TurnOn(power);
            //Act
            _uut.TurnOff();
            //Assert
            _output.Received().OutputLine(Arg.Is<string>(x => x == "PowerTube turned off"));
        }
    }
}

//[TestCase(101)]
//[TestCase(0)]
//[TestCase(10101)]
//[Test]
//public void Test_if_TunOn_is_Not_correct_input(int power)
//{
//    Assert.That(() => _uut.TurnOn(power), Throws.ArgumentException);
//}

//[TestCase(1)]
//public void Test_if_TunOn_is_already_on_1(int power)
//{
//    _uut.TurnOn(power);
//    Assert.That(() => _uut.TurnOn(power), Throws.InvalidOperationException);
//}

//[TestCase(99)]
//public void Test_if_TunOn_is_already_on_99(int power)
//{
//    _uut.TurnOn(power);
//    Assert.That(() => _uut.TurnOn(power), Throws.InvalidOperationException);
//}


//[TestCase(50)]
//public void Test_if_TunOn_is_already_on(int power)
//{
//    _uut.TurnOn(power);
//    Assert.That(() => _uut.TurnOn(power), Throws.InvalidOperationException);
//}