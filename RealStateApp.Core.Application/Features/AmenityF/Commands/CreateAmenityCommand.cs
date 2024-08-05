using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.AmenityDTOs;

namespace RealStateApp.Core.Application.Features.AmenityF.Commands
{
    public sealed record CreateAmenityCommand(AmenityForCreationDTO Amenity) : IRequest<AmenityDTO>;
}
