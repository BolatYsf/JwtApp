using JwtApp.Api.Core.Application.Features.CQRS.Commands;
using JwtApp.Api.Core.Application.Interfaces;
using JwtApp.Api.Core.Domain;
using MediatR;

namespace JwtApp.Api.Core.Application.Features.CQRS.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest>
    {

        private readonly IRepository<Product> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductCommandHandler(IRepository<Product> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var deletedProduct = await _repository.GetByFilter(x=>x.Id==request.Id);
            _repository.Remove(deletedProduct);
            _unitOfWork.Commit();
            return Unit.Value;
        }
    }
}
