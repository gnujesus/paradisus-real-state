using AutoMapper;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.ViewModels.AmenityModels;
using RealStateApp.Core.Application.ViewModels.FavoritesModels;
using RealStateApp.Core.Application.ViewModels.PropertyAmenityModels;
using RealStateApp.Core.Application.ViewModels.PropertyImage;
using RealStateApp.Core.Application.ViewModels.PropertyModels;
using RealStateApp.Core.Application.ViewModels.TypePropertyModels;
using RealStateApp.Core.Application.ViewModels.TypeSaleModels;
using RealStateApp.Core.Application.ViewModels.UserModels;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Mappings.Profiles
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {

            #region PropertyProfile
            CreateMap<Property, PropertyViewModel>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ForMember(dest => dest.Favorites, opt => opt.MapFrom(src => src.Favorites))
                .ForMember(dest => dest.Amenities, opt => opt.MapFrom(src => src.Amenities))
                .ForMember(dest => dest.Type_Property, opt => opt.MapFrom(src => src.Type_Property))
                .ForMember(dest => dest.Type_sale, opt => opt.MapFrom(src => src.Type_sale));

            CreateMap<SavePropertyViewModel, Property>()
                .ReverseMap()
                .ForMember(dest => dest.Images, opt => opt.Condition(src => src.Images != null))
                .ForMember(dest => dest.Favorites, opt => opt.Condition(src => src.Favorites != null))
                .ForMember(dest => dest.Amenities, opt => opt.Condition(src => src.Amenities != null))
                .ForMember(dest => dest.Type_Property, opt => opt.Ignore())
                .ForMember(dest => dest.Type_sale, opt => opt.Ignore());

            #endregion

            #region AmenityProfile
            CreateMap<Amenity, AmenityViewModel>()
                .ForMember(dest => dest.Properties, opt => opt.MapFrom(src => src.Properties));

            CreateMap<SaveAmenityViewModel, Amenity>()
                .ReverseMap()
                .ForMember(dest => dest.Properties, opt => opt.Condition(src => src.Properties != null));
            #endregion

            #region FavoritesProfile
            CreateMap<Favorites, FavoritesViewModel>()
                .ForMember(dest => dest.Property, opt => opt.MapFrom(src => src.Property));

            CreateMap<SaveFavoritesViewModel, Favorites>()
                .ReverseMap()
                .ForMember(dest => dest.Property, opt => opt.Ignore());
            #endregion

            #region PropertyImageProfile
            CreateMap<PropertyImage, PropertyImageViewModel>()
                .ForMember(dest => dest.Property, opt => opt.MapFrom(src => src.Property));

            CreateMap<SavePropertyImageViewModel, PropertyImage>()
                .ReverseMap()
                .ForMember(dest => dest.Property, opt => opt.Ignore());
            #endregion

            #region TypePropertyProfile
            CreateMap<TypeProperty, TypePropertyViewModel>()
                .ForMember(dest => dest.Properties, opt => opt.MapFrom(src => src.Properties));

            CreateMap<SaveTypePropertyViewModel, TypeProperty>()
                .ReverseMap()
                .ForMember(dest => dest.Properties, opt => opt.Condition(src => src.Properties != null));
            #endregion

            #region TypeSaleProfile
            CreateMap<TypeSale, TypeSaleViewModel>()
                .ForMember(dest => dest.Properties, opt => opt.MapFrom(src => src.Properties));

            CreateMap<SaveTypeSaleViewModel, TypeSale>()
                .ReverseMap()
                .ForMember(dest => dest.Properties, opt => opt.Condition(src => src.Properties != null));
            #endregion

            #region PropertyAmenityProfile
            CreateMap<PropertyAmenity, PropertyAmenityViewModel>()
                .ForMember(dest => dest.Property, opt => opt.MapFrom(src => src.Property))
                .ForMember(dest => dest.Amenity, opt => opt.MapFrom(src => src.Amenity));

            CreateMap<SavePropertyAmenityViewModel, PropertyAmenity>()
                .ReverseMap()
                .ForMember(dest => dest.Property, opt => opt.Ignore())
                .ForMember(dest => dest.Amenity, opt => opt.Ignore());
            #endregion


            #region UserIdentityProfile
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();
            #endregion
        }
    }
}
