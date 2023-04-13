using AutoMapper;
using JwtApp.Api.Core.Application.DTOs;
using JwtApp.Api.Core.Application.Features.CQRS.Commands;
using JwtApp.Api.Core.Application.Interfaces;
using JwtApp.Api.Core.Domain;
using MediatR;

namespace JwtApp.Api.Core.Application.Features.CQRS.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
    {


        private readonly IRepository<Product> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IRepository<Product> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product updatedProduct = await _repository.GetByIdAsync(request.Id);

            if (updatedProduct != null)
            {
                updatedProduct.Name = request.Name;
                updatedProduct.Price = request.Price;
                updatedProduct.CategoryId = request.CategoryId;
                updatedProduct.Stock = request.Stock;

                _repository.Update(updatedProduct);
                _unitOfWork.Commit();
            }
            return Unit.Value;
        }
    }
}
