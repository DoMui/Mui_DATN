using System;

namespace DATN.Source_files
{
    public class MyString
    {
        // Ghép chuỗi theo định dạng: "a-b"
        public string GhepChuoi(string a, string b)
        {
            // nối chuỗi thay cho interpolation
            return a + "-" + b;
        }

        // Đếm tổng chiều dài của 2 chuỗi
        public int DoDaiTong(string a, string b)
        {
            // thay ?.Length ?? 0
            int lenA = (a != null) ? a.Length : 0;
            int lenB = (b != null) ? b.Length : 0;
            return lenA + lenB;
        }

        // Kiểm tra chuỗi a có chứa chuỗi b không
        public bool ChuaChuoiKhac(string a, string b)
        {
            if (a == null || b == null) return false;
            return a.Contains(b);
        }

        // ✅ Đảo ngược chuỗi
        public string DaoNguocChuoi(string a)
        {
            if (a == null) return "";
            char[] arr = a.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        // ✅ Viết thường toàn bộ chuỗi
        public string ChuyenThanhChuThuong(string a)
        {
            // thay a?.ToLower() ?? ""
            return (a != null) ? a.ToLower() : "";
        }

        // ✅ Xóa khoảng trắng trong chuỗi
        public string XoaKhoangTrang(string a)
        {
            // thay a?.Replace(...) ?? ""
            return (a != null) ? a.Replace(" ", "") : "";
        }
    }
}
