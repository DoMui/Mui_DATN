using System;
using NUnit.Framework;
using DATN.Source_files;

namespace DATN.Tests
{
    [TestFixture]
    public class ValidatorTests
    {
        [TestCase(35, true)]
        public void LaTuoiHopLeTest(int tuoi, bool expected)
        {
            var obj = new Validator();
            var result = obj.LaTuoiHopLe(tuoi);
            Assert.AreEqual(expected, result);
        }

    }
}
