using System;
using System.Collections.Generic;
using FinancialManager.BLL.DTOs;

namespace FinancialManager.UI
{
    public static class DisplayHelper
    {
        public static void PrintAccounts(IEnumerable<AccountDTO> accounts)
        {
            Console.WriteLine("\n╔═════════════════════════════════════╗");
            Console.WriteLine("║           РАХУНКИ                   ║");
            Console.WriteLine("╠════╦══════════════════╦═════════════╣");
            Console.WriteLine("║ Id ║ Назва            ║ Баланс      ║");
            Console.WriteLine("╠════╬══════════════════╬═════════════╣");
            foreach (var a in accounts)
                Console.WriteLine($"║ {a.Id,-2} ║ {a.Name,-16} ║ {a.Balance,10:F2} ║");
            Console.WriteLine("╚════╩══════════════════╩═════════════╝");
        }

        public static void PrintIncomes(IEnumerable<IncomeDTO> incomes)
        {
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                        ДОХОДИ                                   ║");
            Console.WriteLine("╠════╦═══════════╦════════════╦══════════════════╦════════════════╣");
            Console.WriteLine("║ Id ║ Сума      ║ Дата       ║ Категорія        ║ Рахунок        ║");
            Console.WriteLine("╠════╬═══════════╬════════════╬══════════════════╬════════════════╣");
            foreach (var i in incomes)
                Console.WriteLine(
                    $"║ {i.Id,-2} ║ {i.Amount,9:F2} ║ {i.Date:dd.MM.yyyy} ║ " +
                    $"{i.CategoryName,-16} ║ {i.AccountName,-14} ║");
            Console.WriteLine("╚════╩═══════════╩════════════╩══════════════════╩════════════════╝");
        }

        public static void PrintExpenses(IEnumerable<ExpenseDTO> expenses)
        {
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                       ВИТРАТИ                                   ║");
            Console.WriteLine("╠════╦═══════════╦════════════╦══════════════════╦════════════════╣");
            Console.WriteLine("║ Id ║ Сума      ║ Дата       ║ Категорія        ║ Рахунок        ║");
            Console.WriteLine("╠════╬═══════════╬════════════╬══════════════════╬════════════════╣");
            foreach (var e in expenses)
                Console.WriteLine(
                    $"║ {e.Id,-2} ║ {e.Amount,9:F2} ║ {e.Date:dd.MM.yyyy} ║ " +
                    $"{e.CategoryName,-16} ║ {e.AccountName,-14} ║");
            Console.WriteLine("╚════╩═══════════╩════════════╩══════════════════╩════════════════╝");
        }

        public static void PrintCategories(IEnumerable<CategoryDTO> categories)
        {
            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("║         КАТЕГОРІЇ              ║");
            Console.WriteLine("╠════╦══════════════╦════════════╣");
            Console.WriteLine("║ Id ║ Назва        ║ Тип        ║");
            Console.WriteLine("╠════╬══════════════╬════════════╣");
            foreach (var c in categories)
                Console.WriteLine($"║ {c.Id,-2} ║ {c.Name,-12} ║ {c.Type,-10} ║");
            Console.WriteLine("╚════╩══════════════╩════════════╝");
        }

        public static void PrintIncomeSummary(IEnumerable<CategorySummaryDTO> summaries)
        {
            Console.WriteLine("\n  📈 ДОХОДИ ПО КАТЕГОРІЯХ:");
            Console.WriteLine("  ─────────────────────────────────");
            foreach (var s in summaries)
                Console.WriteLine($"  {s.CategoryName,-20} {s.TotalAmount,10:F2} грн");
        }

        public static void PrintExpenseSummary(IEnumerable<CategorySummaryDTO> summaries)
        {
            Console.WriteLine("\n  📉 ВИТРАТИ ПО КАТЕГОРІЯХ:");
            Console.WriteLine("  ─────────────────────────────────");
            foreach (var s in summaries)
                Console.WriteLine($"  {s.CategoryName,-20} {s.TotalAmount,10:F2} грн");
        }

        public static void PrintAccountSummary(IEnumerable<AccountSummaryDTO> summaries)
        {
            Console.WriteLine("\n  🏦 ЗВЕДЕННЯ ПО РАХУНКАХ:");
            Console.WriteLine("  ───────────────────────────────────────────────");
            Console.WriteLine($"  {"Рахунок",-16} {"Доходи",10} {"Витрати",10} {"Баланс",10}");
            Console.WriteLine("  ───────────────────────────────────────────────");
            foreach (var s in summaries)
                Console.WriteLine(
                    $"  {s.AccountName,-16} {s.TotalIncome,10:F2} {s.TotalExpense,10:F2} {s.Balance,10:F2}");
        }

        public static void PrintError(string message)
        {
            Console.WriteLine($"\n  ❌ Помилка: {message}");
        }

        public static void PrintSuccess(string message)
        {
            Console.WriteLine($"\n  ✅ {message}");
        }
    }
}
