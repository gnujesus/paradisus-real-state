using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using System.Net;

namespace RealStateApp.Core.Application.Features.AmenityF.Commands
{
    //public sealed record DeleteAmenityByIdCommand(string Id, bool TrackChanges) : IRequest<Response<int>>;
    public sealed record AmenityDeletedNotification(string Id, bool TrackChanges) : INotification;

    internal sealed class DeleteAmenityByIdCommandHandler : INotificationHandler<AmenityDeletedNotification>
    {
        private readonly IRepositoryManager _repositoryManager;
        public DeleteAmenityByIdCommandHandler(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

        public async Task Handle(AmenityDeletedNotification notification, CancellationToken cancellationToken)
        {
            var amenity = await _repositoryManager.Amenity.GetByIdAsync(notification.Id, notification.TrackChanges);
            if (amenity == null) 
                throw new ApiException($"The amenity with id: {notification.Id} doesn't exist in the database.", (int)HttpStatusCode.NotFound);

            await _repositoryManager.Amenity.DeleteAsync(amenity);
        }
    }
}
