using Domain.Entities;
using Domain.Respositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly DataBaseContext _context;

        public PedidoRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Pedido pedido)
        {
            try
            {
                await _context.Pedido.AddAsync(pedido);

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
                var pedido = await _context.Pedido.FirstOrDefaultAsync(x => x.Id == id);

                _context.Pedido.Remove(pedido);

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Pedido>> GetAll()
        {
            try
            {
                return await _context.Pedido.Include(x => x.ItensPedido).ThenInclude(c => c.Produto).AsNoTracking().OrderBy(x => x.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Pedido> GetById(int id)
        {
            try
            {
                return await _context.Pedido.Include(x =>x.ItensPedido).ThenInclude(c => c.Produto).AsNoTracking().FirstAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(Pedido pedido)
        {
            try
            {
                _context.Update(pedido);

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
