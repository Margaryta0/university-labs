using System;

namespace FinancialManager.BLL.DTOs
{

    public class AccountDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }

    public class IncomeDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string AccountName { get; set; }   
        public string CategoryName { get; set; }  
        public int AccountId { get; set; }        // потрібен для операцій запису
        public int CategoryId { get; set; }
    }

    public class ExpenseDTO
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
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }  
    }
    public class CategorySummaryDTO
    {
        public string CategoryName { get; set; }
        public decimal TotalAmount { get; set; }
    }

    // DTO для підсумку по рахунку (аналітика)
    public class AccountSummaryDTO
    {
        public string AccountName { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance { get; set; }
    }
}
