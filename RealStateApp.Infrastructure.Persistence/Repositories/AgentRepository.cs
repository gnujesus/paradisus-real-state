using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Domain.Entities;
using RealStateApp.Infrastructure.Persistence.Contexts;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public class AgentRepository : GenericRepository<Agent>, IAgentRepository
    {
        private readonly ApplicationContext _dbContext;
        private readonly IAccountService _accountService;

        public AgentRepository(ApplicationContext dbContext, IAccountService accountService) : base(dbContext)
        {
            _dbContext = dbContext;
            _accountService = accountService;
        }

        public override async Task<List<Agent>> GetAllAsync(bool trackChanges = false)
        {
            return (await _accountService.GetAgentsAsync()).ToList();
        }

        public override async Task<Agent> GetByIdAsync(string id, bool trackChanges = false)
        {
            return await _accountService.GetAgentByIdAsync(id);
        }

        public async Task<Property> GetAgentProperty(string agentId, bool trackChanges = false)
        {
            var property = 
                !trackChanges ?
            _dbContext.Set<Property>()
                    .Where(p => EF.Property<string>(p, "UserId") == agentId)
                    .AsNoTracking().FirstOrDefaultAsync() :
            _dbContext.Set<Property>()
                    .Where(p => EF.Property<string>(p, "UserId") == agentId).FirstOrDefaultAsync();

            return await property;
        }
        public async Task<Agent> ChangeStatus(string agentId, bool status)
        {
            var updatedAgent = await _accountService.UpdateAgentAsync(agentId, status);
            return updatedAgent;
        }
    }
}
