using Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            if (!context.SystemAnalysisModels.Any())
            {
                context.SystemAnalysisModels.Add(new SystemAnalysisModel()
                {
                    ModelDate = DateTime.Now,
                    DrainagePointName = "The Drainage Point is AB1000X",
                    ModelBackground = new Domain.Entities.ModelComponents.ModelBackground(),
                    PVT = new Domain.Entities.ModelComponents.PVT(),
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
