using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealStateApp.Core.Application.Interfaces.Repositories;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.ViewModels.AmenityModels;
using RealStateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Core.Application.Services.MainServices
{
    public class AmenityService: GenericService<SaveAmenityViewModel, AmenityViewModel, Amenity>, IAmenityService
    {
        private readonly IAmenityAsync _amenityRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public AmenityService(IAmenityAsync amenityRepository, IHttpContextAccessor contextAccessor, IMapper mapper): base(amenityRepository,mapper)
        {
            _amenityRepository = amenityRepository;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
        }


    }
}
