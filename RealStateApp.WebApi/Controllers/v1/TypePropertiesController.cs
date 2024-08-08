using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.DataTransferObjects.TypePropertyDTOs;
using RealStateApp.Core.Application.Features.TypePropertyF.Commands;
using RealStateApp.Core.Application.Features.TypePropertyF.Queries;

namespace RealStateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TypePropertyController : BaseApiController
    {
        private readonly ISender _sender;
        private readonly IPublisher _publisher;

        public TypePropertyController(ISender sender, IPublisher publisher)
        {
            _sender = sender;
            _publisher = publisher;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTypeProperty(TypePropertyForCreationDTO typePropertyForCreationDto)
        {
            var typeProperty = await _sender.Send(new CreateTypePropertyCommand(typePropertyForCreationDto));

            return CreatedAtRoute("TypePropertyById", new { id = typeProperty.Data.Id }, typeProperty);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TypePropertyDTO))]

        public async Task<IActionResult> UpdateTypeProperty(string id, TypePropertyForUpdateDTO typePropertyForUpdateDto)
        {
            if (typePropertyForUpdateDto is null)
                return BadRequest("TypePropertyForUpdateDto object is null");

            var updatedTypeProperty = await _sender.Send(new UpdateTypePropertyCommand(id, typePropertyForUpdateDto, TrackChanges: true));

            return Ok(updatedTypeProperty);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Developer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypePropertyDTO>))]
        public async Task<IActionResult> ListTypeProperties()
        {
            var typeProperties = await _sender.Send(new GetTypePropertiesQuery(TrackChanges: false));

            return Ok(typeProperties);
        }

        [HttpGet("{id}", Name = "TypePropertyById")]
        [Authorize(Roles = "Admin, Developer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TypePropertyDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTypeProperty(string id)
        {
            var typeProperty = await _sender.Send(new GetTypePropertyQuery(id, TrackChanges: false));

            return Ok(typeProperty);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTypeProperty(string id)
        {
            await _publisher.Publish(new TypePropertyDeletedNotification(id, TrackChanges: false));

            return NoContent();
        }
    }
}
