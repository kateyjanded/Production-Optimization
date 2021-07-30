using Application.Common.Interfaces;
using Application.PVT.Interfaces;
using Application.SystemAnalysisModels.Interfaces;
using AutoMapper;
using Domain.Common;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.PVT.Command
{
    public class CreatePVTCommand: IRequest<Guid>
    {
        public Guid SystemAnalysisModelId { get; set; }
        public FluidTypeEnum ReservoirType { get; set; }
    }
    public class CreatePVTCommandHandler : IRequestHandler<CreatePVTCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IParamEntryRepository paramEntryRepository;
        private readonly IWellModelRepository wellModelRepository;
        private readonly IPVTRepository pVTRepository;

        public CreatePVTCommandHandler(IWellModelRepository _wellModelRepository, IPVTRepository _pVTRepository, 
            IMapper _mapper, IParamEntryRepository _paramEntryRepository)
        {
            
            wellModelRepository = _wellModelRepository;
            pVTRepository = _pVTRepository;
            mapper = _mapper;
            paramEntryRepository = _paramEntryRepository;
        }


        public async Task<Guid> Handle(CreatePVTCommand request, CancellationToken cancellationToken)
        {
            var wellModel = await wellModelRepository.GetById(request.SystemAnalysisModelId);
            if (wellModel == null)
            {
                return Guid.Empty;
            }
            var pVT = new Domain.Entities.ModelComponents.PVT();
            pVTRepository.Save(pVT);
            wellModel.PVT = pVT;
            wellModelRepository.Update(wellModel);
            await wellModelRepository.SaveChangesAsync(cancellationToken);
            return pVT.Id;
        }
    }
}
