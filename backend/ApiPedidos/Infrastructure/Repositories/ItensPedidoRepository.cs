using Domain.Entities;
using Domain.Respositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ItensPedidoRepository : IItensPedidoRepository
    {
        private readonly DataBaseContext _context;

        public ItensPedidoRepository(DataBaseContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(ItensPedido itesnPedido)
        {
            try
            {
                await _context.ItensPedido.AddAsync(itesnPedido);

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
                var itensPedido = await _context.ItensPedido.FirstOrDefaultAsync(x => x.Id == id);

                _context.ItensPedido.Remove(itensPedido);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ItensPedido>> GetAll()
        {
            try
            {
                return await _context.ItensPedido.AsNoTracking().OrderBy(x => x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ItensPedido> GetById(int id)
        {
            try
            {
                return await _context.ItensPedido.FirstAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> Update(ItensPedido itensPedido)
        {
            try
            {
                _context.Update(itensPedido);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
