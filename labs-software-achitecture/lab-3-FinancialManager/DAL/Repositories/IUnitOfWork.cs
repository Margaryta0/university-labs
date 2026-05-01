using System;
using FinancialManager.DAL.Entities;

namespace FinancialManager.DAL.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Account> Accounts { get; }
        IRepository<Income> Incomes { get; }
        IRepository<Expense> Expenses { get; }
        IRepository<Category> Categories { get; }

        void Save();
    }
}
