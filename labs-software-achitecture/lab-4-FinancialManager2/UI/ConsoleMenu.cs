using FinancialManager.BLL.DTOs;
using FinancialManager.BLL.Interfaces;
using System;
using System.Text;

namespace FinancialManager.UI
{
    public class ConsoleMenu
    {
        private readonly IAccountService _accountService;
        private readonly IIncomeService _incomeService;
        private readonly IExpenseService _expenseService;
        private readonly ICategoryService _categoryService;
        private readonly IAnalyticsService _analyticsService;

        public ConsoleMenu(
            IAccountService accountService,
            IIncomeService incomeService,
            IExpenseService expenseService,
            ICategoryService categoryService,
            IAnalyticsService analyticsService)
        {
            _accountService = accountService;
            _incomeService = incomeService;
            _expenseService = expenseService;
            _categoryService = categoryService;
            _analyticsService = analyticsService;
        }

        public void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            while (true)
            {
                PrintMainMenu();
                var choice = InputHelper.ReadString("  Оберіть пункт: ");
                Console.Clear();
                switch (choice.Trim())
                {
                    case "1": AccountsMenu(); break;
                    case "2": IncomesMenu(); break;
                    case "3": ExpensesMenu(); break;
                    case "4": CategoriesMenu(); break;
                    case "5": AnalyticsMenu(); break;
                    case "0":
                        Console.WriteLine("  До побачення!");
                        return;
                    default:
                        Console.WriteLine("  Невірний вибір.");
                        break;
                }
            }
        }

        private void PrintMainMenu()
        {
            Console.WriteLine("\n╔═══════════════════════════════════╗");
            Console.WriteLine("║     ФІНАНСОВИЙ МЕНЕДЖЕР           ║");
            Console.WriteLine("╠═══════════════════════════════════╣");
            Console.WriteLine("║  1. Рахунки                       ║");
            Console.WriteLine("║  2. Доходи                        ║");
            Console.WriteLine("║  3. Витрати                       ║");
            Console.WriteLine("║  4. Категорії                     ║");
            Console.WriteLine("║  5. Аналітика                     ║");
            Console.WriteLine("║  0. Вихід                         ║");
            Console.WriteLine("╚═══════════════════════════════════╝");
        }

        private void AccountsMenu()
        {
            Console.WriteLine("\n── РАХУНКИ ──");
            Console.WriteLine("  1. Переглянути всі");
            Console.WriteLine("  2. Додати рахунок");
            Console.WriteLine("  3. Видалити рахунок");
            Console.WriteLine("  0. Назад");
            var choice = InputHelper.ReadString("  Оберіть: ");
            switch (choice.Trim())
            {
                case "1":
                    DisplayHelper.PrintAccounts(_accountService.GetAll());
                    break;
                case "2":
                    try
                    {
                        var name = InputHelper.ReadString("  Назва рахунку: ");
                        var balance = InputHelper.ReadDecimal("  Початковий баланс: ");
                        _accountService.Create(new AccountDTO { Name = name, Balance = balance });
                        DisplayHelper.PrintSuccess("Рахунок створено.");
                    }
                    catch (Exception ex) { DisplayHelper.PrintError(ex.Message); }
                    break;
                case "3":
                    try
                    {
                        DisplayHelper.PrintAccounts(_accountService.GetAll());
                        var id = InputHelper.ReadInt("  Id рахунку для видалення: ");
                        _accountService.Delete(id);
                        DisplayHelper.PrintSuccess("Рахунок видалено.");
                    }
                    catch (Exception ex) { DisplayHelper.PrintError(ex.Message); }
                    break;
            }
        }

        private void IncomesMenu()
        {
            Console.WriteLine("\n── ДОХОДИ ──");
            Console.WriteLine("  1. Переглянути всі");
            Console.WriteLine("  2. Переглянути по рахунку");
            Console.WriteLine("  3. Додати дохід");
            Console.WriteLine("  4. Видалити дохід");
            Console.WriteLine("  0. Назад");
            var choice = InputHelper.ReadString("  Оберіть: ");
            switch (choice.Trim())
            {
                case "1":
                    DisplayHelper.PrintIncomes(_incomeService.GetAll());
                    break;
                case "2":
                    DisplayHelper.PrintAccounts(_accountService.GetAll());
                    var accId = InputHelper.ReadInt("  Id рахунку: ");
                    DisplayHelper.PrintIncomes(_incomeService.GetByAccount(accId));
                    break;
                case "3":
                    try
                    {
                        DisplayHelper.PrintAccounts(_accountService.GetAll());
                        var accountId = InputHelper.ReadInt("  Id рахунку: ");
                        DisplayHelper.PrintCategories(_categoryService.GetByType("Income"));
                        var categoryId = InputHelper.ReadInt("  Id категорії: ");
                        var amount = InputHelper.ReadDecimal("  Сума: ");
                        var date = InputHelper.ReadDate("  Дата (дд.мм.рррр або Enter): ");
                        var desc = InputHelper.ReadString("  Опис (необов'язково): ");
                        _incomeService.Create(new IncomeDTO
                        {
                            AccountId = accountId,
                            CategoryId = categoryId,
                            Amount = amount,
                            Date = date,
                            Description = desc
                        });
                        DisplayHelper.PrintSuccess("Дохід додано.");
                    }
                    catch (Exception ex) { DisplayHelper.PrintError(ex.Message); }
                    break;
                case "4":
                    try
                    {
                        DisplayHelper.PrintIncomes(_incomeService.GetAll());
                        var id = InputHelper.ReadInt("  Id доходу для видалення: ");
                        _incomeService.Delete(id);
                        DisplayHelper.PrintSuccess("Дохід видалено.");
                    }
                    catch (Exception ex) { DisplayHelper.PrintError(ex.Message); }
                    break;
            }
        }

