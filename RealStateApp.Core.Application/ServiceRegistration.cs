﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RealStateApp.Core.Application.Interfaces.Services;
using RealStateApp.Core.Application.Mappings;
using RealStateApp.Core.Application.Services.MainServices;
using System.Reflection;

namespace RealStateApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            var mapperConfig = AutoMapperConfig.Initialize();
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<IServiceManager, ServiceManager>();
        }
    }
}
