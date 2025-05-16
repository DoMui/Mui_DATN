using NUnit.Framework;
using DATN.Source_files;

namespace DATN.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void TestCase1()
        {
            Calculator calc = new Calculator();
            Assert.AreEqual(624, calc.Add(985, -361));
        }

        [Test]
        public void TestCase2()
        {
            Calculator calc = new Calculator();
            Assert.AreEqual(-1621, calc.Add(-794, -827));
        }

        [Test]
        public void TestCase3()
        {
            Calculator calc = new Calculator();
            Assert.AreEqual(-750, calc.Add(174, -924));
        }

        [Test]
        public void TestCase4()
        {
            Calculator calc = new Calculator();
            Assert.AreEqual(1195, calc.Add(631, 564));
        }

        [Test]
        public void TestCase5()
        {
            Calculator calc = new Calculator();
            Assert.AreEqual(529, calc.Add(353, 176));
        }

        [Test]
        public void TestCase6()
        {
            Calculator calc = new Calculator();
            Assert.AreEqual(201, calc.Add(765, -564));
        }

        [Test]
        public void TestCase7()
        {
            Calculator calc = new Calculator();
            Assert.AreEqual(-1444, calc.Add(-752, -692));
        }

        [Test]
        public void TestCase8()
        {
            Calculator calc = new Calculator();
            Assert.AreEqual(1188, calc.Add(421, 767));
        }

        [Test]
        public void TestCase9()
        {
            Calculator calc = new Calculator();
            Assert.AreEqual(-15, calc.Add(-481, 466));
        }

        [Test]
        public void TestCase10()
        {
            Calculator calc = new Calculator();
            Assert.AreEqual(450, calc.Add(-79, 529));
        }

    }
}
