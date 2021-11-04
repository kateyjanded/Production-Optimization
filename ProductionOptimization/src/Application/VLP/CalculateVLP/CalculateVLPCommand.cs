using Application.PVT.Query;
using Application.VLP.ParseLiftTable;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.VLP.CalculateVLP
{
    public class CalculateVLPCommand: IRequest<ResultDTO>
    {
        public string LiftTableContent { get; set; }
        public ParamEntryDTO WaterFraction { get; set; }
        public ParamEntryDTO GasFraction { get; set; }
        public ParamEntryDTO GasLiftFraction { get; set; }
        public ParamEntryDTO THP { get; set; }
        public double glr { get; set; }
    }
    public class CalculateVLPCommandHandler :IRequestHandler<CalculateVLPCommand, ResultDTO>
    {
        public async Task<ResultDTO> Handle(CalculateVLPCommand request, CancellationToken cancellationToken)
        {
            ILiftTableInfo liftTableInfo = new LiftTableInfo
            {
                ContentString = request.LiftTableContent,
                GasFractionUnit = request.GasFraction.Symbol,
                WaterFractionUnit = request.WaterFraction.Symbol,
                TubingHeadPressureUnit = request.THP.Symbol,
                ArtificialLiftQuantityUnit = request.GasLiftFraction.Symbol,
                TubingHeadPressure = request.THP.Value,
                WaterFraction = request.WaterFraction.Value,
                GasFraction = request.GasFraction.Value,
                ArtificialLiftQuantity = request.glr
            };
            return await Task.Run(() =>
            {
                var prodSystem = new ProductionSystem(liftTableInfo);
                var resultDto = new ResultDTO()
                {
                    Rates = prodSystem.OutFlowCurve().FlowRates,
                    Pressures = prodSystem.OutFlowCurve().BottomHolePressures,
                };
                return resultDto;
            });

        }
    }
}
