using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.AmenityF.Commands.DeleteAmenity
{
    /// <summary>
    /// Parámetros para la eliminacion de una mejora
    /// </summary> 
    public class DeleteAmenityByIdCommand: IRequest<Response<int>>
    {
        [SwaggerParameter(Description = "El id de la mejora que se desea eliminar")]
        public string Id { get; set; }
    }

    public class DeleteAmenityByIdCommandHandler : IRequestHandler<DeleteAmenityByIdCommand, Response<int>>
    {
        private readonly IAmenityAsync _amenityRepository;
        public DeleteAmenityByIdCommandHandler(IAmenityAsync amenityRepository)
        {
            _amenityRepository = amenityRepository;
        }
        public async Task<Response<int>> Handle(DeleteAmenityByIdCommand command, CancellationToken cancellationToken)
        {
            var amenity = await _amenityRepository.GetByIdAsync(command.Id);
            if (amenity == null) throw new ApiException($"Amenity not found.", (int)HttpStatusCode.NotFound);
            await _amenityRepository.DeleteAsync(amenity);
            return new Response<int>(amenity.Id);
        }
    }
}
