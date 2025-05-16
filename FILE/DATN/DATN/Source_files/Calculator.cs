using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Source_files
{
    public class Calculator
    {
        public int Cong(int a, int b)
        {
            return a + b;
        }

        public int Tru(int a, int b)
        {
            return a - b;
        }

        public int Nhan(int a, int b)
        {
            return a * b;
        }

        public int Chia(int a, int b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Không chia được cho 0");
            }
            return a / b;
        }
    }
}
