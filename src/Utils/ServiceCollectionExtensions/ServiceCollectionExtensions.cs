﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using organisation_service.Providers;
using organisation_service.Services;
using System.Collections.Generic;

namespace organisation_service.Utils.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IOrganisationService, OrganisationService>();       
        }

        public static void RegisterProviders(this IServiceCollection services)
        {
            services.AddSingleton<IOrganisationProvider, FakeOrganisationProvider>();
            services.AddSingleton<IOrganisationProvider, VerintOrganisationProvider>();
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Organisation service API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Description = "Authorization using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });
                c.CustomSchemaIds(x => x.FullName);
            });
        }
    }
}
