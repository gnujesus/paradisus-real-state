using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using System.Net;

namespace RealStateApp.Core.Application.Features.AmenityF.Commands
{
    public sealed record DeleteAmenityByIdCommand(string Id, bool TrackChanges) : IRequest<Response<int>>;

    internal sealed class DeleteAmenityByIdCommandHandler : IRequestHandler<DeleteAmenityByIdCommand, Response<int>>
    {
        private readonly IRepositoryManager _repositoryManager;
        public DeleteAmenityByIdCommandHandler(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;

        public async Task<Response<int>> Handle(DeleteAmenityByIdCommand command, CancellationToken cancellationToken)
        {
            var amenity = await _repositoryManager.Amenity.GetByIdAsync(command.Id);
            if (amenity == null) 
                throw new ApiException($"The amenity with id: {command.Id} doesn't exist in the database.", (int)HttpStatusCode.NotFound);

            await _repositoryManager.Amenity.DeleteAsync(amenity);

            return new Response<int>(amenity.Id);
        }
    }
}
