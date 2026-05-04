# UML Діаграми — Фінансовий Менеджер

---

## 1. Загальна архітектура

```mermaid
graph TD
    UI["UI Layer\nFinancialManager.UI\nConsoleMenu | InputHelper | DisplayHelper"]
    API["API Layer\nFinancialManager.API\nControllers | Models | ApiMappingProfile"]
    BLL["BLL Layer\nFinancialManager.BLL\nServices | DTOs | MappingProfile"]
    DAL["DAL Layer\nFinancialManager.DAL\nRepository | UnitOfWork | AppDbContext | Entities"]
    DB[(SQLite\nfinancial.db)]
    CLIENT["Клієнт\nSwagger / Postman / Браузер"]
 
    CLIENT -->|HTTP запити| API
    UI -->|використовує інтерфейси BLL| BLL
    API -->|використовує інтерфейси BLL| BLL
    BLL -->|використовує IUnitOfWork| DAL
    DAL -->|EF Core| DB
```
 
---

## 2. Залежності між проектами

```mermaid
graph LR
    UI[FinancialManager.UI]
    API[FinancialManager.API]
    BLL[FinancialManager.BLL]
    DAL[FinancialManager.DAL]
 
    UI --> BLL
    UI --> DAL
    API --> BLL
    API --> DAL
    BLL --> DAL
```
 
---

## 3. Діаграма класів — DAL Entities

```mermaid
classDiagram
    class Account {
        +int Id
        +string Name
        +decimal Balance
        +List~Income~ Incomes
        +List~Expense~ Expenses
    }

    class Income {
        +int Id
        +decimal Amount
        +DateTime Date
        +string Description
        +int AccountId
        +int CategoryId
        +Account Account
        +Category Category
    }

    class Expense {
        +int Id
        +decimal Amount
        +DateTime Date
        +string Description
        +int AccountId
        +int CategoryId
        +Account Account
        +Category Category
    }

    class Category {
        +int Id
        +string Name
        +CategoryType Type
        +List~Income~ Incomes
        +List~Expense~ Expenses
    }

    class CategoryType {
        <<enumeration>>
        Income
        Expense
    }

    Account "1" --> "*" Income : має
    Account "1" --> "*" Expense : має
    Category "1" --> "*" Income : має
    Category "1" --> "*" Expense : має
    Category --> CategoryType : використовує
```

---

## 4. Діаграма класів — DAL Repository та UnitOfWork

```mermaid
classDiagram
    class IRepository~T~ {
        <<interface>>
        +GetAll() IEnumerable~T~
        +GetAllWithIncludes() IEnumerable~T~
        +GetById(int id) T
        +Add(T entity) void
        +Update(T entity) void
        +Delete(int id) void
    }

    class Repository~T~ {
        -AppDbContext _context
        -DbSet~T~ _dbSet
        +GetAll() IEnumerable~T~
        +GetAllWithIncludes() IEnumerable~T~
        +GetById(int id) T
        +Add(T entity) void
        +Update(T entity) void
        +Delete(int id) void
    }

    class IUnitOfWork {
        <<interface>>
        +IRepository~Account~ Accounts
        +IRepository~Income~ Incomes
        +IRepository~Expense~ Expenses
        +IRepository~Category~ Categories
        +Save() void
        +Dispose() void
    }

    class UnitOfWork {
        -AppDbContext _context
        -IRepository~Account~ _accounts
        -IRepository~Income~ _incomes
        -IRepository~Expense~ _expenses
        -IRepository~Category~ _categories
        +IRepository~Account~ Accounts
        +IRepository~Income~ Incomes
        +IRepository~Expense~ Expenses
        +IRepository~Category~ Categories
        +Save() void
        +Dispose() void
    }

    class AppDbContext {
        +DbSet~Account~ Accounts
        +DbSet~Income~ Incomes
        +DbSet~Expense~ Expenses
        +DbSet~Category~ Categories
        +OnModelCreating() void
    }

    IRepository~T~ <|.. Repository~T~ : реалізує
    IUnitOfWork <|.. UnitOfWork : реалізує
    Repository~T~ --> AppDbContext : використовує
    UnitOfWork --> AppDbContext : використовує
    UnitOfWork --> IRepository~T~ : містить
```

---

## 5. Діаграма класів — BLL Інтерфейси та Сервіси

