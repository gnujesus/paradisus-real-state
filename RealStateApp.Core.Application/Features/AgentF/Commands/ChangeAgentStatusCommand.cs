using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.AgentDTOs;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.UserModels;
using RealStateApp.Core.Application.Wrappers;
using System.Net;

namespace RealStateApp.Core.Application.Features.AgentF.Commands
{
    public sealed record ChangeAgentStatusCommand(string Id, bool status, bool TrackChanges = false) : IRequest<Response<AgentDTO>>;

    internal sealed class ChangeAgentStatusCommandHandler : IRequestHandler<ChangeAgentStatusCommand, Response<AgentDTO>>
    {
        private readonly IRepositoryManager _repository;
        public readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ChangeAgentStatusCommandHandler(IRepositoryManager repository, IUserService userService, IMapper mapper)
        {
            _repository = repository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<Response<AgentDTO>> Handle(ChangeAgentStatusCommand request, CancellationToken cancellationToken)
        {
            //var agentUser = _repository.Agent.GetByIdAsync(request.Id);
            var agentUser = _userService.GetUserByIdAsync(request.Id);

            if (agentUser is null)
                throw new ApiException($"The agent with id: {request.Id} doesn't exist in the database.", (int)HttpStatusCode.NotFound);

            var agent = _mapper.Map<SaveUserViewModel>(agentUser);
            agent.IsActive = request.status;
           
            await _userService.UpdateUserAsync(agent);

            var response = _mapper.Map<AgentDTO>(agent);

            return new Response<AgentDTO> { Data = response, Succeeded = true };
        }
    }
}
