namespace RealStateApp.Core.Application.Exceptions.Amenity
{
    public sealed class AmenityNotFoundException : ApiException
    {
        public AmenityNotFoundException(string amenityId, int errorCode)
            : base($"The amenity with id: {amenityId} doesn't exist in the database.", errorCode)
        {

        }
    }
}
