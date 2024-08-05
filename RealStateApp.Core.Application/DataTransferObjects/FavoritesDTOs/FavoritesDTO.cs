using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.DataTransferObjects.FavoritesDTOs
{
    public class FavoritesDTO
    {
        public string Id { get; set; }
        public string User_Id { get; set; }
        public string Property_Id { get; set; }
        public Property Property { get; set; }
    }
}
