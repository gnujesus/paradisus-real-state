using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.TypePropertyDTOs;
using RealStateApp.Core.Application.DataTransferObjects.TypeSaleDTOs;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Features.TypePropertyF.Commands
{
    public sealed record CreateTypePropertyCommand(TypePropertyForCreationDTO TypeProperty) : IRequest<Response<TypePropertyDTO>>;

    public class CreateTypePropertyCommandHandler : IRequestHandler<CreateTypePropertyCommand, Response<TypePropertyDTO>>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CreateTypePropertyCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<Response<TypePropertyDTO>> Handle(CreateTypePropertyCommand request, CancellationToken cancellationToken)
        {
            var typePropertyEntity = _mapper.Map<TypeProperty>(request.TypeProperty);

            return new Response<TypePropertyDTO>(data: _mapper.Map<TypePropertyDTO>(await _repositoryManager.TypeProperty.AddAsync(typePropertyEntity)));
        }
    }
}
