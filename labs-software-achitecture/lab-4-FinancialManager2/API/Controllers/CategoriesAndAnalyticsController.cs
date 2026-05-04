using AutoMapper;
using FinancialManager.API.Models;
using FinancialManager.BLL.DTOs;
using FinancialManager.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FinancialManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        // GET /api/categories
        [HttpGet]
        public IActionResult GetAll()
        {
            var dtos = _categoryService.GetAll();
            var models = _mapper.Map<IEnumerable<CategoryModel>>(dtos);
            return Ok(models);
        }

        // GET /api/categories/by-type/Income
        // або
        // GET /api/categories/by-type/Expense
        [HttpGet("by-type/{type}")]
        public IActionResult GetByType(string type)
        {
            var dtos = _categoryService.GetByType(type);
            var models = _mapper.Map<IEnumerable<CategoryModel>>(dtos);
            return Ok(models);
        }

        // POST /api/categories
        [HttpPost]
        public IActionResult Create([FromBody] CreateCategoryModel model)
        {
            try
            {
                var dto = _mapper.Map<CategoryDTO>(model);
                _categoryService.Create(dto);
                return StatusCode(201, new { message = "Категорію створено." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly IMapper _mapper;

        public AnalyticsController(IAnalyticsService analyticsService, IMapper mapper)
        {
            _analyticsService = analyticsService;
            _mapper = mapper;
        }

        // GET /api/analytics/incomes-by-category
        // Повертає суму доходів по кожній категорії
        [HttpGet("incomes-by-category")]
        public IActionResult GetIncomeByCategory()
        {
            var dtos = _analyticsService.GetIncomeByCategory();
            var models = _mapper.Map<IEnumerable<CategorySummaryModel>>(dtos);
            return Ok(models);
        }

        // GET /api/analytics/expenses-by-category
        [HttpGet("expenses-by-category")]
        public IActionResult GetExpenseByCategory()
        {
            var dtos = _analyticsService.GetExpenseByCategory();
            var models = _mapper.Map<IEnumerable<CategorySummaryModel>>(dtos);
            return Ok(models);
        }

        // GET /api/analytics/summary-by-account
        // Повертає зведення по кожному рахунку
        [HttpGet("summary-by-account")]
        public IActionResult GetSummaryByAccount()
        {
            var dtos = _analyticsService.GetSummaryByAccount();
            var models = _mapper.Map<IEnumerable<AccountSummaryModel>>(dtos);
            return Ok(models);
        }
    }
}
