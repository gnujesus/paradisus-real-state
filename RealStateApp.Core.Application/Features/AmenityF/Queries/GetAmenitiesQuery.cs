using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.AmenityDTOs;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;

namespace RealStateApp.Core.Application.Features.AmenityF.Queries
{
    public sealed record GetAmenitiesQuery(bool TrackChanges) : IRequest<Response<IEnumerable<AmenityDTO>>>;

    public class GetAmenitiesQueryHandler : IRequestHandler<GetAmenitiesQuery, Response<IEnumerable<AmenityDTO>>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GetAmenitiesQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<AmenityDTO>>> Handle(GetAmenitiesQuery request, CancellationToken cancellationToken)
        {
            var allAmenities = await _repository.Amenity.GetAllWithIncludeAsync(new List<string> { "Properties" }, request.TrackChanges);
            IEnumerable<AmenityDTO> amenities = null!;

            if (allAmenities.Count == 0)
            {
                return new Response<IEnumerable<AmenityDTO>>() { Message = "No amenities were found." };
            }

            #region Deleted PropertiesQuantity
            //amenities = allAmenities.Select(x =>
            //{
            //    var a = _mapper.Map<AmenityDTO>(x);
            //    a.PropertiesQuantity = x.Properties.Count();
            //    return a;

            //}).ToList();
            #endregion

            amenities = allAmenities.Select(_mapper.Map<AmenityDTO>);

            return new Response<IEnumerable<AmenityDTO>>(amenities) { Succeeded = true };
        }
    }
}
