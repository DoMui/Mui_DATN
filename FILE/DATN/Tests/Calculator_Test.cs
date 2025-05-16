using NUnit.Framework;
using DATN.Source_files;

namespace DATN.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [TestCase(83, 73, ExpectedResult = 156)]
        [TestCase(50, 76, ExpectedResult = 126)]
        public int CongTest(int a, int b)
        {
            var obj = new Calculator();
            return obj.Cong(a, b);
        }

        [TestCase(53, 31, ExpectedResult = 22)]
        [TestCase(-1, 81, ExpectedResult = -82)]
        public int TruTest(int a, int b)
        {
            var obj = new Calculator();
            return obj.Tru(a, b);
        }

        [TestCase(-40, 41, ExpectedResult = -1640)]
        [TestCase(62, -56, ExpectedResult = -3472)]
        public int NhanTest(int a, int b)
        {
            var obj = new Calculator();
            return obj.Nhan(a, b);
        }

        [TestCase(33, 60, ExpectedResult = 0)]
        [TestCase(21, 48, ExpectedResult = 0)]
        public int ChiaTest(int a, int b)
        {
            var obj = new Calculator();
            return obj.Chia(a, b);
        }

    }
}
