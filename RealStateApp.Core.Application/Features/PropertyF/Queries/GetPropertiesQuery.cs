using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.AmenityDTOs;
using RealStateApp.Core.Application.DataTransferObjects.PropertyDTOs;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;

namespace RealStateApp.Core.Application.Features.PropertyF.Queries
{
    public sealed record GetPropertiesQuery(bool TrackChanges) : IRequest<Response<IEnumerable<PropertyDTO>>>;

    public class GetAmenitiesQueryHandler : IRequestHandler<GetPropertiesQuery, Response<IEnumerable<PropertyDTO>>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GetAmenitiesQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<PropertyDTO>>> Handle(GetPropertiesQuery request, CancellationToken cancellationToken)
        {
            var properties = await _repository.Property
                                                .GetAllWithIncludeAsync(new List<string> { "Agent", "TypeProperty", "TypeProperty", "Amenities" },
                                                    request.TrackChanges);
            IEnumerable<PropertyDTO> Properties = null!;

            if (properties.Count == 0)
            {
                return new Response<IEnumerable<PropertyDTO>>() { Message = "No properties were found." };
            }

            Properties = properties.Select(_mapper.Map<PropertyDTO>);

            return new Response<IEnumerable<PropertyDTO>>(Properties) { Succeeded = true };
        }
    }
}
