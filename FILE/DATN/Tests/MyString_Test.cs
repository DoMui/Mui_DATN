using System;
using NUnit.Framework;
using DATN.Source_files;

namespace DATN.Tests
{
    [TestFixture]
    public class MyStringTests
    {
        [TestCase("abc", "abc", "abc-abc")]
        public void GhepChuoiTest(string a, string b, string expected)
        {
            var obj = new MyString();
            var result = obj.GhepChuoi(a, b);
            Assert.AreEqual(expected, result);
        }

    }
}
