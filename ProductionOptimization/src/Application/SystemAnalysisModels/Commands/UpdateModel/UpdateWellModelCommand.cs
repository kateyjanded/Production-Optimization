using Application.SystemAnalysisModels.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SystemAnalysisModels.Commands.UpdateModel
{
    public class UpdateWellModelCommand: IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string WellType { get; set; }
        public bool SandControl { get; set; }
        public bool TemperatureModelling { get; set; }
        public bool ArtificialLift { get; set; }
        public bool SurfaceProfileModelling { get; set; }
        public bool UseLiftTable { get; set; }
    }
    public class UpdateWellModelCommandHandler : IRequestHandler<UpdateWellModelCommand, Guid>
    {
        private readonly IWellModelRepository _wellModelRepository;

        public UpdateWellModelCommandHandler(IWellModelRepository wellModelRepository)
        {
            _wellModelRepository = wellModelRepository;
        }
        public async Task<Guid> Handle(UpdateWellModelCommand request, CancellationToken cancellationToken)
        {
            var entity = await _wellModelRepository.GetById(request.Id);
            _wellModelRepository.Update(entity);
            await _wellModelRepository.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
