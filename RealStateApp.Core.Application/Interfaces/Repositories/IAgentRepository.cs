using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Interfaces.Repositories
{
    public interface IAgentRepository : IGenericRepositoryAsync<Agent>
    {
        Task<Property> GetAgentProperty(string agentId, bool trackChanges = false);
        Task<Agent> ChangeStatus(string agentId, bool status);
    }
}
