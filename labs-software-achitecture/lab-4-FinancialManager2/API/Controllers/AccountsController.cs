using System;
using AutoMapper;
using FinancialManager.API.Models;
using FinancialManager.BLL.DTOs;
using FinancialManager.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FinancialManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var dtos = _accountService.GetAll();
            var models = _mapper.Map<IEnumerable<AccountModel>>(dtos);
            return Ok(models); // HTTP 200 + JSON
        }

        // GET /api/accounts/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var dto = _accountService.GetById(id);
                var model = _mapper.Map<AccountModel>(dto);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // POST /api/accounts
        // Створює новий рахунок
        [HttpPost]
        public IActionResult Create([FromBody] CreateAccountModel model)
        {
            try
            {
                var dto = _mapper.Map<AccountDTO>(model);
                _accountService.Create(dto);
                return StatusCode(201, new { message = "Рахунок створено." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT /api/accounts/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CreateAccountModel model)
        {
            try
            {
                var dto = _mapper.Map<AccountDTO>(model);
                dto.Id = id; 
                _accountService.Update(dto);
                return Ok(new { message = "Рахунок оновлено." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE /api/accounts/1
        // Видаляє рахунок по Id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _accountService.Delete(id);
                return Ok(new { message = "Рахунок видалено." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