```mermaid
classDiagram
    class IAccountService {
        <<interface>>
        +GetAll() IEnumerable~AccountDTO~
        +GetById(int id) AccountDTO
        +Create(AccountDTO dto) void
        +Update(AccountDTO dto) void
        +Delete(int id) void
    }

    class IIncomeService {
        <<interface>>
        +GetAll() IEnumerable~IncomeDTO~
        +GetByAccount(int id) IEnumerable~IncomeDTO~
        +GetByCategory(int id) IEnumerable~IncomeDTO~
        +Create(IncomeDTO dto) void
        +Delete(int id) void
    }

    class IExpenseService {
        <<interface>>
        +GetAll() IEnumerable~ExpenseDTO~
        +GetByAccount(int id) IEnumerable~ExpenseDTO~
        +GetByCategory(int id) IEnumerable~ExpenseDTO~
        +Create(ExpenseDTO dto) void
        +Delete(int id) void
    }

    class ICategoryService {
        <<interface>>
        +GetAll() IEnumerable~CategoryDTO~
        +GetByType(string type) IEnumerable~CategoryDTO~
        +Create(CategoryDTO dto) void
    }

    class IAnalyticsService {
        <<interface>>
        +GetIncomeByCategory() IEnumerable~CategorySummaryDTO~
        +GetExpenseByCategory() IEnumerable~CategorySummaryDTO~
        +GetSummaryByAccount() IEnumerable~AccountSummaryDTO~
    }

    class AccountService {
        -IUnitOfWork _uow
        -IMapper _mapper
        +GetAll() IEnumerable~AccountDTO~
        +GetById(int id) AccountDTO
        +Create(AccountDTO dto) void
        +Update(AccountDTO dto) void
        +Delete(int id) void
    }

    class IncomeService {
        -IUnitOfWork _uow
        -IMapper _mapper
        +GetAll() IEnumerable~IncomeDTO~
        +GetByAccount(int id) IEnumerable~IncomeDTO~
        +GetByCategory(int id) IEnumerable~IncomeDTO~
        +Create(IncomeDTO dto) void
        +Delete(int id) void
    }

    class ExpenseService {
        -IUnitOfWork _uow
        -IMapper _mapper
        +GetAll() IEnumerable~ExpenseDTO~
        +GetByAccount(int id) IEnumerable~ExpenseDTO~
        +GetByCategory(int id) IEnumerable~ExpenseDTO~
        +Create(ExpenseDTO dto) void
        +Delete(int id) void
    }

    class CategoryService {
        -IUnitOfWork _uow
        -IMapper _mapper
        +GetAll() IEnumerable~CategoryDTO~
        +GetByType(string type) IEnumerable~CategoryDTO~
        +Create(CategoryDTO dto) void
    }

    class AnalyticsService {
        -IUnitOfWork _uow
        +GetIncomeByCategory() IEnumerable~CategorySummaryDTO~
        +GetExpenseByCategory() IEnumerable~CategorySummaryDTO~
        +GetSummaryByAccount() IEnumerable~AccountSummaryDTO~
    }

    IAccountService <|.. AccountService : реалізує
    IIncomeService <|.. IncomeService : реалізує
    IExpenseService <|.. ExpenseService : реалізує
    ICategoryService <|.. CategoryService : реалізує
    IAnalyticsService <|.. AnalyticsService : реалізує
```

---

## 6. Діаграма класів — BLL DTOs

```mermaid
classDiagram
    class AccountDTO {
        +int Id
        +string Name
        +decimal Balance
    }

    class IncomeDTO {
        +int Id
        +decimal Amount
        +DateTime Date
        +string Description
        +int AccountId
        +int CategoryId
        +string AccountName
        +string CategoryName
    }

    class ExpenseDTO {
        +int Id
        +decimal Amount
        +DateTime Date
        +string Description
        +int AccountId
        +int CategoryId
        +string AccountName
        +string CategoryName
    }

    class CategoryDTO {
        +int Id
        +string Name
        +string Type
    }

    class CategorySummaryDTO {
        +string CategoryName
        +decimal TotalAmount
    }

    class AccountSummaryDTO {
        +string AccountName
        +decimal TotalIncome
        +decimal TotalExpense
        +decimal Balance
    }
```

---

## 7. Діаграма класів — UI

```mermaid
classDiagram
    class Program {
        +Main() void
        Конфігурує DI контейнер
        Запускає ConsoleMenu
    }

    class ConsoleMenu {
        -IAccountService _accountService
        -IIncomeService _incomeService
        -IExpenseService _expenseService
        -ICategoryService _categoryService
        -IAnalyticsService _analyticsService
        +Run() void
        -AccountsMenu() void
        -IncomesMenu() void
        -ExpensesMenu() void
        -CategoriesMenu() void
        -AnalyticsMenu() void
    }

    class InputHelper {
        <<static>>
        +ReadInt(string prompt) int
        +ReadDecimal(string prompt) decimal
        +ReadDate(string prompt) DateTime
        +ReadString(string prompt) string
    }

    class DisplayHelper {
        <<static>>
        +PrintAccounts() void
        +PrintIncomes() void
        +PrintExpenses() void
        +PrintCategories() void
        +PrintIncomeSummary() void
        +PrintExpenseSummary() void
        +PrintAccountSummary() void
        +PrintError(string message) void
        +PrintSuccess(string message) void
    }

    Program --> ConsoleMenu : створює
    ConsoleMenu --> InputHelper : використовує
    ConsoleMenu --> DisplayHelper : використовує
```

