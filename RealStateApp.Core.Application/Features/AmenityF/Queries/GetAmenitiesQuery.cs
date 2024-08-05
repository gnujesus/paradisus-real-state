using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.AmenityDTOs;

namespace RealStateApp.Core.Application.Features.AmenityF.Queries
{
    public sealed record GetAmenitiesQuery(bool TrackChanges) : IRequest<IEnumerable<AmenityDTO>>;
}
