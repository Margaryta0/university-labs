using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FinancialManager.BLL.Interfaces;
using FinancialManager.BLL.Mapping;
using FinancialManager.BLL.Services;
using FinancialManager.DAL.Context;
using FinancialManager.DAL.Repositories;
using FinancialManager.UI;

var services = new ServiceCollection();

services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=financial.db"));

services.AddScoped<IUnitOfWork, UnitOfWork>();

services.AddAutoMapper(typeof(MappingProfile));

services.AddScoped<IAccountService, AccountService>();
services.AddScoped<IIncomeService, IncomeService>();
services.AddScoped<IExpenseService, ExpenseService>();
services.AddScoped<ICategoryService, CategoryService>();
services.AddScoped<IAnalyticsService, AnalyticsService>();

services.AddScoped<ConsoleMenu>();

var provider = services.BuildServiceProvider();

using (var scope = provider.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

using (var scope = provider.CreateScope())
{
    var menu = scope.ServiceProvider.GetRequiredService<ConsoleMenu>();
    menu.Run();
}
