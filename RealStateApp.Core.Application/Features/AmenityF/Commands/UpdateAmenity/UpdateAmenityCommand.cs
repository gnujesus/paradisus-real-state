using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.AmenityF.Commands.UpdateAmenity
{
    public class UpdateAmenityCommand: IRequest<Response<AmenityUpdateResponse>>
    {
        [SwaggerParameter(Description = "El id de la mejora que se quiere actualizar")]
        public string Id { get; set; }

        /// <example>Consolas</example>
        [SwaggerParameter(Description = "El nuevo nombre de la mejora")]
        public string Name { get; set; }

        /// <example>Dispositivos electronicos de entretenimiento</example>
        [SwaggerParameter(Description = "La nueva descripcion de la mejora")]
        public string? Description { get; set; }
    }

    public class UpdateAmenityCommandHandler : IRequestHandler<UpdateAmenityCommand, Response<AmenityUpdateResponse>>
    {
        private readonly IAmenityAsync _amenityRepository;
        private readonly IMapper _mapper;

        public UpdateAmenityCommandHandler(IAmenityAsync amenityRepositor, IMapper mapper)
        {
            _amenityRepository = amenityRepositor;
            _mapper = mapper;
        }
        public async Task<Response<AmenityUpdateResponse>> Handle(UpdateAmenityCommand command, CancellationToken cancellationToken)
        {
            var amenity = await _amenityRepository.GetByIdAsync(command.Id);

            if (amenity == null)
            {
                throw new ApiException($"Amenity not found.", (int)HttpStatusCode.NotFound);
            }
            else
            {
                amenity = _mapper.Map<Amenity>(command);
                await _amenityRepository.UpdateAsync(amenity, amenity.Id);
                var amenityVm = _mapper.Map<AmenityUpdateResponse>(amenity);
                return new Response<AmenityUpdateResponse>(amenityVm);
            }
        }
    }
}
