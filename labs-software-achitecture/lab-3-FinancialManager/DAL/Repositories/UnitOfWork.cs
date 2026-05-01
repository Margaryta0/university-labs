using System;
using FinancialManager.DAL.Context;
using FinancialManager.DAL.Entities;

namespace FinancialManager.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        // Приватні поля для зберігання екземплярів репозиторіїв 
        private IRepository<Account> _accounts;
        private IRepository<Income> _incomes;
        private IRepository<Expense> _expenses;
        private IRepository<Category> _categories;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<Account> Accounts =>
            _accounts ??= new Repository<Account>(_context);

        public IRepository<Income> Incomes =>
            _incomes ??= new Repository<Income>(_context);

        public IRepository<Expense> Expenses =>
            _expenses ??= new Repository<Expense>(_context);

        public IRepository<Category> Categories =>
            _categories ??= new Repository<Category>(_context);

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
