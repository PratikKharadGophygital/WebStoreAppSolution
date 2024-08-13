using Infrastructure.Data;
using Infrastructure.Repositories;
using WebStoreApp.Application.Interfaces;
using WebStoreApp.Application.Services;
using WebStoreApp.Domain.Interfaces;

namespace WebStoreApp.Extension
{
    public static class ServiceExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Add services to the container 


            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // Customer service 
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            #endregion

            // Register database as service
            services.AddSingleton<DapperDbContext>();
         


            return services;
        }
    }
}
