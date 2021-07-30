using Application.SystemAnalysisModels.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SystemAnalysisModels.Query.GetWellModel
{
    public class GetWellModelByIDQuery:IRequest<WellModelVM>
    {
        public Guid Id { get; set; }
    }
    public class GetWEllModelByIdQueryHandler : IRequestHandler<GetWellModelByIDQuery, WellModelVM>
    {
        private readonly IWellModelRepository repository;
        private readonly IMapper mapper;

        public GetWEllModelByIdQueryHandler(IWellModelRepository _repository, IMapper _mapper)
        {
            repository = _repository;
            mapper = _mapper;
        }
        public async Task<WellModelVM> Handle(GetWellModelByIDQuery request, CancellationToken cancellationToken)
        {
            var entity = await repository.GetById(request.Id);
            if (entity == null)
            {
                return null;
            }
            return  mapper.Map<WellModelVM>(entity);
        }
    }
}
