using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.PropertyDTOs;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using System.Net;

namespace RealStateApp.Core.Application.Features.PropertyF.Queries
{
    public sealed record GetPropertyQuery(string Id, bool TrackChanges) : IRequest<Response<PropertyDTO>>;
    internal sealed class GetPropertyHandler : IRequestHandler<GetPropertyQuery, Response<PropertyDTO>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GetPropertyHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<PropertyDTO>> Handle(GetPropertyQuery request, CancellationToken cancellationToken)
        {
            var Property = await _repository.Property.GetByIdWithIncludeAsync(request.Id, new List<string> { "Agent", "TypeProperty", "TypeSale", "Amenities" }, request.TrackChanges);

            if (Property is null)
                throw new ApiException($"The property with id: {request.Id} doesn't exist in the database.", (int)HttpStatusCode.NotFound);

            var PropertyDto = _mapper.Map<PropertyDTO>(Property);

            return new Response<PropertyDTO>(PropertyDto);
        }
    }
}
