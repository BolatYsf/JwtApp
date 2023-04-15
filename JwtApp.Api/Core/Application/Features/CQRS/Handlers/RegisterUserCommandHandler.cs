using JwtApp.Api.Core.Application.Enums;
using JwtApp.Api.Core.Application.Features.CQRS.Commands;
using JwtApp.Api.Core.Application.Interfaces;
using JwtApp.Api.Core.Domain;
using MediatR;

namespace JwtApp.Api.Core.Application.Features.CQRS.Handlers
{
    public class RegisterUserCommandHandler:IRequestHandler<RegisterUserCommandRequest>
    {
        private readonly IRepository<AppUser> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(IUnitOfWork unitOfWork, IRepository<AppUser> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Unit> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new AppUser
            {
                Password = request.Password,
                UserName = request.UserName,
                AppRoleId = (int)RoleType.Member
            });

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
