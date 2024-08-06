using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.TypePropertyDTOs;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using System.Net;

namespace RealStateApp.Core.Application.Features.TypePropertyF.Queries
{
    public sealed record GetTypePropertyQuery(string Id, bool TrackChanges) : IRequest<Response<TypePropertyDTO>>;
    internal sealed class GetTypePropertyHandler : IRequestHandler<GetTypePropertyQuery, Response<TypePropertyDTO>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GetTypePropertyHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<TypePropertyDTO>> Handle(GetTypePropertyQuery request, CancellationToken cancellationToken)
        {
            var typeProperty = await _repository.TypeProperty.GetByIdWithIncludeAsync(request.Id, new List<string> { "Properties" }, request.TrackChanges);

            if (typeProperty is null)
                throw new ApiException($"The type property with id: {request.Id} doesn't exist in the database.", (int)HttpStatusCode.NotFound);

            var TypePropertyDto = _mapper.Map<TypePropertyDTO>(typeProperty);

            return new Response<TypePropertyDTO>(TypePropertyDto);
        }
    }
}
