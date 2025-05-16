using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Source_files
{
    public class Geometry
    {
        // ✅ Diện tích hình chữ nhật
        public double DienTichChuNhat(double dai, double rong)
        {
            return Math.Round(dai * rong, 3);
        }

        // ✅ Chu vi hình chữ nhật
        public double ChuViChuNhat(double dai, double rong)
        {
            return Math.Round(2 * (dai + rong), 3);
        }

        // ✅ Diện tích hình vuông
        public double DienTichHinhVuong(double canh)
        {
            return Math.Round(canh * canh, 3);
        }

        // ✅ Chu vi hình vuông
        public double ChuViHinhVuong(double canh)
        {
            return 4 * canh;
        }

        // ✅ Diện tích hình tròn
        public double DienTichHinhTron(double banKinh)
        {
            return Math.Round(Math.PI * banKinh * banKinh, 3);
        }

        // ✅ Chu vi hình tròn
        public double ChuViHinhTron(double banKinh)
        {
            return Math.Round(2 * Math.PI * banKinh, 3);
        }

        // ✅ Diện tích tam giác (công thức Heron)
        public double DienTichTamGiac(double a, double b, double c)
        {
            if (a > 0 && b > 0 && c > 0 && a + b > c && a + c > b && b + c > a)
            {
                double s = (a + b + c) / 2;
                double dienTich = Math.Sqrt(s * (s - a) * (s - b) * (s - c));
                return Math.Round(dienTich, 3);
            }
            return 0; // Trả về 0 nếu không phải là tam giác hợp lệ
        }

        // ✅ Chu vi tam giác
        public double ChuViTamGiac(double a, double b, double c)
        {
            return Math.Round(a + b + c, 3);
        }
    }
}
