using NUnit.Framework;
using ConsoleApp1;

namespace DATN.Tests
{
    [TestFixture]
    public class Class1Tests
    {
        [TestCase(47, 15, ExpectedResult = 62)]
        public int HCNTest(int cd, int cr)
        {
            var obj = new Class1();
            return obj.HCN(cd, cr);
        }

    }
}
