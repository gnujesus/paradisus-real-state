using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.DataTransferObjects.PropertyDTOs;
using RealStateApp.Core.Application.Features.PropertyF.Queries;

namespace RealStateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin, Developer")]
    public class PropertiesController : BaseApiController
    {
        private readonly ISender _sender;
        private readonly IPublisher _publisher;

        public PropertiesController(ISender sender, IPublisher publisher)
        {
            _sender = sender;
            _publisher = publisher;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PropertyDTO>))]
        public async Task<IActionResult> GetProperties()
        {
            var properties = await _sender.Send(new GetPropertiesQuery(TrackChanges: false));

            return Ok(properties);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyDTO))]
        public async Task<IActionResult> GetPropertyById(string id)
        {
            var property = await _sender.Send(new GetPropertyQuery(id, TrackChanges: false));

            return Ok(property);
        }

        [HttpGet("{code}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyDTO))]
        public async Task<IActionResult> GetPropertyByCode(string code)
        {
            var property = await _sender.Send(new GetPropertyByCodeQuery(code, TrackChanges: false));

            return Ok(property);
        }
    }
}
