using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.TypePropertyDTOs;
using RealStateApp.Core.Application.DataTransferObjects.TypeSaleDTOs;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.ViewModels.PropertyModels;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Features.TypePropertyF.Queries
{
    public sealed record GetTypePropertiesQuery(bool TrackChanges) : IRequest<Response<IEnumerable<TypePropertyDTO>>>;

    public class GetAmenitiesQueryHandler : IRequestHandler<GetTypePropertiesQuery, Response<IEnumerable<TypePropertyDTO>>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GetAmenitiesQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<TypePropertyDTO>>> Handle(GetTypePropertiesQuery request, CancellationToken cancellationToken)
        {
            var alltypeProperties = await _repository.TypeProperty.GetAllWithIncludeAsync(new List<string> { "Properties" }, request.TrackChanges);


            /*CreateMap<TypeProperty, TypePropertyDTO>()
                .ForMember(dest => dest.Properties, opt => opt.MapFrom(src => src.Properties));*/
            IEnumerable<TypePropertyDTO> typeProperties = _mapper.Map<List<TypePropertyDTO>>(alltypeProperties);

            if (alltypeProperties.Count == 0)
            {
                //throw new ApiException($"No typeProperties were found.", (int)HttpStatusCode.NotFound);
                return new Response<IEnumerable<TypePropertyDTO>>() { Message = "No type properties were found." };
            }

            return new Response<IEnumerable<TypePropertyDTO>>(typeProperties);
        }
    }
}
