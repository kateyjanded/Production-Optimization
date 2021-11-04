using Application.Common.Extensions;
using Application.ModelBackGround.Interfaces;
using Application.SystemAnalysisModels.Interfaces;
using Application.SystemAnalysisModels.Query.GetWellModel;
using AutoMapper;
using Domain.Entities.ModelComponents;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ModelBackGround.Commands.UpdateCommand
{
    public class UpdateModelBackgroundCommand: IRequest<Guid>
    {
        public string Description { get; set; } = "";
        public Guid WellModelID { get; set; }
        //publil string ModelDate { get; set; } = DateTime.Today.ToString("d/MM/yyyy");
        public string FluidType { get; set; }
        public string FlowType { get; set; }
        public string WellType { get; set; }
        public bool SandControl { get; set; }
        public bool TemperatureModelling { get; set; }
        public bool ArtificialLift { get; set; }
        public bool SurfaceProfileModelling { get; set; }
        public bool UseLiftTable { get; set; }
    }
    public class UpdateModelBackgroundCommandHandler : IRequestHandler<UpdateModelBackgroundCommand, Guid>
    {
        private readonly IModelBackgroundRepository repository;
        private IWellModelRepository _wellModelRepository;
        private readonly IMapper mapper;

        public UpdateModelBackgroundCommandHandler(IModelBackgroundRepository _repository, IWellModelRepository wellModelRepository, IMapper _mapper)
        {
            _wellModelRepository = wellModelRepository;
            mapper = _mapper;
            repository = _repository;
        }
        public async Task<Guid> Handle(UpdateModelBackgroundCommand request, CancellationToken cancellationToken)
        {
            var entity = await _wellModelRepository.GetById(request.WellModelID);
            if (entity == null)
            {
                return Guid.Empty;
            }
            entity.ModelBackground.FluidType = request.FluidType.ConvertToEnum();
            entity.ModelBackground.FlowType = request.FluidType.ConvertToFlowTypeEnum();
            entity.ModelBackground.UseLiftTable = request.UseLiftTable;
            entity.ModelBackground.ArtificialLift = request.ArtificialLift;
            entity.ModelBackground.Description = request.Description;
            entity.ModelBackground.WellType = request.WellType.ConvertToEnum();
            entity.ModelBackground.SurfaceProfileModelling = request.SurfaceProfileModelling;
            entity.ModelBackground.TemperatureModelling = request.TemperatureModelling;
            entity.ModelBackground.SandControl = request.SandControl;
            repository.Update(entity.ModelBackground);
            await repository.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
