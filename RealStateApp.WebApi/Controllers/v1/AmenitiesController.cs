﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.DataTransferObjects.AmenityDTOs;
using RealStateApp.Core.Application.Features.AmenityF.Commands;
using RealStateApp.Core.Application.Features.AmenityF.Queries;

namespace RealStateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin")]
    public class AmenitiesController : BaseApiController
    {
        private readonly ISender _sender;
        private readonly IPublisher _publisher;

        public AmenitiesController(ISender sender, IPublisher publisher)
        {
            _sender = sender;
            _publisher = publisher;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AmenityWithoutPropertiesDTO>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAmenities()
        {
            var amenities = await _sender.Send(new GetAmenitiesQuery(TrackChanges: false));

            return Ok(amenities);
        }

        [HttpGet("{id}", Name = "AmenityById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AmenityWithoutPropertiesDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAmenity(string id)
        {
            var typeSale = await _sender.Send(new GetAmenityQuery(id, TrackChanges: false));

            return Ok(typeSale);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAmenity(AmenityForCreationDTO typeSaleForCreationDto) // [FromBody]
        {
            var typeSale = await _sender.Send(new CreateAmenityCommand(typeSaleForCreationDto));

            return CreatedAtRoute("AmenityById", new { id = typeSale.Data.Id }, typeSale);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AmenityWithoutPropertiesDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAmenity(string id, AmenityForUpdateDTO typeSaleForUpdateDto)
        {
            if (typeSaleForUpdateDto is null)
                return BadRequest("AmenityForUpdateDto object is null");

            var updatedAmenity = await _sender.Send(new UpdateAmenityCommand(id, typeSaleForUpdateDto, TrackChanges: true));

            return Ok(updatedAmenity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAmenity(string id)
        {
            await _publisher.Publish(new AmenityDeletedNotification(id, TrackChanges: false));

            return NoContent();
        }
    }
}
