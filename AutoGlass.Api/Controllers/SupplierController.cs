using AutoGlass.Application.Dtos;
using AutoGlass.Application.Interfaces;
using AutoGlass.Application.Validators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoGlass.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierApplicationService _supplierApplicationService;

        public SupplierController(ISupplierApplicationService supplierApplicationService)
        {
            _supplierApplicationService = supplierApplicationService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SupplierDto>> Get()
        {
            return Ok(_supplierApplicationService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDto>> Get(int id)
        {
            try
            {
                return Ok(await _supplierApplicationService.Get(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SupplierDto dto)
        {
            if (dto is null)
                return BadRequest();

            try
            {
                await _supplierApplicationService.Add<SupplierDtoValidator>(dto);
                return Ok("Fornecedor cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] SupplierDto dto)
        {
            try
            {
                if (dto is null)
                    return NotFound();

                await _supplierApplicationService.Update<SupplierDtoValidator>(dto);
                return Ok("Dados do fornecedor atualizados com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return NotFound();

                await _supplierApplicationService.Remove(id);
                return Ok("Fornecedor deletado com sucesso");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}