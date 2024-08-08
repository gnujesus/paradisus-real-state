using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Application.DataTransferObjects.PropertyDTOs;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.Wrappers;
using System.Net;

namespace RealStateApp.Core.Application.Features.AgentF.Queries
{
    public sealed record GetAgentPropertyQuery(string AgentId, bool TrackChanges) : IRequest<Response<PropertyDTO>>;
    internal sealed class GetAgentPropertyQueryHandler : IRequestHandler<GetAgentPropertyQuery, Response<PropertyDTO>>
    {
        private readonly IRepositoryManager _repository;
        public readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetAgentPropertyQueryHandler(IRepositoryManager repository, IUserService userService, IMapper mapper)
        {
            _repository = repository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<Response<PropertyDTO>> Handle(GetAgentPropertyQuery request, CancellationToken cancellationToken)
        {
            //var property = await _repository.Agent.GetAgentProperty(request.AgentId);
            var property = (await _repository.Property.GetAllAsync())
                .Where(p => EF.Property<string>(p, "UserId") == request.AgentId).FirstOrDefault();

            if (property is null)
                throw new ApiException($"The property of the agent with id: {request.AgentId} doesn't exist in the database.", (int)HttpStatusCode.NotFound);

            var propertyDto = _mapper.Map<PropertyDTO>(property);

            return new Response<PropertyDTO>(propertyDto);
        }
    }
}
