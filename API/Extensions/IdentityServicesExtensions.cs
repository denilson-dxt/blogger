using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace API.Extensions;

public static class IdentityServicesExtensions
{
    public static IServiceCollection AddIdentityServicesExtensions(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(options => 
        {
            options.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<DataContext>();

        return services;
    }
}