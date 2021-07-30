using Application.ModelBackGround.Interfaces;
using Domain.Entities.ModelComponents;
using Infrastructure.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repository.EntityFrameWork_Repository
{
    public class ModelBackgroundRepository: IModelBackgroundRepository
    {
        private IApplicationDbContext _context;
        public ModelBackgroundRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public void Delete(ModelBackground entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ModelBackground entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ModelBackground> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ModelBackground> GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void Save(ModelBackground entity)
        {
            _context.ModelBackgrounds.Add(entity);
        }

        public async Task SaveAsync(ModelBackground entity)
        {
            await _context.ModelBackgrounds.AddAsync(entity);
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        public void Update(ModelBackground entity)
        {
             _context.ModelBackgrounds.Update(entity);
        }
    }
}
