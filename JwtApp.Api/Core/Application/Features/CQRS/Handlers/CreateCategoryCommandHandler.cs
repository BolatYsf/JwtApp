using JwtApp.Api.Core.Application.DTOs;
using JwtApp.Api.Core.Application.Features.CQRS.Commands;
using JwtApp.Api.Core.Application.Interfaces;
using JwtApp.Api.Core.Domain;
using MediatR;

namespace JwtApp.Api.Core.Application.Features.CQRS.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest>
    {
        private readonly IRepository<Category> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IRepository<Category> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Category
            {
                Definition = request.Definition,
            });

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
