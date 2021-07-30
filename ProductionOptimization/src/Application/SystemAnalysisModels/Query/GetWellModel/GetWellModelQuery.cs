using Application.SystemAnalysisModels.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SystemAnalysisModels.Query.GetWellModel
{
    public class GetWellModelQuery: IRequest<List<WellModelVM>>
    {
    }
    public class GetWellModelQueryHandler : IRequestHandler<GetWellModelQuery, List<WellModelVM>>
    {
        IWellModelRepository _repository;
        IMapper _mapper;
        public GetWellModelQueryHandler(IWellModelRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<WellModelVM>> Handle(GetWellModelQuery request, CancellationToken cancellationToken)
        {
            var items = _repository.GetAll();
            return await items.ProjectTo<WellModelVM>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
