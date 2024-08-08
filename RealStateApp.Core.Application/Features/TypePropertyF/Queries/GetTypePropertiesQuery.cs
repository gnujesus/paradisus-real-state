using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.TypePropertyDTOs;
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
            var allTypeProperties = await _repository.TypeSale.GetAllWithIncludeAsync(new List<string> { "Properties" }, request.TrackChanges);
            IEnumerable<TypePropertyDTO> typeProperties = null!;

            if (allTypeProperties.Count == 0)
            {
                return new Response<IEnumerable<TypePropertyDTO>>() { Message = "No type properties were found." };
            }

            typeProperties = allTypeProperties.Select(_mapper.Map<TypePropertyDTO>);

            return new Response<IEnumerable<TypePropertyDTO>>(typeProperties) { Succeeded = true };
        }
    }
}
