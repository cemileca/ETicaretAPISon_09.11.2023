using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.FileEntities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories
{
    public class InvocieImageFileWriteRepository : WriteRepository<InvoiceImageFile>, IInvoiceImageFileWriteRepository
    {
        public InvocieImageFileWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
