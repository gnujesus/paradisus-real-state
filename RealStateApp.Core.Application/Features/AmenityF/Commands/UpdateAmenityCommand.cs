﻿using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.AmenityDTOs;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using System.Net;

namespace RealStateApp.Core.Application.Features.AmenityF.Commands
{
    public sealed record UpdateAmenityCommand(string Id, AmenityForUpdateDTO Amenity, bool TrackChanges) : IRequest<Response<AmenityWithoutPropertiesDTO>>;

    internal sealed class UpdateAmenityHandler : IRequestHandler<UpdateAmenityCommand, Response<AmenityWithoutPropertiesDTO>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public UpdateAmenityHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<AmenityWithoutPropertiesDTO>> Handle(UpdateAmenityCommand request, CancellationToken cancellationToken)
        {
            var amenityEntity = await _repository.Amenity.GetByIdWithIncludeAsync(request.Id, new List<string> { "Properties" }, request.TrackChanges);

            if (amenityEntity is null)
                throw new ApiException($"The amenity with id: {request.Id} doesn't exist in the database.", (int)HttpStatusCode.NotFound);
<<<<<<< HEAD

            var updatedProperties = new List<Property>();
            foreach (var propertyId in request.Amenity.PropertiesIds)
            {
                var prop = await _repository.Property.GetByIdAsync(propertyId);
                if (prop != null)
                {
                    updatedProperties.Add(prop);
                }
            }
=======
                
            _mapper.Map(request.Amenity, AmenityEntity);
            await _repository.SaveAsync();
>>>>>>> d7da0004611b730c4ef9f7e9ee8e4a309fef9d6c

            amenityEntity.Properties = updatedProperties;

            _mapper.Map(request.Amenity, amenityEntity);

            await _repository.Amenity.UpdateWithNavigationsAsync(amenityEntity, request.Id);

            var amenity = await _repository.Amenity.GetByIdAsync(request.Id);
            var response = _mapper.Map<AmenityWithoutPropertiesDTO>(amenity);

            if (amenity.Properties is not null)
                response.PropertiesQuantity = amenity.Properties.Count();
            else
                response.PropertiesQuantity = 0;
                
            _mapper.Map(request.Amenity, AmenityEntity);
            await _repository.SaveAsync();

            return new Response<AmenityWithoutPropertiesDTO> { Data = response, Succeeded = true};
        }
    }
}