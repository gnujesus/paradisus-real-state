using MediatR;

namespace RealStateApp.Core.Application.Features.AmenityF.Commands
{
    public record DeleteCompanyCommand(string Id, bool TrackChanges) : IRequest;
}
