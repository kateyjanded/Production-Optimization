using Application.PVT.Interfaces;
using Domain.Entities.ModelComponents;
using Infrastructure.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository.EntityFrameWork_Repository
{
    public class PVTRepository : IPVTRepository
    {
        private readonly IApplicationDbContext context;

        public PVTRepository(IApplicationDbContext _context)
        {
            context = _context;
        }
        public void Delete(PVT entity)
        {
            context.PVTs.Remove(entity);
        }

        public Task DeleteAsync(PVT entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PVT> GetAll()
        {
            return context.PVTs;
        }

        public async Task<PVT> GetById(Guid Id)
        {
            return await context.PVTs.FindAsync(Id);
        }

        public void Save(PVT entity)
        {
            context.PVTs.Add(entity);
        }

        public Task SaveAsync(PVT entity)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        public void Update(PVT entity)
        {
            context.PVTs.Update(entity);
        }
    }
}
