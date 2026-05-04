using System.Collections.Generic;

namespace FinancialManager.DAL.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }     
        public decimal Balance { get; set; }  

        public ICollection<Income> Incomes { get; set; } = new List<Income>();
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
