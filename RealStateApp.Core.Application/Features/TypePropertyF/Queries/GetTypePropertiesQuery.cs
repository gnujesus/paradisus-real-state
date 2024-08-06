using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.TypeSaleDTOs;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;

namespace RealStateApp.Core.Application.Features.TypePropertyF.Queries
{
    public sealed record GetTypePropertiesQuery(bool TrackChanges) : IRequest<Response<IEnumerable<TypeSaleDTO>>>;

    public class GetAmenitiesQueryHandler : IRequestHandler<GetTypePropertiesQuery, Response<IEnumerable<TypeSaleDTO>>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GetAmenitiesQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<TypeSaleDTO>>> Handle(GetTypePropertiesQuery request, CancellationToken cancellationToken)
        {
            var allAmenities = await _repository.TypeSale.GetAllWithIncludeAsync(new List<string> { "Properties" }, request.TrackChanges);
            IEnumerable<TypeSaleDTO> typeProperties = null!;

            if (allAmenities.Count == 0)
            {
                //throw new ApiException($"No typeProperties were found.", (int)HttpStatusCode.NotFound);
                return new Response<IEnumerable<TypeSaleDTO>>() { Message = "No type properties were found." };
            }

            return new Response<IEnumerable<TypeSaleDTO>>(typeProperties);
        }
    }
}
