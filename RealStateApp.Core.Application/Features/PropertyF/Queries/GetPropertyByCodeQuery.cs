using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.PropertyDTOs;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using System.Net;

namespace RealStateApp.Core.Application.Features.PropertyF.Queries
{
    public sealed record GetPropertyByCodeQuery(string Code, bool TrackChanges) : IRequest<Response<PropertyDTO>>;
    internal sealed class GetPropertyByCodeHandler : IRequestHandler<GetPropertyByCodeQuery, Response<PropertyDTO>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GetPropertyByCodeHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<PropertyDTO>> Handle(GetPropertyByCodeQuery request, CancellationToken cancellationToken)
        {
            var Property = await _repository.Property.GetByCodeWithIncludeAsync(request.Code, new List<string> { "Agent", "TypeProperty", "TypeSale", "Amenities" }, request.TrackChanges);

            if (Property is null)
                throw new ApiException($"The property with code: {request.Code} doesn't exist in the database.", (int)HttpStatusCode.NotFound);

            var PropertyDto = _mapper.Map<PropertyDTO>(Property);

            return new Response<PropertyDTO>(PropertyDto);
        }
    }
}