        private void ExpensesMenu()
        {
            Console.WriteLine("\n── ВИТРАТИ ──");
            Console.WriteLine("  1. Переглянути всі");
            Console.WriteLine("  2. Переглянути по рахунку");
            Console.WriteLine("  3. Додати витрату");
            Console.WriteLine("  4. Видалити витрату");
            Console.WriteLine("  0. Назад");
            var choice = InputHelper.ReadString("  Оберіть: ");
            switch (choice.Trim())
            {
                case "1":
                    DisplayHelper.PrintExpenses(_expenseService.GetAll());
                    break;
                case "2":
                    DisplayHelper.PrintAccounts(_accountService.GetAll());
                    var accId = InputHelper.ReadInt("  Id рахунку: ");
                    DisplayHelper.PrintExpenses(_expenseService.GetByAccount(accId));
                    break;
                case "3":
                    try
                    {
                        DisplayHelper.PrintAccounts(_accountService.GetAll());
                        var accountId = InputHelper.ReadInt("  Id рахунку: ");
                        DisplayHelper.PrintCategories(_categoryService.GetByType("Expense"));
                        var categoryId = InputHelper.ReadInt("  Id категорії: ");
                        var amount = InputHelper.ReadDecimal("  Сума: ");
                        var date = InputHelper.ReadDate("  Дата (дд.мм.рррр або Enter): ");
                        var desc = InputHelper.ReadString("  Опис (необов'язково): ");
                        _expenseService.Create(new ExpenseDTO
                        {
                            AccountId = accountId,
                            CategoryId = categoryId,
                            Amount = amount,
                            Date = date,
                            Description = desc
                        });
                        DisplayHelper.PrintSuccess("Витрату додано.");
                    }
                    catch (Exception ex) { DisplayHelper.PrintError(ex.Message); }
                    break;
                case "4":
                    try
                    {
                        DisplayHelper.PrintExpenses(_expenseService.GetAll());
                        var id = InputHelper.ReadInt("  Id витрати для видалення: ");
                        _expenseService.Delete(id);
                        DisplayHelper.PrintSuccess("Витрату видалено.");
                    }
                    catch (Exception ex) { DisplayHelper.PrintError(ex.Message); }
                    break;
            }
        }
        private void CategoriesMenu()
        {
            Console.WriteLine("\n── КАТЕГОРІЇ ──");
            Console.WriteLine("  1. Переглянути всі");
            Console.WriteLine("  2. Додати категорію");
            Console.WriteLine("  0. Назад");
            var choice = InputHelper.ReadString("  Оберіть: ");
            switch (choice.Trim())
            {
                case "1":
                    DisplayHelper.PrintCategories(_categoryService.GetAll());
                    break;
                case "2":
                    try
                    {
                        var name = InputHelper.ReadString("  Назва категорії: ");
                        Console.WriteLine("  Тип: 1 - Дохід, 2 - Витрата");
                        var typeChoice = InputHelper.ReadString("  Оберіть: ");
                        var type = typeChoice == "1" ? "Income" : "Expense";
                        _categoryService.Create(new CategoryDTO { Name = name, Type = type });
                        DisplayHelper.PrintSuccess("Категорію додано.");
                    }
                    catch (Exception ex) { DisplayHelper.PrintError(ex.Message); }
                    break;
            }
        }
        private void AnalyticsMenu()
        {
            Console.WriteLine("\n── АНАЛІТИКА ──");
            DisplayHelper.PrintIncomeSummary(_analyticsService.GetIncomeByCategory());
            DisplayHelper.PrintExpenseSummary(_analyticsService.GetExpenseByCategory());
            DisplayHelper.PrintAccountSummary(_analyticsService.GetSummaryByAccount());
            Console.WriteLine();
        }
    }
}
