using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.AgentDTOs;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.Wrappers;

namespace RealStateApp.Core.Application.Features.AgentF.Queries
{
    public sealed record GetAgentsQuery(bool TrackChanges) : IRequest<Response<IEnumerable<AgentDTO>>>;

    public class GetAgentsQueryHandler : IRequestHandler<GetAgentsQuery, Response<IEnumerable<AgentDTO>>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetAgentsQueryHandler(IRepositoryManager repository, IUserService userService, IMapper mapper)
        {
            _repository = repository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<AgentDTO>>> Handle(GetAgentsQuery request, CancellationToken cancellationToken)
        {
            var agents = await _userService.GetAllAgentsAsync();

            //var allAgents = (await _repository.Agent.GetAllAsync());

            //if (allAgents.Count == 0)
            //{
            //    return new Response<IEnumerable<AgentDTO>>() { Message = "No agents were found." };
            //}

            IEnumerable<AgentDTO> agentsDTOs = null!;
            agentsDTOs = agents.Select(_mapper.Map<AgentDTO>);

            agentsDTOs.Select(async (a) =>
            {
                a.PropertiesAmount = (await _repository.Property.GetAllAsync()).Where(p => p.UserId == a.Id).ToList().Count();
            }).ToList();

            return new Response<IEnumerable<AgentDTO>>(agentsDTOs) { Succeeded = true };
        }
    }
}
