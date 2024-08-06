using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.AmenityDTOs;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using System.Net;

namespace RealStateApp.Core.Application.Features.AmenityF.Queries
{
    public sealed record GetAmenityQuery(string Id, bool TrackChanges) : IRequest<Response<AmenityWithoutPropertiesDTO>>;
    internal sealed class GetAmenityHandler : IRequestHandler<GetAmenityQuery, Response<AmenityWithoutPropertiesDTO>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GetAmenityHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<AmenityWithoutPropertiesDTO>> Handle(GetAmenityQuery request, CancellationToken cancellationToken)
        {
            var amenities = await _repository.Amenity.GetAllWithIncludeAsync(new List<string> { "Properties" }, request.TrackChanges);
            var amenity = amenities.FirstOrDefault(a => a.Id == request.Id);

            if (amenity is null)
                throw new ApiException($"The amenity with id: {request.Id} doesn't exist in the database.", (int)HttpStatusCode.NotFound);

            var AmenityDto = _mapper.Map<AmenityWithoutPropertiesDTO>(amenity);

            AmenityDto.PropertiesQuantity = amenity.Properties.Count;
            return new Response<AmenityWithoutPropertiesDTO>(AmenityDto);
        }
    }
}
