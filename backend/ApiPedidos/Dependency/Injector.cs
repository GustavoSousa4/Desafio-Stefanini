using Application.Pedido.Service;
using Application.Produto.Service;
using Domain.Respositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dependency
{
    public static class Injector
    {
        public static void AddLocalServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Repository
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddTransient<IItensPedidoRepository, ItensPedidoRepository>();
            #endregion

            #region Service
            services.AddTransient<IProdutoService, ProdutoService>();
            services.AddTransient<IPedidoService, PedidoService>();
            #endregion
        }
    }
}
