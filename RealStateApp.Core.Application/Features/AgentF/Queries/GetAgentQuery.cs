﻿using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.AgentDTOs;
using RealStateApp.Core.Application.Exceptions;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.Wrappers;
using System.Net;

namespace RealStateApp.Core.Application.Features.AgentF.Queries
{
    public sealed record GetAgentQuery(string Id, bool TrackChanges) : IRequest<Response<AgentDTO>>;
    internal sealed class GetAgentHandler : IRequestHandler<GetAgentQuery, Response<AgentDTO>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetAgentHandler(IRepositoryManager repository, IUserService userService, IMapper mapper)
        {
            _repository = repository;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<Response<AgentDTO>> Handle(GetAgentQuery request, CancellationToken cancellationToken)
        {
            //var agent = await _repository.Agent.GetByIdAsync(request.Id);
            var agent = await _userService.GetUserByIdAsync(request.Id);

            if (agent is null)
                throw new ApiException($"The agent with id: {request.Id} doesn't exist in the database.", (int)HttpStatusCode.NotFound);

            var AgentDto = _mapper.Map<AgentDTO>(agent);

            AgentDto.PropertiesAmount = (await _repository.Property.GetAllAsync()).Where(p => p.UserId == agent.Id).Count();

            return new Response<AgentDTO>(AgentDto);
        }
    }
}
