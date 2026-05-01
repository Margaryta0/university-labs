using System;
using System.Collections.Generic;
using AutoMapper;
using FinancialManager.BLL.DTOs;
using FinancialManager.BLL.Interfaces;
using FinancialManager.DAL.Entities;
using FinancialManager.DAL.Repositories;

namespace FinancialManager.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public IEnumerable<AccountDTO> GetAll()
        {
            var accounts = _uow.Accounts.GetAll();
            // Map: IEnumerable<Account> -> IEnumerable<AccountDTO>
            return _mapper.Map<IEnumerable<AccountDTO>>(accounts);
        }

        public AccountDTO GetById(int id)
        {
            var account = _uow.Accounts.GetById(id);
            if (account == null)
                throw new Exception($"Рахунок з Id={id} не знайдено.");
            return _mapper.Map<AccountDTO>(account);
        }

        public void Create(AccountDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Назва рахунку не може бути порожньою.");

            if (dto.Balance < 0)
                throw new ArgumentException("Початковий баланс не може бути від'ємним.");

            var account = _mapper.Map<Account>(dto);
            _uow.Accounts.Add(account);
            _uow.Save();  // фіксуємо в БД
        }

        public void Update(AccountDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Назва рахунку не може бути порожньою.");

            var account = _mapper.Map<Account>(dto);
            _uow.Accounts.Update(account);
            _uow.Save();
        }

        public void Delete(int id)
        {
            var account = _uow.Accounts.GetById(id);
            if (account == null)
                throw new Exception($"Рахунок з Id={id} не знайдено.");

            _uow.Accounts.Delete(id);
            _uow.Save();
        }
    }
}
