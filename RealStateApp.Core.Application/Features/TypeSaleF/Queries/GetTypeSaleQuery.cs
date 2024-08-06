using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.TypeSaleDTOs;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using System.Net;

namespace RealStateApp.Core.Application.Features.TypeSaleF.Queries
{
    public sealed record GetTypeSaleQuery(string Id, bool TrackChanges) : IRequest<Response<TypeSaleDTO>>;
    internal sealed class GetTypeSaleHandler : IRequestHandler<GetTypeSaleQuery, Response<TypeSaleDTO>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GetTypeSaleHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<TypeSaleDTO>> Handle(GetTypeSaleQuery request, CancellationToken cancellationToken)
        {
            var typeSale = await _repository.TypeSale.GetByIdWithIncludeAsync(request.Id, new List<string> { "Properties" }, request.TrackChanges);

            if (typeSale is null)
                throw new ApiException($"The type sale with id: {request.Id} doesn't exist in the database.", (int)HttpStatusCode.NotFound);

            var TypeSaleDto = _mapper.Map<TypeSaleDTO>(typeSale);

            return new Response<TypeSaleDTO>(TypeSaleDto);
        }
    }
}
