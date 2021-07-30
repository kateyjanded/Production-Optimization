using Application.SystemAnalysisModels.Interfaces;
using Application.SystemAnalysisModels.Query.GetWellModel;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.ModelComponents;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SystemAnalysisModels.Commands.CreateModel
{
    public class CreateSystemAnalysisModel: IRequest<WellModelVM>
    {
        public string DrainagePointName { get; set; }
        public string FluidPropertyName { get; set; }
        public DateTime ModelDate { get; set; }
        public string Name { get; set; }

    }
    public class CreateSystemAnalysisModelCommandHandler : IRequestHandler<CreateSystemAnalysisModel, WellModelVM>
    {
        IWellModelRepository _repository;
        private readonly IMapper mapper;

        public CreateSystemAnalysisModelCommandHandler(IWellModelRepository repository, IMapper _mapper)
        {
            _repository = repository;
            mapper = _mapper;
        }
        public async Task<WellModelVM> Handle(CreateSystemAnalysisModel request, CancellationToken cancellationToken)
        {
            var entity = new SystemAnalysisModel()
            {
                DrainagePointName = request.DrainagePointName,
                ModelDate = request.ModelDate,
                Name = request.Name,
                ModelBackground = new Domain.Entities.ModelComponents.ModelBackground(),
                PVT = new Domain.Entities.ModelComponents.PVT()
                {
                    WaterSalinity = new ParamEntry(),
                    GasRatio = new ParamEntry(),
                    Pressure = new ParamEntry(),
                    Temperature = new ParamEntry()
                },
                IPR = new IPR()
                
            };
            _repository.Save(entity);
            await _repository.SaveChangesAsync(cancellationToken);
            return mapper.Map<WellModelVM>(entity);
        }
    }
}