using JwtApp.Api.Core.Application.Features.CQRS.Commands;
using JwtApp.Api.Core.Application.Interfaces;
using JwtApp.Api.Core.Domain;
using MediatR;

namespace JwtApp.Api.Core.Application.Features.CQRS.Handlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest>
    {
        private readonly IRepository<Category> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, IRepository<Category> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var deletedCategory=await _repository.GetByIdAsync(request.Id);

            if (deletedCategory != null)
            {
                _repository.Remove(deletedCategory);
                _unitOfWork.Commit();
            }
            return Unit.Value;
        }
    }
}
