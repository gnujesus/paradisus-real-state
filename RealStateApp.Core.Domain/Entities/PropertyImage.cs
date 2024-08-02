namespace RealStateApp.Core.Domain.Entities
{
    public class PropertyImage
    {
        public string Id { get; set; }
        public byte[] Image { get; set; }
        public string PropertyId { get; set; }
        public Property Property { get; set; }
    }
}
