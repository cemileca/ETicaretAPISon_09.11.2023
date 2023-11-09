using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.FileEntities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories
{
    public class InvoiceImageFileReadRepository : ReadRepository<InvoiceImageFile>, IInvoiceImageFileReadRepository
    {
        public InvoiceImageFileReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
