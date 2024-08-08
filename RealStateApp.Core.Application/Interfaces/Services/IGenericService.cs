namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel, Model>
           where SaveViewModel : class
           where ViewModel : class
           where Model : class
    {
        Task Update(SaveViewModel vm, string id);
        Task<SaveViewModel> Add(SaveViewModel vm);
        Task Delete(string id);
        Task<SaveViewModel> GetByIdSaveViewModel(string id, bool trackChanges = false);
        Task<List<ViewModel>> GetAllViewModel(List<string> properties, bool trackChanges = false);
    }
}
