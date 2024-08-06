using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.DataTransferObjects.TypeSaleDTOs;
using RealStateApp.Core.Application.Features.TypeSaleF.Commands;
using RealStateApp.Core.Application.Features.TypeSaleF.Queries;

namespace RealStateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin")]
    public class TypeSalesController : BaseApiController
    {
        private readonly ISender _sender;
        private readonly IPublisher _publisher;

        public TypeSalesController(ISender sender, IPublisher publisher)
        {
            _sender = sender;
            _publisher = publisher;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypeSaleDTO>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTypeSales()
        {
            var typeSales = await _sender.Send(new GetTypeSalesQuery(TrackChanges: false));

            return Ok(typeSales);
        }

        [HttpGet("{id}", Name = "TypeSaleById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TypeSaleDTO))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTypeSale(string id)
        {
            var typeSale = await _sender.Send(new GetTypeSaleQuery(id, TrackChanges: false));

            return Ok(typeSale);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTypeSale(TypeSaleForCreationDTO typeSaleForCreationDto) // [FromBody]
        {
            var typeSale = await _sender.Send(new CreateTypeSaleCommand(typeSaleForCreationDto));

            return CreatedAtRoute("TypeSaleById", new { id = typeSale.Data.Id }, typeSale);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TypeSaleDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTypeSale(string id, TypeSaleForUpdateDTO typeSaleForUpdateDto)
        {
            if (typeSaleForUpdateDto is null)
                return BadRequest("TypeSaleForUpdateDto object is null");

            var updatedTypeSale = await _sender.Send(new UpdateTypeSaleCommand(id, typeSaleForUpdateDto, TrackChanges: true));

            return Ok(updatedTypeSale);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTypeSale(string id)
        {
            await _publisher.Publish(new TypeSaleDeletedNotification(id, TrackChanges: false));

            return NoContent();
        }
    }
}
