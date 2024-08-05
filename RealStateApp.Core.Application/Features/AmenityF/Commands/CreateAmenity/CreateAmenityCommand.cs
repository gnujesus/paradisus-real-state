using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateApp.Core.Application.Features.AmenityF.Commands.CreateAmenity
{

    /// <summary>
    /// Parámetros para la creacion de una Mejora
    /// </summary> 
    public class CreateAmenityCommand: IRequest<Response<int>>
    {
        /// <example>Piscina</example>
        [SwaggerParameter(Description = "El nombre de la Mejora")]
        public string Name { get; set; }

        /// <example>Construcción que almacena agua</example>
        [SwaggerParameter(Description = "La descripcion de la mejora")]
        public string? Description { get; set; }
    }

    public class CreateAmenityCommandHandler : IRequestHandler<CreateAmenityCommand, Response<int>>
    {
        private readonly IAmenityAsync _amenityRepository;
        private readonly IMapper _mapper;
        private readonly ICodeGeneratorService _codegenService;
        public CreateAmenityCommandHandler(IAmenityAsync amenityRepository, IMapper mapper, ICodeGeneratorService codegenService)
        {
            _amenityRepository = amenityRepository;
            _mapper = mapper;
            _codegenService = codegenService;
        }

        public async Task<Response<int>> Handle(CreateAmenityCommand request, CancellationToken cancellationToken)
        {

            var amenity = _mapper.Map<Amenity>(request);
            amenity.Id = await _codegenService.GenerateUniqueCodeAsync();

            await _amenityRepository.AddAsync(amenity);
            return new Response<int>(amenity.Id);
        }
    }
}
