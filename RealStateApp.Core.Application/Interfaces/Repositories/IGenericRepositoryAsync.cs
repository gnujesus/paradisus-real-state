using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepositoryAsync<Entity> where Entity : class
    {
        Task<List<Entity>> GetAllAsync();
        Task<Entity> GetByIdAsync(string id);
        Task<Entity> AddAsync(Entity entity);
        Task UpdateAsync(Entity entity, string id);
        Task DeleteAsync(Entity entity);
    }
}
