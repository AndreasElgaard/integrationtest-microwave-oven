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
    public class T1Display
    {
        private IOutput _output;
        private Display _utt;

        [SetUp]
        public void Setup()
        {
            _output = new Output();
            _utt = new Display(_output);
        }

        [TestCase(1, 20)]
        public void ShowTime(int min, int sec)
        {
            _output.OutputLine($"Display shows: {min:D2}:{sec:D2}");


           
        }



    }
}