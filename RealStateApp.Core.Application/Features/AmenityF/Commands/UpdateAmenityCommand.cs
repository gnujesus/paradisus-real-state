using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.AmenityDTOs;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using System.Net;

namespace RealStateApp.Core.Application.Features.AmenityF.Commands
{
    public sealed record UpdateAmenityCommand(string Id, AmenityForUpdateDTO Amenity, bool TrackChanges) : IRequest<Response<AmenityDTO>>;

    internal sealed class UpdateAmenityHandler : IRequestHandler<UpdateAmenityCommand, Response<AmenityDTO>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public UpdateAmenityHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<AmenityDTO>> Handle(UpdateAmenityCommand request, CancellationToken cancellationToken)
        {
            var amenityEntity = await _repository.Amenity.GetByIdWithIncludeAsync(request.Id, new List<string> { "Properties" }, request.TrackChanges);

            if (amenityEntity is null)
                throw new ApiException($"The amenity with id: {request.Id} doesn't exist in the database.", (int)HttpStatusCode.NotFound);

            #region Deleted Properties
            //var updatedProperties = new List<Property>();
            //foreach (var propertyId in request.Amenity.PropertiesIds)
            //{
            //    var prop = await _repository.Property.GetByIdAsync(propertyId);
            //    if (prop != null)
            //    {
            //        updatedProperties.Add(prop);
            //    }
            //}

            //amenityEntity.Properties = updatedProperties;
            #endregion


            _mapper.Map(request.Amenity, amenityEntity);

            await _repository.Amenity.UpdateAsync(amenityEntity, request.Id);

            var amenity = await _repository.Amenity.GetByIdAsync(request.Id);
            var response = _mapper.Map<AmenityDTO>(amenity);

            #region Deleted PropertiesQuantity
            //if (amenity.Properties is not null)
            //    response.PropertiesQuantity = amenity.Properties.Count();
            //else
            //    response.PropertiesQuantity = 0;
            #endregion


            return new Response<AmenityDTO> { Data = response, Succeeded = true};
        }
    }
}