using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using System.Net;

namespace RealStateApp.Core.Application.Features.TypeSaleF.Commands
{
    public sealed record TypeSaleDeletedNotification(string Id, bool TrackChanges) : INotification;

    internal sealed class DeleteTypeSaleByIdCommandHandler : INotificationHandler<TypeSaleDeletedNotification>
    {
        private readonly IRepositoryManager _repositoryManager;
        public DeleteTypeSaleByIdCommandHandler(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

        public async Task Handle(TypeSaleDeletedNotification notification, CancellationToken cancellationToken)
        {
            var typeSale = await _repositoryManager.TypeSale.GetByIdAsync(notification.Id, notification.TrackChanges);
            if (typeSale == null) 
                throw new ApiException($"The type sale with id: {notification.Id} doesn't exist in the database.", (int)HttpStatusCode.NotFound);

            await _repositoryManager.TypeSale.DeleteAsync(typeSale);
        }
    }
}
