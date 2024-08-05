using MediatR;
using RealStateApp.Core.Application.Features.AmenityF.Notifications;
using RealStateApp.Core.Application.Interfaces.Repositories;

namespace RealStateApp.Core.Application.Features.AmenityF.Handlers
{
    internal sealed class DeleteAmenityHandler : INotificationHandler<AmenityDeletedNotification>
    {
        private readonly IRepositoryManager _repository;

        public DeleteAmenityHandler(IRepositoryManager repository) => _repository = repository;

        public async Task Handle(AmenityDeletedNotification notification, CancellationToken cancellationToken)
        {
            var Amenity = await _repository.Amenity.GetAmenityAsync(notification.Id, notification.TrackChanges);
            if (Amenity is null)
                throw new AmenityNotFoundException(notification.Id);

            await _repository.Amenity.DeleteAsync(Amenity);
            await _repository.SaveAsync();
        }
    }
}
