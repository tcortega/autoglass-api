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
    public class ProductController : ControllerBase
    {
        private readonly IProductApplicationService _productApplicationService;

        public ProductController(IProductApplicationService productApplicationService)
        {
            _productApplicationService = productApplicationService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> Get()
        {
            return Ok(_productApplicationService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            try
            {
                return Ok(await _productApplicationService.Get(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDto dto)
        {
            if (dto is null)
                return BadRequest();

            try
            {
                await _productApplicationService.Add<ProductDtoValidator>(dto);
                return Ok("Produto cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ProductDto dto)
        {
            try
            {
                if (dto is null)
                    return NotFound();

                await _productApplicationService.Update<ProductDtoValidator>(dto);
                return Ok("Dados do Produto atualizados com sucesso!");
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

                await _productApplicationService.Remove(id);
                return Ok("Produto deletado com sucesso");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}