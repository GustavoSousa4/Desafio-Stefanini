
using Application.Produto.Model.Response;
using Application.Produto.Service;
using Domain.Entities;
using Domain.Respositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesTest
{
    public class ProdutoServiceTest
    {
        private readonly Mock<IProdutoRepository> _repositoryMock;
        private readonly ProdutoService _service;

        public ProdutoServiceTest(ProdutoService service, Mock<IProdutoRepository> repositoryMock)
        {
            _service = service;
            _repositoryMock = repositoryMock;
        }

        [Fact]
        public async void GetProductsById_ReturnProducts()
        {
            //Arrange
            var product = new Produto("AXD", 1);
            var productDto = new ProdutoResponseDto
            {
                Id = 1,
                NomeProduto = "AXD",
                Valor = 1
            };
            _repositoryMock.Setup(c => c.Create(It.IsAny<Produto>())).ReturnsAsync(true);
            _repositoryMock.Setup(c => c.GetById(productDto.Id)).ReturnsAsync(product);

            //Act
            var result = await _service.GetProductById(productDto.Id);
            //Assert
            Assert.Equal(productDto.NomeProduto, result.NomeProduto);
            Assert.Equal(productDto.Valor, result.Valor);
        }
    }
}
