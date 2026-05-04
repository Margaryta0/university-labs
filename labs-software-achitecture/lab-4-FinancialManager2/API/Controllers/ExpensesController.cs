using System;
using System.Collections.Generic;
using AutoMapper;
using FinancialManager.API.Models;
using FinancialManager.BLL.DTOs;
using FinancialManager.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IMapper _mapper;

        public ExpensesController(IExpenseService expenseService, IMapper mapper)
        {
            _expenseService = expenseService;
            _mapper = mapper;
        }

        // GET /api/expenses
        [HttpGet]
        public IActionResult GetAll()
        {
            var dtos = _expenseService.GetAll();
            var models = _mapper.Map<IEnumerable<ExpenseModel>>(dtos);
            return Ok(models);
        }

        // GET /api/expenses/by-account/1
        [HttpGet("by-account/{accountId}")]
        public IActionResult GetByAccount(int accountId)
        {
            var dtos = _expenseService.GetByAccount(accountId);
            var models = _mapper.Map<IEnumerable<ExpenseModel>>(dtos);
            return Ok(models);
        }

        // GET /api/expenses/by-category/2
        [HttpGet("by-category/{categoryId}")]
        public IActionResult GetByCategory(int categoryId)
        {
            var dtos = _expenseService.GetByCategory(categoryId);
            var models = _mapper.Map<IEnumerable<ExpenseModel>>(dtos);
            return Ok(models);
        }

        // POST /api/expenses
        [HttpPost]
        public IActionResult Create([FromBody] CreateExpenseModel model)
        {
            try
            {
                var dto = _mapper.Map<ExpenseDTO>(model);
                _expenseService.Create(dto);
                return StatusCode(201, new { message = "Витрату додано." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE /api/expenses/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _expenseService.Delete(id);
                return Ok(new { message = "Витрату видалено." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
