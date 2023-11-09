using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Persistence.Contexts;
using ETicaretAPI.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection service)
        {
            service.AddDbContext<ETicaretAPIDbContext>(option => option.UseNpgsql(Configurations.ConnectionString));


            service.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            service.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

            service.AddScoped<IOrderReadRepositoty, OrderReadRepository>();
            service.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            service.AddScoped<IProductReadRepository, ProductReadRepository>();
            service.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            service.AddScoped<IBaseFileEntityReadRepository, BaseFileEntityReadRepository>();
            service.AddScoped<IBaseFileEntityWriteRepository, BaseFileEntityWriteRepository>();

            service.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            service.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

            service.AddScoped<IInvoiceImageFileReadRepository, InvoiceImageFileReadRepository>();
            service.AddScoped<IInvoiceImageFileWriteRepository, InvocieImageFileWriteRepository>();





        }
    }
}
