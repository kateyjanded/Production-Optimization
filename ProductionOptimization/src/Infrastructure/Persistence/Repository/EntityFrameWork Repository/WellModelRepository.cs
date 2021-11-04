using Application.SystemAnalysisModels.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repository.EntityFrameWork_Repository
{
    public class WellModelRepository : IWellModelRepository
    {
        private IApplicationDbContext _context;
        public WellModelRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public void Delete(SystemAnalysisModel entity)
        {
            _context.SystemAnalysisModels.Remove(entity);
        }
        public Task DeleteAsync(SystemAnalysisModel entity)
        {
            throw new NotImplementedException();
        }
        public IQueryable<SystemAnalysisModel> GetAll()
        {
            return _context.SystemAnalysisModels;
        }
        public async Task<SystemAnalysisModel> GetById(Guid Id)
        {
            var entity = _context.SystemAnalysisModels
                .Include(x=>x.ModelBackground)
                .Include(x=>x.PVT)
                .Include(x=>x.PVT.GasRatio)
                .Include(x=>x.PVT.Pressure)
                .Include(x=>x.PVT.Temperature)
                .Include(x=>x.PVT.WaterSalinity)
                .Include(x=>x.IPR)
                .Include(x => x.IPR.ReservoirPressure)
                .Include(x => x.IPR.ReservoirTemperature)
                .Include(x => x.IPR.GasFraction)
                .Include(x => x.IPR.WaterFraction)
                .Include(x => x.IPR.ProductivityIndex)
                .Include(x => x.VLP)
                .Include(x => x.VLP.THP)
                .Include(x => x.VLP.GasFraction)
                .Include(x => x.VLP.WaterFraction)
                .Include(x => x.VLP.GasLiftFraction)
                .Where(x => x.Id == Id);
                return await entity.FirstOrDefaultAsync();
        }
        public SystemAnalysisModel GetWellModelByName(string drainagePoint)
        {
            return _context.SystemAnalysisModels.Where(x => x.DrainagePointName == drainagePoint).FirstOrDefault();
        }
        public void Save(SystemAnalysisModel entity)
        {
            _context.SystemAnalysisModels.Add(entity);
        }
        public async Task SaveAsync(SystemAnalysisModel entity)
        {
            await _context.SystemAnalysisModels.AddAsync(entity);
        }
        public void Update(SystemAnalysisModel entity)
        {
            _context.SystemAnalysisModels.Update(entity);
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
