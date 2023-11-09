using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.FileEntities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories
{
    public class BaseFileEntityReadRepository : ReadRepository<BaseFileEntity>,IBaseFileEntityReadRepository
    {
        public BaseFileEntityReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
