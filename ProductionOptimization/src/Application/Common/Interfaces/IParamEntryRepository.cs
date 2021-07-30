using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IParamEntryRepository: IRepository<ParamEntry>
    {
        void SaveAll(IList<ParamEntry> parameters);
        void UpdateAll(IList<ParamEntry> paramEntries);
        Task<ParamEntry> GetByName(string name);
    }
}
