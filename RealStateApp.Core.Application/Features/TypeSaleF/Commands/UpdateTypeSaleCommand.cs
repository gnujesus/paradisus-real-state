using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.TypeSaleDTOs;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using System.Net;

namespace RealStateApp.Core.Application.Features.TypeSaleF.Commands
{
    public sealed record UpdateTypeSaleCommand(string Id, TypeSaleForUpdateDTO TypeSale, bool TrackChanges) : IRequest<Response<TypeSaleDTO>>;

    internal sealed class UpdateTypeSaleHandler : IRequestHandler<UpdateTypeSaleCommand, Response<TypeSaleDTO>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public UpdateTypeSaleHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<TypeSaleDTO>> Handle(UpdateTypeSaleCommand request, CancellationToken cancellationToken)
        {
            var typeSaleEntity = await _repository.TypeSale.GetByIdAsync(request.Id, request.TrackChanges);

            if (typeSaleEntity is null)
                throw new ApiException($"The type sale with id: {request.Id} doesn't exist in the database.", (int)HttpStatusCode.NotFound);

            _mapper.Map(request.TypeSale, typeSaleEntity);

            await _repository.TypeSale.UpdateAsync(typeSaleEntity, request.Id);

            var typeSale = await _repository.TypeSale.GetByIdAsync(request.Id);
            var response = _mapper.Map<TypeSaleDTO>(typeSale);

            return new Response<TypeSaleDTO> { Data = response, Succeeded = true};
        }
    }
}