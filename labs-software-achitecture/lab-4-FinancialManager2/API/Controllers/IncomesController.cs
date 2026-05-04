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
    public class IncomesController : ControllerBase
    {
        private readonly IIncomeService _incomeService;
        private readonly IMapper _mapper;

        public IncomesController(IIncomeService incomeService, IMapper mapper)
        {
            _incomeService = incomeService;
            _mapper = mapper;
        }

        // GET /api/incomes
        [HttpGet]
        public IActionResult GetAll()
        {
            var dtos = _incomeService.GetAll();
            var models = _mapper.Map<IEnumerable<IncomeModel>>(dtos);
            return Ok(models);
        }

        // GET /api/incomes/by-account/1
        [HttpGet("by-account/{accountId}")]
        public IActionResult GetByAccount(int accountId)
        {
            var dtos = _incomeService.GetByAccount(accountId);
            var models = _mapper.Map<IEnumerable<IncomeModel>>(dtos);
            return Ok(models);
        }

        // GET /api/incomes/by-category/2
        [HttpGet("by-category/{categoryId}")]
        public IActionResult GetByCategory(int categoryId)
        {
            var dtos = _incomeService.GetByCategory(categoryId);
            var models = _mapper.Map<IEnumerable<IncomeModel>>(dtos);
            return Ok(models);
        }

        // POST /api/incomes
        [HttpPost]
        public IActionResult Create([FromBody] CreateIncomeModel model)
        {
            try
            {
                var dto = _mapper.Map<IncomeDTO>(model);
                _incomeService.Create(dto);
                return StatusCode(201, new { message = "Дохід додано." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE /api/incomes/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _incomeService.Delete(id);
                return Ok(new { message = "Дохід видалено." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
