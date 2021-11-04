using Application.PVT.Query;
using Application.SystemAnalysisModels.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.VLP.UpdateVLP
{
    public class UpdateVLPCommand: IRequest<Guid>
    {
        public ParamEntryDTO WaterFraction { get; set; }
        public ParamEntryDTO GasFraction { get; set; }
        public ParamEntryDTO THP { get; set; }
        public ParamEntryDTO GasLiftFraction { get; set; }
        public string LiftTableContent { get; set; }
        public string LiftTablePath { get; set; }
        public double[] Rates { get; set; }
        public double[] Pressures { get; set; }
        public Guid WellModelID { get; set; }
    }
    public class UpdateVLPCommandHandler : IRequestHandler<UpdateVLPCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IWellModelRepository wellModelRepository;

        public UpdateVLPCommandHandler(IMapper _mapper, IWellModelRepository _wellModelRepository)
        {
            mapper = _mapper;
            wellModelRepository = _wellModelRepository;
        }
        public async Task<Guid> Handle(UpdateVLPCommand request, CancellationToken cancellationToken)
        {
            var entity = await wellModelRepository.GetById(request.WellModelID);
            if (entity == null)
            {
                return Guid.Empty;
            }
            var vlp = entity.VLP;
            vlp.GasFraction = AssignValues(vlp.GasFraction, request.GasFraction);
            vlp.WaterFraction = AssignValues(vlp.WaterFraction, request.WaterFraction);
            vlp.THP = mapper.Map<ParamEntry>(request.THP);
            vlp.LiftTableContent = request.LiftTableContent;
            vlp.LiftTablePath = request.LiftTablePath;
            vlp.GasLiftFraction = AssignValues(vlp.GasLiftFraction, request.GasLiftFraction);
            vlp.Rates = request.Rates;
            vlp.Pressures = request.Pressures;
            wellModelRepository.Update(entity);
            await wellModelRepository.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
        private static ParamEntry AssignValues(ParamEntry paramEntry, ParamEntryDTO request)
        {
            paramEntry.Name = request.Name;
            paramEntry.Value = request.Value;
            paramEntry.Symbol = request.Symbol;
            return paramEntry;
        }
    }
}
