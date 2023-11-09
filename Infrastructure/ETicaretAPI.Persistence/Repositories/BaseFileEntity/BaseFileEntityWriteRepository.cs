using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.FileEntities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories
{
    public class BaseFileEntityWriteRepository : WriteRepository<BaseFileEntity>, IBaseFileEntityWriteRepository
    {
        public BaseFileEntityWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
