using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.AmenityDTOs;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using System.Net;

namespace RealStateApp.Core.Application.Features.AmenityF.Commands
{
    public sealed record UpdateAmenityCommand(string Id, AmenityForUpdateDTO Amenity, bool TrackChanges) : IRequest<Response<Unit>>;

    internal sealed class UpdateAmenityHandler : IRequestHandler<UpdateAmenityCommand, Response<Unit>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public UpdateAmenityHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<Unit>> Handle(UpdateAmenityCommand request, CancellationToken cancellationToken)
        {
            var AmenityEntity = await _repository.Amenity.GetByIdAsync(request.Id, request.TrackChanges);

            if (AmenityEntity is null)
                throw new ApiException($"The amenity with id: {request.Id} doesn't exist in the database.", (int)HttpStatusCode.NotFound);

            if (request.Amenity.PropertiesIds.Count() > 0)
            {
                if (AmenityEntity.Properties is null)
                    AmenityEntity.Properties = new List<Property>();
                else 
                    AmenityEntity.Properties.Clear();

                foreach (var propertyId in request.Amenity.PropertiesIds)
                {
                    AmenityEntity.Properties.Add(await _repository.Property.GetByIdAsync(propertyId));
                }
            }
                
            _mapper.Map(request.Amenity, AmenityEntity);
            await _repository.SaveAsync();

            return new Response<Unit> { Data = Unit.Value };
        }
    }
}