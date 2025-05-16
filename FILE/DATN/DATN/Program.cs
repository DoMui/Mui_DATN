using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms; // THÊM dòng này để dùng Windows Forms

namespace DATN
{
    public class Program
    {
        [STAThread] // BẮT BUỘC phải có với ứng dụng WinForms
        static void Main(string[] args)
        {
            
            Application.Run(new Form1());        // Chạy Form1

        }
    }
}
