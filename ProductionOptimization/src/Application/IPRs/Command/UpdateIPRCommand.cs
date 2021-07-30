using Application.PVT.Query;
using Application.SystemAnalysisModels.Interfaces;
using Application.SystemAnalysisModels.Query.GetWellModel;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.IPRs.Command
{
    public class UpdateIPRCommand: IRequest<Guid>
    {
        public bool UseLiftTable { get; set; }
        public ParamEntryDTO WaterFraction { get; set; }
        public ParamEntryDTO GasFraction { get; set; }
        public ParamEntryDTO ReservoirPressure { get; set; }
        public ParamEntryDTO ReservoirTemperature { get; set; }
        public ParamEntryDTO ProductivityIndex { get; set; }
        public string LiftTableContent { get; set; }
        public string LiftTablePath { get; set; }
        public Guid WellModelID { get; set; }
    }
    public class UpdateIPRCommandHandler : IRequestHandler<UpdateIPRCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IWellModelRepository wellModelRepository;

        public UpdateIPRCommandHandler(IMapper _mapper, IWellModelRepository _wellModelRepository)
        {
            mapper = _mapper;
            wellModelRepository = _wellModelRepository;
        }
        public async Task<Guid> Handle(UpdateIPRCommand request, CancellationToken cancellationToken)
        {
            var entity = await wellModelRepository.GetById(request.WellModelID);
            if (entity == null)
            {
                return Guid.Empty;
            }
            var ipr = entity.IPR;
            ipr.GasFraction = mapper.Map<ParamEntry>(request.GasFraction);
            ipr.WaterFraction = mapper.Map<ParamEntry>(request.WaterFraction);
            ipr.UseLiftTable = request.UseLiftTable;
            ipr.ReservoirPressure = mapper.Map<ParamEntry>(request.ReservoirPressure);
            ipr.ReservoirTemperature = mapper.Map<ParamEntry>(request.ReservoirTemperature);
            ipr.LiftTableContent = request.LiftTableContent;
            ipr.ProductivityIndex = mapper.Map<ParamEntry>(request.ProductivityIndex);
            wellModelRepository.Update(entity);
            await wellModelRepository.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
