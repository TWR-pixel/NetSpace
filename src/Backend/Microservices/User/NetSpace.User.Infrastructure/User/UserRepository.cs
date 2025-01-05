using Microsoft.EntityFrameworkCore;
using NetSpace.User.Domain.User;
using NetSpace.User.UseCases.Common;
using NetSpace.User.UseCases.User;

namespace NetSpace.User.Infrastructure.User;

public sealed class UserRepository(NetSpaceDbContext dbContext) : RepositoryBase<UserEntity, Guid>(dbContext), IUserRepository, IUserReadonlyRepository
{
    public override Task UpdateAsync(UserEntity entity, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<UserEntity>> FilterAsync(UserFilterOptions filter,
                                                           PaginationOptions pagination,
                                                           SortOptions sort,
                                                           CancellationToken cancellationToken = default)
    {
        var query = DbContext.Users.AsQueryable();

        if (filter.Id != null)
            query = query.Where(u => u.Id == filter.Id);

        if (!string.IsNullOrWhiteSpace(filter.Nickname))
            query = query.Where(u => u.Nickname == filter.Nickname);

        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(u => u.Name.ToUpper() == filter.Name.ToUpper());

        if (!string.IsNullOrWhiteSpace(filter.Surname))
            query = query.Where(u => u.Surname.ToUpper() == filter.Surname.ToUpper());

        if (!string.IsNullOrWhiteSpace(filter.Email))
            query = query.Where(u => u.Email == filter.Email);

        if (!string.IsNullOrWhiteSpace(filter.LastName))
            query = query.Where(u => u.LastName.ToUpper() == filter.LastName.ToUpper());

        if (!string.IsNullOrWhiteSpace(filter.About))
            query = query.Where(u => u.About == filter.About);

        if (!string.IsNullOrWhiteSpace(filter.AvatarUrl))
            query = query.Where(u => u.AvatarUrl == filter.AvatarUrl);

        if (filter.BirthDate != null)
            query = query.Where(u => u.BirthDate == filter.BirthDate);

        if (filter.RegistrationDate != null)
            query = query.Where(u => u.RegistrationDate == filter.RegistrationDate);

        if (!string.IsNullOrWhiteSpace(filter.Hometown))
            query = query.Where(u => u.Hometown == filter.Hometown);

        if (filter.Language != null)
            query = query.Where(u => u.Language == filter.Language);

        if (filter.MaritalStatus != null)
            query = query.Where(u => u.MaritalStatus == filter.MaritalStatus);

        if (!string.IsNullOrWhiteSpace(filter.CurrentCity))
            query = query.Where(u => u.CurrentCity == filter.CurrentCity);

        if (!string.IsNullOrWhiteSpace(filter.PersonalSite))
            query = query.Where(u => u.PersonalSite == filter.PersonalSite);

        if (filter.Gender != null)
            query = query.Where(u => u.Gender == filter.Gender);

        if (!string.IsNullOrWhiteSpace(filter.SchoolName))
            query = query.Where(u => u.SchoolName == filter.SchoolName);

        if (filter.IncludePosts)
            query = query.Include(u => u.UserPosts);

        if (filter.IncludeComments)
            query = query.Include(u => u.UserPostUserComments);

        query = query
            .Skip((pagination.PageCount - 1) * pagination.PageSize)
            .Take(pagination.PageSize);

        query = sort.OrderByAscending switch
        {
            "Id" => query.OrderBy(u => u.Id),
            "Nickname" => query.OrderBy(u => u.Nickname),
            "Name" => query.OrderBy(u => u.Name),
            "Surname" => query.OrderBy(u => u.Surname),
            "LastName" => query.OrderBy(u => u.LastName),
            "Email" => query.OrderBy(u => u.Email),
            "About" => query.OrderBy(u => u.About),
            "AvatarUrl" => query.OrderBy(u => u.AvatarUrl),
            "BirthDate" => query.OrderBy(u => u.BirthDate),
            "RegistrationDate" => query.OrderBy(u => u.RegistrationDate),
            "LastLoginAt" => query.OrderBy(u => u.LastLoginAt),
            "Hometown" => query.OrderBy(u => u.Hometown),
            "Language" => query.OrderBy(u => u.Language),
            "MaritalStatus" => query.OrderBy(u => u.MaritalStatus),
            "CurrentCity" => query.OrderBy(u => u.CurrentCity),
            "PersonalSite" => query.OrderBy(u => u.PersonalSite),
            "Gender" => query.OrderBy(u => u.Gender),
            "SchoolName" => query.OrderBy(u => u.SchoolName),
            _ => query.OrderBy(u => u.Id)
        };

        query = sort.OrderByDescending switch
        {
            "Id" => query.OrderByDescending(u => u.Id),
            "Nickname" => query.OrderByDescending(u => u.Nickname),
            "Name" => query.OrderByDescending(u => u.Name),
            "Surname" => query.OrderByDescending(u => u.Surname),
            "LastName" => query.OrderByDescending(u => u.LastName),
            "Email" => query.OrderByDescending(u => u.Email),
            "About" => query.OrderByDescending(u => u.About),
            "AvatarUrl" => query.OrderByDescending(u => u.AvatarUrl),
            "BirthDate" => query.OrderByDescending(u => u.BirthDate),
            "RegistrationDate" => query.OrderByDescending(u => u.RegistrationDate),
            "LastLoginAt" => query.OrderByDescending(u => u.LastLoginAt),
            "Hometown" => query.OrderByDescending(u => u.Hometown),
            "Language" => query.OrderByDescending(u => u.Language),
            "MaritalStatus" => query.OrderByDescending(u => u.MaritalStatus),
            "CurrentCity" => query.OrderByDescending(u => u.CurrentCity),
            "PersonalSite" => query.OrderByDescending(u => u.PersonalSite),
            "Gender" => query.OrderByDescending(u => u.Gender),
            "SchoolName" => query.OrderByDescending(u => u.SchoolName),
            _ => query.OrderBy(u => u.Id)
        };

        return await query.ToListAsync(cancellationToken);
    }
}
