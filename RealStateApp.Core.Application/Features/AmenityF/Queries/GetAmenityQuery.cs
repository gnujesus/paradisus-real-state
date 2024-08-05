using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.AmenityDTOs;

namespace RealStateApp.Core.Application.Features.AmenityF.Queries
{
    public sealed record GetAmenityQuery(string Id, bool TrackChanges) : IRequest<AmenityDTO>;
}
