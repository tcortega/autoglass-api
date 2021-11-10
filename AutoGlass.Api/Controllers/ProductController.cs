using AutoGlass.Application.Dtos;
using AutoGlass.Application.Interfaces;
using AutoGlass.Application.Validators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        public ActionResult<ProductDto> Get(int id)
        {
            try
            {
                return Ok(_productApplicationService.Get(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] ProductDto dto)
        {
            if (dto is null)
                return BadRequest();

            try
            {
                _productApplicationService.Add<ProductDtoValidator>(dto);
                return Ok("Produto cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult Put([FromBody] ProductDto dto)
        {
            try
            {
                if (dto is null)
                    return NotFound();

                _productApplicationService.Update<ProductDtoValidator>(dto);
                return Ok("Dados do Produto atualizados com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return NotFound();

                _productApplicationService.Remove(id);
                return Ok("Produto deletado com sucesso");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}