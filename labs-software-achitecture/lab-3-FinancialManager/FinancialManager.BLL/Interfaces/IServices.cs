using System.Collections.Generic;
using FinancialManager.BLL.DTOs;

namespace FinancialManager.BLL.Interfaces
{

    public interface IAccountService
    {
        IEnumerable<AccountDTO> GetAll();
        AccountDTO GetById(int id);
        void Create(AccountDTO dto);
        void Update(AccountDTO dto);
        void Delete(int id);
    }

    public interface IIncomeService
    {
        IEnumerable<IncomeDTO> GetAll();
        IEnumerable<IncomeDTO> GetByAccount(int accountId);
        IEnumerable<IncomeDTO> GetByCategory(int categoryId);
        void Create(IncomeDTO dto);
        void Delete(int id);
    }

    public interface IExpenseService
    {
        IEnumerable<ExpenseDTO> GetAll();
        IEnumerable<ExpenseDTO> GetByAccount(int accountId);
        IEnumerable<ExpenseDTO> GetByCategory(int categoryId);
        void Create(ExpenseDTO dto);
        void Delete(int id);
    }

    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAll();
        IEnumerable<CategoryDTO> GetByType(string type);
        void Create(CategoryDTO dto);
    }

    public interface IAnalyticsService
    {
        IEnumerable<CategorySummaryDTO> GetIncomeByCategory();

        IEnumerable<CategorySummaryDTO> GetExpenseByCategory();

        // Повертає зведений баланс по кожному рахунку
        IEnumerable<AccountSummaryDTO> GetSummaryByAccount();
    }
}
