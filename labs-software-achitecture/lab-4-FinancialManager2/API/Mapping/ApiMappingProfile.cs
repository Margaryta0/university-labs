using AutoMapper;
using FinancialManager.API.Models;
using FinancialManager.BLL.DTOs;

namespace FinancialManager.API.Mapping
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<AccountDTO, AccountModel>().ReverseMap();

            CreateMap<CreateAccountModel, AccountDTO>();

            CreateMap<IncomeDTO, IncomeModel>().ReverseMap();

            CreateMap<CreateIncomeModel, IncomeDTO>();

            CreateMap<ExpenseDTO, ExpenseModel>().ReverseMap();

            CreateMap<CreateExpenseModel, ExpenseDTO>();

            CreateMap<CategoryDTO, CategoryModel>().ReverseMap();

            CreateMap<CreateCategoryModel, CategoryDTO>();

            CreateMap<CategorySummaryDTO, CategorySummaryModel>();
            CreateMap<AccountSummaryDTO, AccountSummaryModel>();
        }
    }
}
