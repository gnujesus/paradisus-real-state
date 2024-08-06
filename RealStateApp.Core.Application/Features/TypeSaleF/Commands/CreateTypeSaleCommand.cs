using AutoMapper;
using MediatR;
using RealStateApp.Core.Application.DataTransferObjects.TypeSaleDTOs;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Features.TypeSaleF.Commands
{
    public sealed record CreateTypeSaleCommand(TypeSaleForCreationDTO TypeSale) : IRequest<Response<TypeSaleDTO>>;

    public class CreateTypeSaleCommandHandler : IRequestHandler<CreateTypeSaleCommand, Response<TypeSaleDTO>>
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CreateTypeSaleCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<Response<TypeSaleDTO>> Handle(CreateTypeSaleCommand request, CancellationToken cancellationToken)
        {
            var typeSaleEntity = _mapper.Map<TypeSale>(request.TypeSale);

            return new Response<TypeSaleDTO>(data: _mapper.Map<TypeSaleDTO>(await _repositoryManager.TypeSale.AddAsync(typeSaleEntity)));
        }
    }
}
