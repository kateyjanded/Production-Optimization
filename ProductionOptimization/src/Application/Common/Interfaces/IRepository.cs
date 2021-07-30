using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IRepository<T> where T: AuditableEntity
    {
        void Save(T entity);
        Task SaveAsync(T entity);
        void Update(T entity);
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<T> GetById(Guid Id);
        IQueryable<T> GetAll();
        void Delete(T entity);
        Task DeleteAsync(T entity);
    }
}