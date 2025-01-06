using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetSpace.Community.Infrastructure.Community;
using NetSpace.Community.Infrastructure.CommunityPost;
using NetSpace.Community.Infrastructure.CommunityPostUserComment;
using NetSpace.Community.Infrastructure.User;
using NetSpace.Community.UseCases.Common;
using NetSpace.Community.UseCases.Community;
using NetSpace.Community.UseCases.CommunityPost;
using NetSpace.Community.UseCases.CommunityPostUserComment;
using NetSpace.Community.UseCases.User;

namespace NetSpace.Community.Infrastructure.Common.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<NetSpaceDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<ICommunityRepository, CommunityRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICommunityPostRepository, CommunityPostRepository>();
        services.AddScoped<ICommunityPostUserCommentRepository, CommunityPostUserCommentRepository>();

        services.AddScoped<ICommunityReadonlyRepository, CommunityRepository>();
        services.AddScoped<IUserReadonlyRepository, UserRepository>();
        services.AddScoped<ICommunityPostReadonlyRepository, CommunityPostRepository>();
        services.AddScoped<ICommunityPostUserCommentReadonlyRepository, CommunityPostUserCommentRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IReadonlyUnitOfWork, ReadonlyUnitOfWork>();

        return services;
    }
}
