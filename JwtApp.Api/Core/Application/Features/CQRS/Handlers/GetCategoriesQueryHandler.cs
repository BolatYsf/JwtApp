using AutoMapper;
using JwtApp.Api.Core.Application.DTOs;
using JwtApp.Api.Core.Application.Features.CQRS.Queries;
using JwtApp.Api.Core.Application.Interfaces;
using JwtApp.Api.Core.Domain;
using MediatR;

namespace JwtApp.Api.Core.Application.Features.CQRS.Handlers
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQueryRequest, List<CategoryListDto>>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CategoryListDto>> Handle(GetCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            var data=await _repository.GetAllAsync();
            return _mapper.Map<List<CategoryListDto>>(data);

        }
    }
}
