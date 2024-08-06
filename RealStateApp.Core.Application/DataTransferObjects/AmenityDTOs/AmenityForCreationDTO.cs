namespace RealStateApp.Core.Application.DataTransferObjects.AmenityDTOs
{
    public class AmenityForCreationDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> PropertiesIds { get; set; }
    }
}
