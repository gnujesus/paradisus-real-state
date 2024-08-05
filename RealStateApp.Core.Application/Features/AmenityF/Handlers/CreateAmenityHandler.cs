using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.AmenityDTOs;
using RealStateApp.Core.Application.Features.AmenityF.Commands;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Features.AmenityF.Handlers
{
    internal sealed class CreateAmenityHandler : IRequestHandler<CreateAmenityCommand, AmenityDTO>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CreateAmenityHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AmenityDTO> Handle(CreateAmenityCommand request, CancellationToken cancellationToken)
        {
            var AmenityEntity = _mapper.Map<Amenity>(request.Amenity);

            await _repository.Amenity.AddAsync(AmenityEntity);
            await _repository.SaveAsync();

            var AmenityToReturn = _mapper.Map<AmenityDTO>(AmenityEntity);

            return AmenityToReturn;
        }
    }
}
