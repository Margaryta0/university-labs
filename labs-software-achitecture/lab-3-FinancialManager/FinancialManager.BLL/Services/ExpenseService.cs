using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FinancialManager.BLL.DTOs;
using FinancialManager.BLL.Interfaces;
using FinancialManager.DAL.Entities;
using FinancialManager.DAL.Repositories;

namespace FinancialManager.BLL.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ExpenseService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public IEnumerable<ExpenseDTO> GetAll()
        {
            var expenses = _uow.Expenses.GetAllWithIncludes(
                e => e.Account,
                e => e.Category
            );
            return _mapper.Map<IEnumerable<ExpenseDTO>>(expenses);
        }

        public IEnumerable<ExpenseDTO> GetByAccount(int accountId)
        {
            var expenses = _uow.Expenses.GetAllWithIncludes(
                e => e.Account,
                e => e.Category
            ).Where(e => e.AccountId == accountId);

            return _mapper.Map<IEnumerable<ExpenseDTO>>(expenses);
        }

        public IEnumerable<ExpenseDTO> GetByCategory(int categoryId)
        {
            var expenses = _uow.Expenses.GetAllWithIncludes(
                e => e.Account,
                e => e.Category
            ).Where(e => e.CategoryId == categoryId);

            return _mapper.Map<IEnumerable<ExpenseDTO>>(expenses);
        }

        public void Create(ExpenseDTO dto)
        {
            if (dto.Amount <= 0)
                throw new ArgumentException("Сума витрати має бути більше нуля.");

            var account = _uow.Accounts.GetById(dto.AccountId);
            if (account == null)
                throw new Exception($"Рахунок з Id={dto.AccountId} не знайдено.");

            if (account.Balance < dto.Amount)
                throw new Exception(
                    $"Недостатньо коштів на рахунку '{account.Name}'. " +
                    $"Баланс: {account.Balance:F2}, необхідно: {dto.Amount:F2}");

            var category = _uow.Categories.GetById(dto.CategoryId);
            if (category == null)
                throw new Exception($"Категорія з Id={dto.CategoryId} не знайдено.");
            if (category.Type != CategoryType.Expense)
                throw new Exception("Обрана категорія не є категорією витрат.");

            var expense = _mapper.Map<Expense>(dto);
            expense.Date = dto.Date == default ? DateTime.Now : dto.Date;

            account.Balance -= dto.Amount;

            _uow.Expenses.Add(expense);
            _uow.Accounts.Update(account);
            _uow.Save();
        }

        public void Delete(int id)
        {
            var expense = _uow.Expenses.GetById(id);
            if (expense == null)
                throw new Exception($"Витрата з Id={id} не знайдено.");

            var account = _uow.Accounts.GetById(expense.AccountId);
            if (account != null)
            {
                account.Balance += expense.Amount;
                _uow.Accounts.Update(account);
            }

            _uow.Expenses.Delete(id);
            _uow.Save();
        }
    }
}
