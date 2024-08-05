namespace RealStateApp.Core.Application.Exceptions.Amenity
{
    public sealed class AmenityNotFoundException : NotFoundException
    {
        public AmenityNotFoundException(string companyId)
            : base($"The company with id: {companyId} doesn't exist in the database.")
        {
        }
    }
}
