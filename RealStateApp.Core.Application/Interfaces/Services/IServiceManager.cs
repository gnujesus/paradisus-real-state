namespace RealStateApp.Core.Application.Interfaces.Services
{
    public interface IServiceManager
    {
        IAmenityService Amenity { get; }
        IFavoriteService Favorite { get; }
        IPropertyAmenityService PropertyAmenity { get; }
        IPropertyService Property { get; }
        IPropertyImageService PropertyImage { get; }
        ITypePropertyService TypeProperty { get; }
        ITypeSaleService TypeSale { get; }
        IUserService user { get; }
        Task SaveAsync();
    }
}
