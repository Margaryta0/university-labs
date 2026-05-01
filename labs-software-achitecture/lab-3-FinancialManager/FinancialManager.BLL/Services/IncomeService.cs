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
    public class IncomeService : IIncomeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public IncomeService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public IEnumerable<IncomeDTO> GetAll()
        {
            // GetAllWithIncludes — EF зробить JOIN з Account і Category
            // Тепер income.Account і income.Category — НЕ null
            // AutoMapper побачить Account.Name і Category.Name
            // і заповнить AccountName і CategoryName в DTO автоматично
            var incomes = _uow.Incomes.GetAllWithIncludes(
                i => i.Account,
                i => i.Category
            );
            return _mapper.Map<IEnumerable<IncomeDTO>>(incomes);
        }

        public IEnumerable<IncomeDTO> GetByAccount(int accountId)
        {
            var incomes = _uow.Incomes.GetAllWithIncludes(
                i => i.Account,
                i => i.Category
            ).Where(i => i.AccountId == accountId);

            return _mapper.Map<IEnumerable<IncomeDTO>>(incomes);
        }

        public IEnumerable<IncomeDTO> GetByCategory(int categoryId)
        {
            var incomes = _uow.Incomes.GetAllWithIncludes(
                i => i.Account,
                i => i.Category
            ).Where(i => i.CategoryId == categoryId);

            return _mapper.Map<IEnumerable<IncomeDTO>>(incomes);
        }

        public void Create(IncomeDTO dto)
        {
            if (dto.Amount <= 0)
                throw new ArgumentException("Сума доходу має бути більше нуля.");

            var account = _uow.Accounts.GetById(dto.AccountId);
            if (account == null)
                throw new Exception($"Рахунок з Id={dto.AccountId} не знайдено.");

            var category = _uow.Categories.GetById(dto.CategoryId);
            if (category == null)
                throw new Exception($"Категорія з Id={dto.CategoryId} не знайдено.");
            if (category.Type != CategoryType.Income)
                throw new Exception("Обрана категорія не є категорією доходів.");

            var income = _mapper.Map<Income>(dto);
            income.Date = dto.Date == default ? DateTime.Now : dto.Date;

            account.Balance += dto.Amount;

            _uow.Incomes.Add(income);
            _uow.Accounts.Update(account);
            _uow.Save();
        }

        public void Delete(int id)
        {
            var income = _uow.Incomes.GetById(id);
            if (income == null)
                throw new Exception($"Дохід з Id={id} не знайдено.");

            var account = _uow.Accounts.GetById(income.AccountId);
            if (account != null)
            {
                account.Balance -= income.Amount;
                _uow.Accounts.Update(account);
            }

            _uow.Incomes.Delete(id);
            _uow.Save();
        }
    }
}
