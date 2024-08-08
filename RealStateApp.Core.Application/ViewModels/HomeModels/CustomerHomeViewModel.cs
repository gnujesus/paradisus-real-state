
using RealStateApp.Core.Application.ViewModels.PropertyModels;

namespace RealStateApp.Core.Application.ViewModels.HomeModels
{

    //Everything already declared to prevent a null reference
    public class CustomerHomeViewModel
    {
        public List<PropertyViewModel> Properties { get; set; } = new();
        public string PropertyCode { get; set; } = string.Empty;
        public string PropertyType { get; set; } = string.Empty;
        public int MinPrice { get; set; } = 0;
        public int MaxPrice { get; set; } = 0;
        public int NumberOfRooms { get; set; } = 0;
        public int NumberOfBathrooms { get; set; } = 0;
    }
}
