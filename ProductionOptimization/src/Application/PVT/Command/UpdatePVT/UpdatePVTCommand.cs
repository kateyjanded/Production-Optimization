using Application.PVT.Interfaces;
using Application.PVT.Query;
using Application.SystemAnalysisModels.Interfaces;
using Application.SystemAnalysisModels.Query.GetWellModel;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.PVT.Command.UpdatPVT
{
    public class UpdatePVTCommand: IRequest<Guid>
    {
        public string FluidType { get; set; }
        public double OilGravity { get; set; }
        public double GasGravity { get; set; }
        public ParamEntryDTO GasRatio { get; set; }
        public ParamEntryDTO Temperature { get; set; }
        public ParamEntryDTO WaterSalinity { get; set; }
        public ParamEntryDTO Pressure { get; set; }
        public double C02 { get; set; }
        public double H2S { get; set; }
        public double N2 { get; set; }
        public string RSBO { get; set; }
        public string UO { get; set; }
        public double GasViscosity { get; set; }
        public string BlackOilModel { get; set; }
        public Guid WellModelID { get; set; }
    }
    public class UpdatePVTHandler : IRequestHandler<UpdatePVTCommand, Guid>
    {
        private readonly IWellModelRepository wellModelRepository;
        private readonly IMapper mapper;

        public UpdatePVTHandler(IWellModelRepository _wellModelRepository, IMapper _mapper)
        {
            wellModelRepository = _wellModelRepository;
            mapper = _mapper;
        }
        public async Task<Guid> Handle(UpdatePVTCommand request, CancellationToken cancellationToken)
        {
            var entity = await wellModelRepository.GetById(request.WellModelID);
            if (entity == null)
            {
                return Guid.Empty;
            }
            entity.PVT.FluidType = request.FluidType;
            entity.PVT.GasGravity = request.GasGravity;
            entity.PVT.GasRatio = mapper.Map<ParamEntry>(request.GasRatio);
            entity.PVT.H2S = request.H2S;
            entity.PVT.OilGravity = request.OilGravity;
            entity.PVT.N2 = request.N2;
            entity.PVT.Pressure = mapper.Map<ParamEntry>(request.Pressure);
            entity.PVT.RSBO = request.RSBO;
            entity.PVT.Temperature = mapper.Map<ParamEntry>(request.Temperature);
            entity.PVT.WaterSalinity = mapper.Map<ParamEntry>(request.WaterSalinity);
            entity.PVT.UO = request.UO;
            entity.PVT.BlackOilModel = request.BlackOilModel;
            entity.PVT.C02 = request.C02;
            entity.PVT.GasViscosity = request.GasViscosity;
            wellModelRepository.Update(entity);
            await wellModelRepository.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
