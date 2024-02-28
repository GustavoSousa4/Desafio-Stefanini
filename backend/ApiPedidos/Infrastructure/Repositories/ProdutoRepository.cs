using Domain.Entities;
using Domain.Respositories;
using Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DataBaseContext _context;

        public ProdutoRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Produto produto)
        {
            try
            {
                await _context.Produto.AddAsync(produto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var produto = await _context.Produto.FirstOrDefaultAsync(x => x.Id == id);

                _context.Produto.Remove(produto);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(Produto produto)
        {
            try
            {
                _context.Produto.Update(produto);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<Produto>> GetAll()
        {
            try
            {
                return await _context.Produto.AsNoTracking().OrderBy(x => x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Produto> GetById(int id)
        {
            try
            {
                return await _context.Produto.FirstAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Produto> GetByName(string name)
        {
            try
            {
                return await _context.Produto.FirstAsync(x => x.NomeProduto == name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<decimal> GetPriceProduct(int id)
        {
            try
            {
                var produto = await GetById(id);
                return produto.Valor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
