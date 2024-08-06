using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.AmenityDTOs;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;

namespace RealStateApp.Core.Application.Features.AmenityF.Queries
{
    public sealed record GetAmenitiesQuery(bool TrackChanges) : IRequest<Response<IEnumerable<AmenityWithoutPropertiesDTO>>>;

    public class GetAmenitiesQueryHandler : IRequestHandler<GetAmenitiesQuery, Response<IEnumerable<AmenityWithoutPropertiesDTO>>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GetAmenitiesQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<AmenityWithoutPropertiesDTO>>> Handle(GetAmenitiesQuery request, CancellationToken cancellationToken)
        {
            var allAmenities = await _repository.Amenity.GetAllWithIncludeAsync(new List<string> { "Properties" }, request.TrackChanges);
            IEnumerable<AmenityWithoutPropertiesDTO> amenities = null!;

            if (allAmenities.Count == 0)
            {
                //throw new ApiException($"No amenities were found.", (int)HttpStatusCode.NotFound);
                return new Response<IEnumerable<AmenityWithoutPropertiesDTO>>() { Message = "No amenities were found." };
            }

            amenities = allAmenities.Select(x =>
            {
                var a = _mapper.Map<AmenityWithoutPropertiesDTO>(x);
                a.PropertiesQuantity = x.Properties.Count();
                return a;

            }).ToList();

            return new Response<IEnumerable<AmenityWithoutPropertiesDTO>>(amenities);
        }
    }
}
