﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.DataTransferObjects.AmenityDTOs;
using RealStateApp.Core.Application.Features.AmenityF.Commands;
using RealStateApp.Core.Application.Features.AmenityF.Queries;

namespace RealStateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class AmenitiesController : BaseApiController
    {
        private readonly ISender _sender;
        private readonly IPublisher _publisher;

        public AmenitiesController(ISender sender, IPublisher publisher)
        {
            _sender = sender;
            _publisher = publisher;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAmenity(AmenityForCreationDTO amenityForCreationDto) // [FromBody]
        {
            var amenity = await _sender.Send(new CreateAmenityCommand(amenityForCreationDto));

            return CreatedAtRoute("AmenityById", new { id = amenity.Data.Id }, amenity);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AmenityDTO))]
        public async Task<IActionResult> UpdateAmenity(string id, AmenityForUpdateDTO amenityForUpdateDto)
        {
            if (amenityForUpdateDto is null)
                return BadRequest("AmenityForUpdateDto object is null");

            var updatedAmenity = await _sender.Send(new UpdateAmenityCommand(id, amenityForUpdateDto, TrackChanges: true));

            return Ok(updatedAmenity);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Developer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AmenityDTO>))]
        public async Task<IActionResult> GetAmenities()
        {
            var amenities = await _sender.Send(new GetAmenitiesQuery(TrackChanges: false));

            return Ok(amenities);
        }

        [HttpGet("{id}", Name = "AmenityById")]
        [Authorize(Roles = "Admin, Developer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AmenityDTO))]
        public async Task<IActionResult> GetAmenity(string id)
        {
            var amenity = await _sender.Send(new GetAmenityQuery(id, TrackChanges: false));

            return Ok(amenity);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAmenity(string id)
        {
            await _publisher.Publish(new AmenityDeletedNotification(id, TrackChanges: false));

            return NoContent();
        }
    }
}
