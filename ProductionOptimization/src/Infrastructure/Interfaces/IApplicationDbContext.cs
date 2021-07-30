using Domain.Entities;
using Domain.Entities.ModelComponents;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<SystemAnalysisModel> SystemAnalysisModels { get; set; }
        public DbSet<ModelBackground> ModelBackgrounds { get; set; }
        public DbSet<PVT> PVTs { get; set; }
        public DbSet<ParamEntry> ParamEntries { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
