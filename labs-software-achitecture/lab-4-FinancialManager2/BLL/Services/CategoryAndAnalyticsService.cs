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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<CategoryDTO>>(_uow.Categories.GetAll());
        }

        public IEnumerable<CategoryDTO> GetByType(string type)
        {
            var categoryType = type == "Income" ? CategoryType.Income : CategoryType.Expense;
            var categories = _uow.Categories.GetAll()
                .Where(c => c.Type == categoryType);
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public void Create(CategoryDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Назва категорії не може бути порожньою.");

            if (dto.Type != "Income" && dto.Type != "Expense")
                throw new ArgumentException("Тип категорії має бути 'Income' або 'Expense'.");

            var category = _mapper.Map<Category>(dto);
            _uow.Categories.Add(category);
            _uow.Save();
        }
    }

    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork _uow;

        public AnalyticsService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<CategorySummaryDTO> GetIncomeByCategory()
        {
            var incomes = _uow.Incomes.GetAll();
            var categories = _uow.Categories.GetAll();

            return incomes
                .GroupBy(i => i.CategoryId)
                .Select(g => new CategorySummaryDTO
                {
                    CategoryName = categories
                        .FirstOrDefault(c => c.Id == g.Key)?.Name ?? "Невідома",
                    TotalAmount = g.Sum(i => i.Amount)
                })
                .OrderByDescending(s => s.TotalAmount)
                .ToList();
        }

        public IEnumerable<CategorySummaryDTO> GetExpenseByCategory()
        {
            var expenses = _uow.Expenses.GetAll();
            var categories = _uow.Categories.GetAll();

            return expenses
                .GroupBy(e => e.CategoryId)
                .Select(g => new CategorySummaryDTO
                {
                    CategoryName = categories
                        .FirstOrDefault(c => c.Id == g.Key)?.Name ?? "Невідома",
                    TotalAmount = g.Sum(e => e.Amount)
                })
                .OrderByDescending(s => s.TotalAmount)
                .ToList();
        }

        public IEnumerable<AccountSummaryDTO> GetSummaryByAccount()
        {
            var accounts = _uow.Accounts.GetAll();
            var incomes = _uow.Incomes.GetAll();
            var expenses = _uow.Expenses.GetAll();

            return accounts.Select(a => new AccountSummaryDTO
            {
                AccountName = a.Name,
                TotalIncome = incomes
                    .Where(i => i.AccountId == a.Id)
                    .Sum(i => i.Amount),
                TotalExpense = expenses
                    .Where(e => e.AccountId == a.Id)
                    .Sum(e => e.Amount),
                Balance = a.Balance
            }).ToList();
        }
    }
}
