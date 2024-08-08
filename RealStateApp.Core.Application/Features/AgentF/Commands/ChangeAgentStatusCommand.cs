using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.AgentDTOs;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using System.Net;

namespace RealStateApp.Core.Application.Features.AgentF.Commands
{
    public sealed record ChangeAgentStatusCommand(string Id, bool status, bool TrackChanges = false) : IRequest<Response<AgentDTO>>;

    internal sealed class ChangeAgentStatusCommandHandler : IRequestHandler<ChangeAgentStatusCommand, Response<AgentDTO>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public ChangeAgentStatusCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<AgentDTO>> Handle(ChangeAgentStatusCommand request, CancellationToken cancellationToken)
        {
            var agentUser = _repository.Agent.GetByIdAsync(request.Id);

            if (agentUser is null)
                throw new ApiException($"The agent with id: {request.Id} doesn't exist in the database.", (int)HttpStatusCode.NotFound);

            var agent = _mapper.Map<Agent>(agentUser);
            agent.IsActive = request.status;

            await _repository.Agent.UpdateAsync(agent, request.Id);

            var response = _mapper.Map<AgentDTO>(agent);

            return new Response<AgentDTO> { Data = response, Succeeded = true };
        }
    }
}
