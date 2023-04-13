using AutoMapper;
using JwtApp.Api.Core.Application.DTOs;
using JwtApp.Api.Core.Application.Features.CQRS.Commands;
using JwtApp.Api.Core.Application.Interfaces;
using JwtApp.Api.Core.Application.Mapping;
using JwtApp.Api.Core.Domain;
using MediatR;

namespace JwtApp.Api.Core.Application.Features.CQRS.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest>
    {

        private readonly IRepository<Product> _repository;
        private readonly IUnitOfWork _unitOfWork;
        

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IRepository<Product> repository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
          
        }

        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Product
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                Stock = request.Stock,
                Price = request.Price
            });

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
