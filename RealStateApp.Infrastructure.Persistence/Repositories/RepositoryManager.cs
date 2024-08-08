using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Infrastructure.Identity.Services;
using RealStateApp.Infrastructure.Persistence.Contexts;

namespace RealStateApp.Infrastructure.Persistence.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationContext _repositoryContext;
        private readonly Lazy<IAmenityAsync> _amenityRepository;
        private readonly Lazy<IFavoriteAsync> _favoriteRepository;
        private readonly Lazy<IPropertyAmenityAsync> _propertyAmenityRepository;
        private readonly Lazy<IPropertyAsync> _propertyRepository;
        private readonly Lazy<IPropertyImageRepository> _propertyImageRepository;
        private readonly Lazy<ITypePropertyAsync> _typePropertyRepository;
        private readonly Lazy<ITypeSaleAsync> _typeSaleRepository;
        //private readonly Lazy<IAgentRepository> _agentRepository;

        public RepositoryManager(ApplicationContext repositoryContext, IAccountService accountService)
        {
            _repositoryContext = repositoryContext;
            _amenityRepository = new Lazy<IAmenityAsync>(() => new AmenityRepository(repositoryContext));
            _favoriteRepository = new Lazy<IFavoriteAsync>(() => new FavoritesRepository(repositoryContext));
            _propertyAmenityRepository = new Lazy<IPropertyAmenityAsync>(() => new PropertyAmenityRepository(repositoryContext));
            _propertyRepository = new Lazy<IPropertyAsync>(() => new PropertyRepository(repositoryContext));
            _propertyImageRepository = new Lazy<IPropertyImageRepository>(() => new PropertyImageRepository(repositoryContext));
            _typePropertyRepository = new Lazy<ITypePropertyAsync>(() => new TypePropertyRepository(repositoryContext));
            _typeSaleRepository = new Lazy<ITypeSaleAsync>(() => new TypeSaleRepository(repositoryContext));
            //_agentRepository = new Lazy<IAgentRepository>(() => new AgentRepository(repositoryContext, accountService));
        }

        public IAmenityAsync Amenity => _amenityRepository.Value;
        public IFavoriteAsync Favorite => _favoriteRepository.Value;
        public IPropertyAmenityAsync PropertyAmenity => _propertyAmenityRepository.Value;
        public IPropertyAsync Property => _propertyRepository.Value;
        public IPropertyImageRepository PropertyImage => _propertyImageRepository.Value;
        public ITypePropertyAsync TypeProperty => _typePropertyRepository.Value;
        public ITypeSaleAsync TypeSale => _typeSaleRepository.Value;
        //public IAgentRepository Agent => _agentRepository.Value;

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
