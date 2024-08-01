namespace RealStateApp.Core.Application.Interfaces.Repositories
{
    public interface IRepositoryManager
    {
        IAmenityAsync Amenity { get; }
        IFavoriteAsync Favorite { get; }
        IPropertyAmenityAsync PropertyAmenity { get; }
        IPropertyAsync Property { get; }
        IPropertyImageRepository PropertyImage { get; }
        ITypePropertyAsync TypeProperty { get; }
        ITypeSaleAsync TypeSale { get; }
        Task SaveAsync();
    }
}
