namespace RealStateApp.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepositoryAsync<Entity> where Entity : class
    {
        Task<List<Entity>> GetAllAsync(bool trackChanges = false);
        Task<Entity> GetByIdAsync(string id, bool trackChanges = false);
        Task<Entity> AddAsync(Entity entity);
        Task UpdateAsync(Entity entity, string id);
        Task DeleteAsync(Entity entity);
        Task<List<Entity>> GetAllWithIncludeAsync(List<string> properties, bool trackChanges = false);
    }
}
