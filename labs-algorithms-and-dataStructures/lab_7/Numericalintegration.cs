using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_7
{
    class NumericalIntegration
    {
        public static double F(double x)
        {
            return (1.0 + Math.Sqrt(x)) / (x * x);
        }

        public static double RectangleMethod(double a, double b, double h)
        {
            double sum = 0.0;
            int n = (int)Math.Round((b - a) / h);
            for (int i = 0; i < n; i++)
            {
                double x = a + i * h;   
                sum += F(x);
            }
            return sum * h;
        }

        public static double TrapezoidMethod(double a, double b, double h)
        {
            int n = (int)Math.Round((b - a) / h);
            double sum = F(a) + F(b);
            for (int i = 1; i < n; i++)
            {
                double x = a + i * h;
                sum += 2.0 * F(x);    
            }
            return sum * h / 2.0;
        }

        public static double SimpsonMethod(double a, double b, double h)
        {
            int n = (int)Math.Round((b - a) / h);
            if (n % 2 != 0) n++; 
            h = (b - a) / n;     

            double sum = F(a) + F(b); 
            for (int i = 1; i < n; i++)
            {
                double x = a + i * h;
                sum += (i % 2 != 0) ? 4.0 * F(x) : 2.0 * F(x);
            }
            return sum * h / 3.0;
        }
    }
}
