﻿using Application.Produto.Model.Request;
using Application.Produto.Model.Response;
using Application.Produto.Service;
using Domain.Entities;
using Domain.Respositories;
using Infrastructure;
using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace ApiPedidos.Controllers
{
    [ApiController]
    [Route("Produto")]
    public class ProdutoController : ControllerBase
    {
     
        private readonly IProdutoService _service;
        public ProdutoController(IProdutoService produtoService)
        {
            _service = produtoService;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<ProdutoResponseDto>> GetAll() { return await _service.GetAllProducts(); }

        [HttpGet("GetById/{id}")]
        public async Task<ProdutoResponseDto> GetById([FromRoute] int id)
        {
            return await _service.GetProductById(id);
        }
        
        [HttpGet("GetByName/{nomeProduto}")]
        public async Task<ProdutoResponseDto> GetByname([FromRoute] string nomeProduto)
        {
            return await _service.GetProductByName(nomeProduto);
        }
        [HttpGet("GetPrice/{id}")]
        public async Task<decimal> GetPricee([FromRoute] int id)
        {
            return await _service.GetProductPrice(id);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ProdutoRequestDto produto)
        {
            if(await _service.CreateProduct(produto))
                return Ok("Criado com sucesso!");
            return BadRequest("Erro ao criar produto");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] ProdutoRequestDto produto)
        {
           if(await _service.UpdateProduct(id, produto))
                return Ok("Alterado com sucesso!");
            return BadRequest("Erro ao atualizar produto");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
           if(await _service.DeleteProduct(id))
                return Ok("Excluido com sucesso!");
            return BadRequest("Erro ao excluir produto");
        }
    }
}
