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
            var amenityEntity = _mapper.Map<Amenity>(request.Amenity);

            if (request.Amenity.PropertiesIds.Count() > 0)
            {
                request.Amenity.PropertiesIds.Select(async (id) =>
                {
                    if (id.Length == 6)
                        amenityEntity.Properties.Add(await _repositoryManager.Property.GetByIdAsync(id));

                    return id;
                });
            }

            await _repositoryManager.Amenity.AddAsync(amenityEntity);

            var AmenityToReturn = _mapper.Map<AmenityDTO>(amenityEntity);

            return new Response<AmenityDTO>(data: AmenityToReturn);
        }
    }
}
