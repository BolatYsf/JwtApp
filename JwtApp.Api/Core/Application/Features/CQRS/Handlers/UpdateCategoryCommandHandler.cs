using JwtApp.Api.Core.Application.DTOs;
using JwtApp.Api.Core.Application.Features.CQRS.Commands;
using JwtApp.Api.Core.Application.Interfaces;
using JwtApp.Api.Core.Domain;
using MediatR;

namespace JwtApp.Api.Core.Application.Features.CQRS.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest>
    {
        private readonly IRepository<Category> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IRepository<Category> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            //getfilter kullanamam cunku asnotrackıng kullanıyor ef core nesnenın context yasam dongusunde izlemez 
            var updatedEntity= await _repository.GetByIdAsync(request.Id);

            if (updatedEntity != null) {

                updatedEntity.Definition = request.Definition;
                 _repository.Update(updatedEntity);
                _unitOfWork.Commit();
            
            }

            return Unit.Value;
        }
    }
}
