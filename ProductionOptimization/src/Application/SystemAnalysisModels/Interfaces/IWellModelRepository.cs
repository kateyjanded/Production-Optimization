using Application.Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SystemAnalysisModels.Interfaces
{
    public interface IWellModelRepository: IRepository<SystemAnalysisModel>
    {
        SystemAnalysisModel GetWellModelByName(string drainagePoint);
    }
}
