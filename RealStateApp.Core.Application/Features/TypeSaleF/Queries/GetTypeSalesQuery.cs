using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.TypeSaleDTOs;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;

namespace RealStateApp.Core.Application.Features.TypeSaleF.Queries
{
    public sealed record GetTypeSalesQuery(bool TrackChanges) : IRequest<Response<IEnumerable<TypeSaleDTO>>>;

    public class GetAmenitiesQueryHandler : IRequestHandler<GetTypeSalesQuery, Response<IEnumerable<TypeSaleDTO>>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GetAmenitiesQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<TypeSaleDTO>>> Handle(GetTypeSalesQuery request, CancellationToken cancellationToken)
        {
            var allAmenities = await _repository.TypeSale.GetAllWithIncludeAsync(new List<string> { "Properties" }, request.TrackChanges);
            IEnumerable<TypeSaleDTO> typeSales = null!;

            if (allAmenities.Count == 0)
            {
                //throw new ApiException($"No typeSales were found.", (int)HttpStatusCode.NotFound);
                return new Response<IEnumerable<TypeSaleDTO>>() { Message = "No type sales were found." };
            }

            return new Response<IEnumerable<TypeSaleDTO>>(typeSales);
        }
    }
}
