using MediatR;

namespace RealStateApp.Core.Application.Features.AmenityF.Notifications
{
    public sealed record AmenityDeletedNotification(string Id, bool TrackChanges) : INotification;
}
