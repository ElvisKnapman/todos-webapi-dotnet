using Todos.Api.DTOs.User;
using Todos.Api.Models;

namespace Todos.Api.Mapping;
public static class UserMappingExtensions
{
    public static UserGetDto MapToGetDto(this UserModel user)
    {
        return new UserGetDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            EmailAddress = user.EmailAddress,
            Username = user.Username,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }

    public static UserModel MapToUserModel(this UserCreateDto userToCreate)
    {
        return new UserModel()
        {
            Id = 0,
            FirstName = userToCreate.FirstName ?? "",
            LastName = userToCreate.LastName ?? "",
            EmailAddress = userToCreate.EmailAddress ?? "",
            Username = userToCreate.Username ?? ""
        };
    }
}
