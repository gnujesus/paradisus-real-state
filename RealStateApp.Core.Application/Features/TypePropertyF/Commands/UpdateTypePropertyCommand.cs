using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.TypePropertyDTOs;
using RealStateApp.Core.Application.DataTransferObjects.TypeSaleDTOs;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using System.Net;

namespace RealStateApp.Core.Application.Features.TypePropertyF.Commands
{
    public sealed record UpdateTypePropertyCommand(string Id, TypePropertyForUpdateDTO TypeProperty, bool TrackChanges) : IRequest<Response<TypePropertyDTO>>;

    internal sealed class UpdateTypePropertyHandler : IRequestHandler<UpdateTypePropertyCommand, Response<TypePropertyDTO>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public UpdateTypePropertyHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<TypePropertyDTO>> Handle(UpdateTypePropertyCommand request, CancellationToken cancellationToken)
        {
            var typePropertyEntity = await _repository.TypeProperty.GetByIdAsync(request.Id, request.TrackChanges);

            if (typePropertyEntity is null)
                throw new ApiException($"The type property with id: {request.Id} doesn't exist in the database.", (int)HttpStatusCode.NotFound);

            _mapper.Map(request.TypeProperty, typePropertyEntity);

            await _repository.TypeProperty.UpdateAsync(typePropertyEntity, request.Id);

            var typeProperty = await _repository.TypeProperty.GetByIdAsync(request.Id);
            var response = _mapper.Map<TypePropertyDTO>(typeProperty);

            return new Response<TypePropertyDTO> { Data = response, Succeeded = true };
        }
    }
}
