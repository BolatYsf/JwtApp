using AutoMapper;
using JwtApp.Api.Core.Application.DTOs;
using JwtApp.Api.Core.Application.Features.CQRS.Queries;
using JwtApp.Api.Core.Application.Interfaces;
using JwtApp.Api.Core.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JwtApp.Api.Core.Application.Features.CQRS.Handlers
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQueryRequest, CategoryListDto?>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoryListDto> Handle(GetCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var data=await _repository.GetByFilter(x=>x.Id==request.Id);

            return _mapper.Map<CategoryListDto>(data);
        }
    }
}
