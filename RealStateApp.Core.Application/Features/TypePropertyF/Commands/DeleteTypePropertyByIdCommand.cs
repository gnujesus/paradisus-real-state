using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using System.Net;

namespace RealStateApp.Core.Application.Features.TypePropertyF.Commands
{
    public sealed record TypePropertyDeletedNotification(string Id, bool TrackChanges) : INotification;

    internal sealed class DeleteTypePropertyByIdCommandHandler : INotificationHandler<TypePropertyDeletedNotification>
    {
        private readonly IRepositoryManager _repositoryManager;
        public DeleteTypePropertyByIdCommandHandler(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

        public async Task Handle(TypePropertyDeletedNotification notification, CancellationToken cancellationToken)
        {
            var typeProperty = await _repositoryManager.TypeProperty.GetByIdAsync(notification.Id, notification.TrackChanges);
            if (typeProperty == null)
                throw new ApiException($"The type property with id: {notification.Id} doesn't exist in the database.", (int)HttpStatusCode.NotFound);

            await _repositoryManager.TypeProperty.DeleteAsync(typeProperty);
        }
    }
}
