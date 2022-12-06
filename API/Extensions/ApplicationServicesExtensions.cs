using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Services;
using Application.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;

namespace API.Extensions;
public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServicesExtensions(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IFileUploader, LocalFileUploader>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthenticateUser, JWtUserAuthentication>();
        return services;
    }
}
