using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_7
{
    class RootFinding
    {
        public static double G(double x)
        {
            return Math.Pow(x, 5) + 18.0 * Math.Pow(x, 3) - 34.0;
        }

        public static double G_Deriv(double x)
        {
            return 5.0 * Math.Pow(x, 4) + 54.0 * x * x;
        }

        public static void BisectionMethod(double a, double b, double eps = 1e-6)
        {
            Console.WriteLine("\n--- Метод половинчастого ділення ---");
            if (G(a) * G(b) > 0)
            {
                Console.WriteLine("На цьому інтервалі коренів немає (g(a)*g(b) > 0).");
                return;
            }

            int iter = 0;
            double mid = a;
            while ((b - a) > eps)
            {
                mid = (a + b) / 2.0;    
                if (G(a) * G(mid) <= 0)
                    b = mid;           
                else
                    a = mid;           
                iter++;
            }
            Console.WriteLine($"Корінь: x ≈ {mid:F8},  g(x) = {G(mid):E4},  ітерацій: {iter}");
        }

        public static void NewtonMethod(double a, double b, double eps = 1e-6)
        {
            Console.WriteLine("\n--- Метод дотичних (Ньютона) ---");
            if (G(a) * G(b) > 0)
            {
                Console.WriteLine("На цьому інтервалі коренів немає.");
                return;
            }

            double x = (G(a) * G_Deriv(a) > 0) ? a : b;
            int iter = 0;

            while (true)
            {
                double gx = G(x);
                double gpx = G_Deriv(x);

                if (Math.Abs(gpx) < 1e-14) 
                {
                    Console.WriteLine("Похідна близька до нуля — метод не збігається.");
                    return;
                }

                double xNext = x - gx / gpx; 
                iter++;

                if (Math.Abs(xNext - x) < eps) 
                {
                    x = xNext;
                    break;
                }
                x = xNext;
            }
            Console.WriteLine($"Корінь: x ≈ {x:F8},  g(x) = {G(x):E4},  ітерацій: {iter}");
        }

        public static void ChordMethod(double a, double b, double eps = 1e-6)
        {
            Console.WriteLine("\n--- Метод хорд ---");
            if (G(a) * G(b) > 0)
            {
                Console.WriteLine("На цьому інтервалі коренів немає.");
                return;
            }

            int iter = 0;
            double x = a;
            while (true)
            {
                double ga = G(a);
                double gb = G(b);

                x = a - ga * (b - a) / (gb - ga); 
                iter++;

                double gx = G(x);
                if (Math.Abs(gx) < eps || Math.Abs(b - a) < eps)
                    break;

                if (ga * gx < 0)
                    b = x;
                else
                    a = x;
            }
            Console.WriteLine($"Корінь: x ≈ {x:F8},  g(x) = {G(x):E4},  ітерацій: {iter}");
        }
    }

}
