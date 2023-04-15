using JwtApp.Api.Core.Application.DTOs;
using JwtApp.Api.Core.Application.Features.CQRS.Queries;
using JwtApp.Api.Core.Application.Interfaces;
using JwtApp.Api.Core.Domain;
using MediatR;

namespace JwtApp.Api.Core.Application.Features.CQRS.Handlers
{
    public class CheckUserRequestHandler:IRequestHandler<CheckUserQueryRequest,CheckUserResponseDto>
    {
        private readonly IRepository<AppUser> _repository;
        private readonly IRepository<AppRole> _repositoryRole;

        public CheckUserRequestHandler(IRepository<AppRole> repositoryRole, IRepository<AppUser> repository)
        {
            _repositoryRole = repositoryRole;
            _repository = repository;
        }

        public async Task<CheckUserResponseDto> Handle(CheckUserQueryRequest request, CancellationToken cancellationToken)
        {
            var dto=new CheckUserResponseDto();

            var user = await _repository.GetByFilter(x => x.UserName == request.UserName && x.Password==request.Password);

           if (user == null)
            {
                dto.IsExist = false;
            }
            else
            {
                dto.UserName= user.UserName;
                dto.Id = user.Id;
                dto.IsExist= true;
                var role=await _repositoryRole.GetByFilter(x=>x.Id==user.AppRoleId);
                dto.Role = role?.Definition;
            }
           return dto;
        }
    }
}
