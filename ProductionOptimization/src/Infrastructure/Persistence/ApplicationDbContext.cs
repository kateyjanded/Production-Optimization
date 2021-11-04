using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.ModelComponents;
using IdentityServer4.EntityFramework.Options;
using Infrastructure.Identity;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
    {
        IDateTime _dateTime;
        ICurrentUserService _currentUserService;
        IDomainEventService _domainEventService;
        public ApplicationDbContext(DbContextOptions options, 
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            IDateTime dateTime, ICurrentUserService currentUserService,
            IDomainEventService domainEventService): base(options, operationalStoreOptions)
        {
            _dateTime = dateTime;
            _currentUserService = currentUserService;
            _domainEventService = domainEventService;
        }

        public DbSet<SystemAnalysisModel> SystemAnalysisModels { get; set; }
        public DbSet<ModelBackground> ModelBackgrounds { get; set; }
        public DbSet<PVT> PVTs { get; set; }
        public DbSet<IPR> IPRs { get; set; }
        public DbSet<IPR> VLPs { get; set; }
        public DbSet<ParamEntry> ParamEntries { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    default:
                        break;
                }
            }
            var result =  await base.SaveChangesAsync(cancellationToken);
            await DispatchEvent();
            return result;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IPR>().Property(e => e.Pressures).
                HasConversion(new ValueConverter<double[], string>(
                    x => string.Join(";", x),
                    x => x.Split(";", StringSplitOptions.RemoveEmptyEntries)
                    .Select(val => double.Parse(val)).ToArray()), 
                    new ValueComparer<double[]>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToArray()));
            builder.Entity<IPR>().Property(e => e.Rates).
                HasConversion(new ValueConverter<double[], string>(
                    x => string.Join(";", x),
                    x => x.Split(";", StringSplitOptions.RemoveEmptyEntries)
                    .Select(val => double.Parse(val)).ToArray()),
                     new ValueComparer<double[]>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToArray()));
            builder.Entity<VLP>().Property(e => e.Pressures).
                HasConversion(new ValueConverter<double[], string>(
                    x => string.Join(";", x),
                    x => x.Split(";", StringSplitOptions.RemoveEmptyEntries)
                    .Select(val => double.Parse(val)).ToArray()),
                    new ValueComparer<double[]>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToArray()));
            builder.Entity<VLP>().Property(e => e.Rates).
                HasConversion(new ValueConverter<double[], string>(
                    x => string.Join(";", x),
                    x => x.Split(";", StringSplitOptions.RemoveEmptyEntries)
                    .Select(val => double.Parse(val)).ToArray()),
                     new ValueComparer<double[]>(
                        (c1, c2) => c1.SequenceEqual(c2),
                        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                        c => c.ToArray()));
            builder.ApplyConfigurationsFromAssembly(Assembly.GetEntryAssembly());
            base.OnModelCreating(builder);
        }
        private async Task DispatchEvent()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x) 
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .FirstOrDefault();
                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }
    }
}