---

## 8. Діаграма класів — API Models
 
```mermaid
classDiagram
    class AccountModel {
        +int Id
        +string Name
        +decimal Balance
    }
 
    class CreateAccountModel {
        +string Name
        +decimal Balance
    }
 
    class IncomeModel {
        +int Id
        +decimal Amount
        +DateTime Date
        +string Description
        +string AccountName
        +string CategoryName
        +int AccountId
        +int CategoryId
    }
 
    class CreateIncomeModel {
        +decimal Amount
        +DateTime Date
        +string Description
        +int AccountId
        +int CategoryId
    }
 
    class ExpenseModel {
        +int Id
        +decimal Amount
        +DateTime Date
        +string Description
        +string AccountName
        +string CategoryName
        +int AccountId
        +int CategoryId
    }
 
    class CreateExpenseModel {
        +decimal Amount
        +DateTime Date
        +string Description
        +int AccountId
        +int CategoryId
    }
 
    class CategoryModel {
        +int Id
        +string Name
        +string Type
    }
 
    class CreateCategoryModel {
        +string Name
        +string Type
    }
 
    class CategorySummaryModel {
        +string CategoryName
        +decimal TotalAmount
    }
 
    class AccountSummaryModel {
        +string AccountName
        +decimal TotalIncome
        +decimal TotalExpense
        +decimal Balance
    }
```
 
---
 
## 9. Діаграма класів — API Controllers
 
```mermaid
classDiagram
    class AccountsController {
        -IAccountService _accountService
        -IMapper _mapper
        +GetAll() IActionResult
        +GetById(int id) IActionResult
        +Create(CreateAccountModel) IActionResult
        +Update(int id, CreateAccountModel) IActionResult
        +Delete(int id) IActionResult
    }
 
    class IncomesController {
        -IIncomeService _incomeService
        -IMapper _mapper
        +GetAll() IActionResult
        +GetByAccount(int accountId) IActionResult
        +GetByCategory(int categoryId) IActionResult
        +Create(CreateIncomeModel) IActionResult
        +Delete(int id) IActionResult
    }
 
    class ExpensesController {
        -IExpenseService _expenseService
        -IMapper _mapper
        +GetAll() IActionResult
        +GetByAccount(int accountId) IActionResult
        +GetByCategory(int categoryId) IActionResult
        +Create(CreateExpenseModel) IActionResult
        +Delete(int id) IActionResult
    }
 
    class CategoriesController {
        -ICategoryService _categoryService
        -IMapper _mapper
        +GetAll() IActionResult
        +GetByType(string type) IActionResult
        +Create(CreateCategoryModel) IActionResult
    }
 
    class AnalyticsController {
        -IAnalyticsService _analyticsService
        -IMapper _mapper
        +GetIncomeByCategory() IActionResult
        +GetExpenseByCategory() IActionResult
        +GetSummaryByAccount() IActionResult
    }
 
    AccountsController --> IAccountService : використовує
    IncomesController --> IIncomeService : використовує
    ExpensesController --> IExpenseService : використовує
    CategoriesController --> ICategoryService : використовує
    AnalyticsController --> IAnalyticsService : використовує
```
 
---

## 10. HTTP методи та endpoints
 
```mermaid
graph LR
    subgraph Accounts
        A1[GET /api/accounts]
        A2[GET /api/accounts/id]
        A3[POST /api/accounts]
        A4[PUT /api/accounts/id]
        A5[DELETE /api/accounts/id]
    end
 
    subgraph Incomes
        I1[GET /api/incomes]
        I2[GET /api/incomes/by-account/id]
        I3[GET /api/incomes/by-category/id]
        I4[POST /api/incomes]
        I5[DELETE /api/incomes/id]
    end
 
    subgraph Expenses
        E1[GET /api/expenses]
        E2[GET /api/expenses/by-account/id]
        E3[GET /api/expenses/by-category/id]
        E4[POST /api/expenses]
        E5[DELETE /api/expenses/id]
    end
 
    subgraph Categories
        C1[GET /api/categories]
        C2[GET /api/categories/by-type/type]
        C3[POST /api/categories]
    end
 
    subgraph Analytics
        AN1[GET /api/analytics/incomes-by-category]
        AN2[GET /api/analytics/expenses-by-category]
        AN3[GET /api/analytics/summary-by-account]
    end
```
