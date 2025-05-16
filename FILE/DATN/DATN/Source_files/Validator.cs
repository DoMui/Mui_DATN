using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DATN.Source_files
{
    public class Validator
    {
        // ✅ Kiểm tra tuổi hợp lệ để lao động(từ 18 đến 60)
        public bool LaTuoiHopLe(int tuoi)
        {
            return tuoi >= 18 && tuoi <= 60;
        }

        // ✅ Kiểm tra mật khẩu hợp lệ (dài >= 6, không chứa khoảng trắng)
        public bool MatKhauHopLe(string matkhau)
        {
            return matkhau.Length >= 6 && !matkhau.Contains(" ");
        }

        // ✅ Kiểm tra điểm số hợp lệ (từ 0 đến 10)
        public bool DiemHopLe(double diem)
        {
            return diem >= 0.0 && diem <= 10.0;
        }

        // ✅ Kiểm tra chuỗi không rỗng
        public bool KhongRong(string chuoi)
        {
            return !string.IsNullOrWhiteSpace(chuoi);
        }

        // ✅ Kiểm tra định dạng email
        public bool EmailHopLe(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // ✅ Kiểm tra năm nhuận
        public bool NamNhuan(int nam)
        {
            if (nam <= 0)
            {
                return false; // Năm không hợp lệ (<= 0) thì không phải năm nhuận
            }

            return (nam % 4 == 0 && nam % 100 != 0) || (nam % 400 == 0);
        }

        // ✅ Kiểm tra số dương
        public bool LaSoDuong(int so)
        {
            return so > 0;
        }

        // ✅ Kiểm tra số chẵn
        public bool LaSoChan(int so)
        {
            return so % 2 == 0;
        }

        // ✅ Kiểm tra số nằm trong khoảng [min, max]
        public bool NamTrongKhoang(int giaTri, int nhoNhat, int lonNhat)
        {
            return giaTri >= nhoNhat && giaTri <= lonNhat;
        }

        // ✅ Kiểm tra có chứa ký tự đặc biệt hay không
        public bool CoKyTuDacBiet(string chuoi)
        {
            return Regex.IsMatch(chuoi, @"[^a-zA-Z0-9]");
        }
    }
}
