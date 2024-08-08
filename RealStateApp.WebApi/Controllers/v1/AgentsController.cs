using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateApp.Core.Application.DataTransferObjects.AgentDTOs;
using RealStateApp.Core.Application.Features.AgentF.Commands;
using RealStateApp.Core.Application.Features.AgentF.Queries;

namespace RealStateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class AgentsController : BaseApiController
    {
        private readonly ISender _sender;
        private readonly IPublisher _publisher;

        public AgentsController(ISender sender, IPublisher publisher)
        {
            _sender = sender;
            _publisher = publisher;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Developer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AgentDTO>))]
        public async Task<IActionResult> GetAgents()
        {
            var agents = await _sender.Send(new GetAgentsQuery(TrackChanges: false));
            return Ok(agents);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Developer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentDTO))]
        public async Task<IActionResult> GetAgent(string id)
        {
            var typeSale = await _sender.Send(new GetAgentQuery(id, TrackChanges: false));
            return Ok(typeSale);
        }

        [HttpGet("GetAgentProperty/{id}")]
        [Authorize(Roles = "Admin, Developer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentDTO))]
        public async Task<IActionResult> GetAgentProperty(string id)
        {
            var typeSale = await _sender.Send(new GetAgentPropertyQuery(id, TrackChanges: false));
            return Ok(typeSale);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgentDTO))]
        public async Task<IActionResult> ChangeStatus(string id, bool status)
        {
            var updatedAgent = await _sender.Send(new ChangeAgentStatusCommand(id, status, TrackChanges: true));

            return NoContent();
        }
    }
}
