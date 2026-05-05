using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_5
{
    static class Level1
    {
        static void SortByTaxCode(Student[] arr, int size)
        {
            for (int i = 1; i < size; i++)
            {
                Student key = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j].TaxCode > key.TaxCode)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
        }

        public static int InterpolationSearch(Student[] arr, int size, uint key)
        {
            int left = 0;
            int right = size - 1;

            while (left <= right
                   && key >= arr[left].TaxCode
                   && key <= arr[right].TaxCode)
            {
                if (arr[left].TaxCode == arr[right].TaxCode)
                {
                    if (arr[left].TaxCode == key) return left;
                    else return -1;
                }

                int pos = left + (int)(
                    ((long)(key - arr[left].TaxCode) * (right - left))
                    / (arr[right].TaxCode - arr[left].TaxCode)
                );

                if (arr[pos].TaxCode == key) return pos;
                if (arr[pos].TaxCode < key) left  = pos + 1;
                else right = pos - 1;
            }
            return -1;
        }

        static int DeleteAt(Student[] arr, int size, int idx)
        {
            for (int i = idx; i < size - 1; i++)
                arr[i] = arr[i + 1];
            arr[size - 1] = null;
            return size - 1;
        }

        public static void Run()
        {
            Console.WriteLine("=== РІВЕНЬ 1: Інтерполяційний пошук (невпорядкований масив) ===\n");

            Student[] arr = new Student[20];
            int size = 20;

            arr[0]  = new Student("Іваненко", "Олег", 1234567890u, "денна");
            arr[1]  = new Student("Петренко", "Анна", 2876543210u, "заочна");
            arr[2]  = new Student("Сидоренко", "Марко", 3456789012u, "денна");
            arr[3]  = new Student("Коваль", "Тетяна", 1987654321u, "заочна");
            arr[4]  = new Student("Мороз", "Дмитро", 2345678901u, "денна");
            arr[5]  = new Student("Бондар", "Ірина", 3012345678u, "заочна");
            arr[6]  = new Student("Лисенко", "Сергій", 1654321098u, "денна");
            arr[7]  = new Student("Шевченко", "Олена", 2109876543u, "заочна");
            arr[8]  = new Student("Гриценко", "Павло", 3789012345u, "денна");
            arr[9]  = new Student("Тимченко", "Юлія", 1543210987u, "заочна");
            arr[10] = new Student("Кравченко", "Андрій", 2678901234u, "денна");
            arr[11] = new Student("Савченко", "Наталя", 3210987654u, "заочна");
            arr[12] = new Student("Романенко", "Віктор", 1890123456u, "денна");
            arr[13] = new Student("Данченко", "Катерина", 2456789012u, "заочна");
            arr[14] = new Student("Поліщук", "Олексій", 3123456789u, "денна");
            arr[15] = new Student("Ткаченко", "Марина", 1765432109u, "заочна");
            arr[16] = new Student("Федоренко", "Руслан", 2234567890u, "денна");
            arr[17] = new Student("Мельник", "Оксана", 3567890123u, "заочна");
            arr[18] = new Student("Клименко", "Богдан", 1432109876u, "денна");
            arr[19] = new Student("Назаренко", "Аліна", 2901234567u, "заочна");

            Console.WriteLine("Масив до сортування (невпорядкований):");
            T.Print(arr);

            SortByTaxCode(arr, size);
            Console.WriteLine("\nМасив після сортування за TaxCode (для пошуку):");
            T.Print(arr);

            uint[] searchCodes = { 2876543210u, 1234567890u, 2456789012u };

            foreach (uint code in searchCodes)
            {
                Console.WriteLine($"\nПошук студента з TaxCode = {code}:");
                int idx = InterpolationSearch(arr, size, code);

                if (idx == -1)
                {
                    Console.WriteLine("  Не знайдено.");
                    continue;
                }

                Console.WriteLine($"  Знайдено: {arr[idx]}");

                if (arr[idx].IsExtramural)
                {
                    Console.WriteLine("  Студент навчається заочно — видаляємо.");
                    size = DeleteAt(arr, size, idx);
                    Console.WriteLine($"  Масив після видалення ({size} елементів):");
                    T.Header();
                    for (int i = 0; i < size; i++) Console.WriteLine(arr[i]);
                    T.Footer();
                }
                else
                {
                    Console.WriteLine("  Студент навчається денно — не видаляємо.");
                }
            }
        }
    }
}
