using System;
using NUnit.Framework;
using DATN.Source_files;

namespace DATN.Tests
{
    [TestFixture]
    public class GeometryTests
    {
        [TestCase(1, 2, 2)]
        public void DienTichChuNhatTest(double dai, double rong, double expected)
        {
            var obj = new Geometry();
            var result = obj.DienTichChuNhat(dai, rong);
            Assert.AreEqual(expected, result);
        }

        [TestCase(1, 2, 6)]
        public void ChuViChuNhatTest(double dai, double rong, double expected)
        {
            var obj = new Geometry();
            var result = obj.ChuViChuNhat(dai, rong);
            Assert.AreEqual(expected, result);
        }

    }
}
