using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_7
{
    class DifferentialEquation
    {
        public static double F(double x, double y)
        {
            double denom = y * y - 6.0 * x;
            if (Math.Abs(denom) < 1e-14)
                return double.NaN;
            return -2.0 * y / denom;
        }

        public static void RungeKutta4(double x0, double y0, double xEnd, double h)
        {
            Console.WriteLine("\n--- Метод Рунге-Кутта 4-го порядку ---");
            Console.WriteLine($"{"x",12}  {"y",18}");
            Console.WriteLine(new string('-', 34));

            double x = x0;
            double y = y0;

            Console.WriteLine($"{x,12:F4}  {y,18:F8}"); 

            while (x < xEnd - 1e-10)
            {
                if (x + h > xEnd) h = xEnd - x;

                double k1 = h * F(x, y);
                double k2 = h * F(x + h / 2.0, y + k1 / 2.0);
                double k3 = h * F(x + h / 2.0, y + k2 / 2.0);
                double k4 = h * F(x + h, y + k3);

                if (double.IsNaN(k1) || double.IsNaN(k2) ||
                    double.IsNaN(k3) || double.IsNaN(k4))
                {
                    Console.WriteLine($"{'*',12}  Особлива точка — обчислення зупинено.");
                    break;
                }

                // Формула Рунге-Кутта 4-го порядку
                y = y + (k1 + 2.0 * k2 + 2.0 * k3 + k4) / 6.0;
                x += h;

                Console.WriteLine($"{x,12:F4}  {y,18:F8}");
            }
        }
    }

}
