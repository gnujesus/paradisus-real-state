using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.ViewModels.FavoritesModels
{
    public class FavoritesViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Property_Id { get; set; }
        public Property Property { get; set; }
    }
}
