using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.AgentDTOs;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;

namespace RealStateApp.Core.Application.Features.AgentF.Queries
{
    public sealed record GetAgentsQuery(bool TrackChanges) : IRequest<Response<IEnumerable<AgentDTO>>>;

    public class GetAgentsQueryHandler : IRequestHandler<GetAgentsQuery, Response<IEnumerable<AgentDTO>>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public GetAgentsQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<AgentDTO>>> Handle(GetAgentsQuery request, CancellationToken cancellationToken)
        {
            var allAgents = (await _repository.Agent.GetAllAsync());

            if (allAgents.Count == 0)
            {
                return new Response<IEnumerable<AgentDTO>>() { Message = "No agents were found." };
            }

            IEnumerable<AgentDTO> agents = null!;

            allAgents.Select(async (a) =>
            {
                a.PropertiesAmount = (await _repository.Property.GetAllAsync()).Where(p => p.UserId == a.Id).ToList().Count();
            }).ToList();

            agents = allAgents.Select(_mapper.Map<AgentDTO>);

            return new Response<IEnumerable<AgentDTO>>(agents) { Succeeded = true };
        }
    }
}
