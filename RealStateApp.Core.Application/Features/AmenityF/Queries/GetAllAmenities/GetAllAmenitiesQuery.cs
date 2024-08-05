using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.ViewModels.AmenityModels;
using RealStateApp.Core.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.AmenityF.Queries.GetAllAmenities
{
    /// <summary>
    /// Parámetros para el listado de mejores
    /// </summary> 
    public class GetAllAmenitiesQuery : IRequest<Response<IList<AmenityViewModel>>>
    {

    }

    public class GetAllAmenitiesQueryHandler : IRequestHandler<GetAllAmenitiesQuery, Response<IList<AmenityViewModel>>>
    {
        private readonly IAmenityAsync _amenityAsync;

        public GetAllAmenitiesQueryHandler(IAmenityAsync amenityAsync)
        {
            _amenityAsync = amenityAsync;
        }

        public async Task<Response<IList<AmenityViewModel>>> Handle(GetAllAmenitiesQuery request, CancellationToken cancellationToken)
        {

            return await GetAllViewModelWithInclude();
        }

        private async Task<Response<IList<AmenityViewModel>>> GetAllViewModelWithInclude()
        {
            var amenityList = await _amenityAsync.GetAllAsync();

            if (amenityList == null || amenityList
                .Count == 0) throw new ApiException($"Amenities not found.", (int)HttpStatusCode.NotFound);

            return new Response<IList<AmenityViewModel>>(amenityList.Select(amenity => new AmenityViewModel
            {
                Name = amenity.Name,
                Description = amenity.Description,
                Id = amenity.Id,
                Properties = amenity.Properties
            }).ToList());
        }
    }
}
