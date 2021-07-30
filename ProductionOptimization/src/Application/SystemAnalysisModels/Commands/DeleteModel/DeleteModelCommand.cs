using Application.SystemAnalysisModels.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SystemAnalysisModels.Commands.DeleteModel
{
    public class DeleteModelCommand: IRequest
    {
        public Guid Id { get; set; }
    }
    public class DeleteModelCommandHandler : IRequestHandler<DeleteModelCommand>
    {
        private readonly IWellModelRepository wellModelRepository;

        public DeleteModelCommandHandler(IWellModelRepository _wellModelRepository)
        {
            wellModelRepository = _wellModelRepository;
        }
        public async Task<Unit> Handle(DeleteModelCommand request, CancellationToken cancellationToken)
        {
            var model = await wellModelRepository.GetById(request.Id);
            if (model == null)
            {
                return Unit.Value;
            }
            wellModelRepository.Delete(model);
            await wellModelRepository.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
