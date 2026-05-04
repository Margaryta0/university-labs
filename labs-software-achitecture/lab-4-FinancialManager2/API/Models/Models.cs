using System;
namespace FinancialManager.API.Models
{

    // Модель рахунку для відповіді клієнту (GET)
    public class AccountModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }

    public class CreateAccountModel
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }

    // Модель доходу для відповіді клієнту
    public class IncomeModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string AccountName { get; set; }
        public string CategoryName { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
    }

    // Модель для створення доходу (POST)
    public class CreateIncomeModel
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
    }

    // Модель витрати для відповіді клієнту
    public class ExpenseModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string AccountName { get; set; }
        public string CategoryName { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
    }

    // Модель для створення витрати (POST)
    public class CreateExpenseModel
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
    }

    // Модель категорії
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    // Модель для створення категорії
    public class CreateCategoryModel
    {
        public string Name { get; set; }
        public string Type { get; set; } 
    }

    // Модель для аналітики — підсумок по категорії
    public class CategorySummaryModel
    {
        public string CategoryName { get; set; }
        public decimal TotalAmount { get; set; }
    }

    // Модель для аналітики — підсумок по рахунку
    public class AccountSummaryModel
    {
        public string AccountName { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance { get; set; }
    }
}
