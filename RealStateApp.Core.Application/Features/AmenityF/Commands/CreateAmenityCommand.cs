using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.AmenityDTOs;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Features.AmenityF.Commands
{
    public sealed record CreateAmenityCommand(AmenityForCreationDTO Amenity) : IRequest<Response<AmenityDTO>>;

    public class CreateAmenityCommandHandler : IRequestHandler<CreateAmenityCommand, Response<AmenityDTO>>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CreateAmenityCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<Response<AmenityDTO>> Handle(CreateAmenityCommand request, CancellationToken cancellationToken)
        {
            var AmenityEntity = _mapper.Map<Amenity>(request.Amenity);

            await _repositoryManager.Amenity.AddAsync(AmenityEntity);
            await _repositoryManager.SaveAsync();

            var AmenityToReturn = _mapper.Map<AmenityDTO>(AmenityEntity);

            return new Response<AmenityDTO>(data: AmenityToReturn);
        }
    }
}
