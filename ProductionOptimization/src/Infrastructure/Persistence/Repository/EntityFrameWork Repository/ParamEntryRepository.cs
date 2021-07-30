using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repository.EntityFrameWork_Repository
{
    public class ParamEntryRepository : IParamEntryRepository
    {
        private readonly IApplicationDbContext context;

        public ParamEntryRepository(IApplicationDbContext _context)
        {
            context = _context;
        }
        public void Delete(ParamEntry entity)
        {
            context.ParamEntries.Remove(entity);
        }

        public Task DeleteAsync(ParamEntry entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ParamEntry> GetAll()
        {
            return context.ParamEntries;
        }

        public async Task<ParamEntry> GetById(Guid Id)
        {
            return await context.ParamEntries.FindAsync(Id);
        }

        public async Task<ParamEntry> GetByName(string name)
        {
            return await context.ParamEntries.FindAsync(name);
        }

        public void Save(ParamEntry entity)
        {
            context.ParamEntries.Add(entity);
        }

        public void SaveAll(IList<ParamEntry> parameters)
        {
            context.ParamEntries.AddRange(parameters);
        }

        public async Task SaveAsync(ParamEntry entity)
        {
            await context.ParamEntries.AddAsync(entity);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        public void Update(ParamEntry entity)
        {
            context.ParamEntries.Update(entity);
        }

        public void UpdateAll(IList<ParamEntry> paramEntries)
        {
            throw new NotImplementedException();
        }
    }
}
