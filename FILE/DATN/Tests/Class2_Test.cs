using NUnit.Framework;
using ConsoleApp1;

namespace DATN.Tests
{
    [TestFixture]
    public class Class2Tests
    {
        [TestCase("!@#TEST_string_123", "   ", ExpectedResult = "!@#TEST_string_123-   ")]
        [TestCase("!@#TEST_string_123", "", ExpectedResult = "!@#TEST_string_123-")]
        [TestCase("", "default", ExpectedResult = "-default")]
        [TestCase("!@#TEST_string_123", "   ", ExpectedResult = "!@#TEST_string_123-   ")]
        [TestCase("!@#TEST_string_123", "!@#TEST_string_123", ExpectedResult = "!@#TEST_string_123-!@#TEST_string_123")]
        [TestCase("MGPno", "default", ExpectedResult = "MGPno-default")]
        [TestCase("!@#TEST_string_123", "   ", ExpectedResult = "!@#TEST_string_123-   ")]
        [TestCase("   ", "default", ExpectedResult = "   -default")]
        [TestCase("default", "", ExpectedResult = "default-")]
        public string GhepChuoiTest(string a, string b)
        {
            var obj = new Class2();
            return obj.GhepChuoi(a, b);
        }

    }
}
