using Application.Common.Extensions;
using Application.ModelBackGround.Interfaces;
using Application.SystemAnalysisModels.Interfaces;
using Domain.Entities.ModelComponents;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ModelBackGround.Commands
{
    public class CreateModelBackgroundCommand: IRequest<Guid>
    {
        public string Description { get; set; } = "";
        //publil string ModelDate { get; set; } = DateTime.Today.ToString("d/MM/yyyy");
        public string FluidType { get; set; }
        public string FlowType { get; set; }
        public string WellType { get; set; }
        public bool SandControl { get; set; }
        public bool TemperatureModelling { get; set; }
        public bool ArtificialLift { get; set; }
        public bool SurfaceProfileModelling { get; set; }
        public bool UseLiftTable { get; set; }
        public Guid SystemAnalysisModelId { get; set; }
    }
    public class CreateModelBackgroundCommandHandler : IRequestHandler<CreateModelBackgroundCommand, Guid>
    {
        private readonly IWellModelRepository wellModelRepository;

        public CreateModelBackgroundCommandHandler(IModelBackgroundRepository _modelBackgroundRepository, IWellModelRepository _wellModelRepository)
        {
            modelBackgroundRepository = _modelBackgroundRepository;
            wellModelRepository = _wellModelRepository;
        }
        public IModelBackgroundRepository modelBackgroundRepository { get; }

        public async Task<Guid> Handle(CreateModelBackgroundCommand request, CancellationToken cancellationToken)
        {
            var wellModel = await wellModelRepository.GetById(request.SystemAnalysisModelId);
            if (wellModel == null)
            {
                return Guid.Empty;
            }
            var modelBackground = new ModelBackground()
            {
                FluidType = request.FluidType.ConvertToEnum(),
                FlowType = request.FluidType.ConvertToFlowTypeEnum(),
                UseLiftTable = request.UseLiftTable,
                ArtificialLift = request.ArtificialLift,
                Description = request.Description,
                WellType = request.WellType,
                SurfaceProfileModelling = request.SurfaceProfileModelling,
            };
            modelBackgroundRepository.Save(modelBackground);
            wellModel.ModelBackground = modelBackground;
            wellModelRepository.Update(wellModel);
            await wellModelRepository.SaveChangesAsync(cancellationToken);
            return modelBackground.Id;
        }
    }
}