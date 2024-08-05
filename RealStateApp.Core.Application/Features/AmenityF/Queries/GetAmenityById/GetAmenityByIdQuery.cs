using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.ViewModels.AmenityModels;
using RealStateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Features.AmenityF.Queries.GetAmenityById
{
    /// <summary>
    /// Parámetros para obtener una categoria por su id
    /// </summary>  
    public class GetAmenityByIdQuery: IRequest<Response<AmenityWithoutPropertiesViewModel>>
    {
        [SwaggerParameter(Description = "El id de la Mejora que se desea seleccionar")]
        public string Id { get; set; }
    }

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetAmenityByIdQuery, Response<AmenityWithoutPropertiesViewModel>>
    {
        private readonly IAmenityAsync _amenityAsync;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(IAmenityAsync amenityAsync, IMapper mapper)
        {
            _amenityAsync = amenityAsync;
            _mapper = mapper;
        }

        public async Task<Response<AmenityWithoutPropertiesViewModel>> Handle(GetAmenityByIdQuery query, CancellationToken cancellationToken)
        {
            var amenities = await _amenityAsync.GetAllWithIncludeAsync(new List<string> { "Properties" });
            var amenity = amenities.FirstOrDefault(w => w.Id == query.Id);
            if (amenities == null) throw new ApiException($"Amenity not found.", (int)HttpStatusCode.NotFound);
            var amenityVm = _mapper.Map<AmenityWithoutPropertiesViewModel>(amenity);
            amenityVm.PropertiesQuantity = amenity.Properties.Count;
            return new Response<AmenityWithoutPropertiesViewModel>(amenityVm);
        }
    }
}
