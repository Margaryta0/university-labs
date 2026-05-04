using AutoMapper;
using FinancialManager.BLL.DTOs;
using FinancialManager.DAL.Entities;

namespace FinancialManager.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDTO>().ReverseMap();

            CreateMap<Income, IncomeDTO>()
                .ForMember(dest => dest.AccountName,
                    opt => opt.MapFrom(src => src.Account != null ? src.Account.Name : ""))
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : ""));

            CreateMap<IncomeDTO, Income>()
                .ForMember(dest => dest.Account, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<Expense, ExpenseDTO>()
                .ForMember(dest => dest.AccountName,
                    opt => opt.MapFrom(src => src.Account != null ? src.Account.Name : ""))
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : ""));

            CreateMap<ExpenseDTO, Expense>()
                .ForMember(dest => dest.Account, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<Category, CategoryDTO>()
                .ForMember(dest => dest.Type,
                    opt => opt.MapFrom(src => src.Type.ToString()));

            CreateMap<CategoryDTO, Category>()
                .ForMember(dest => dest.Type,
                    opt => opt.MapFrom(src =>
                        src.Type == "Income" ? CategoryType.Income : CategoryType.Expense));
        }
    }
}
