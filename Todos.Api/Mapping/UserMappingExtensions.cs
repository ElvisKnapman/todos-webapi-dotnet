using Todos.Api.DTOs.User;
using Todos.Api.Models;

namespace Todos.Api.Mapping;
public static class UserMappingExtensions
{
    public static UserGetDto MapToGetDto(this UserModel userModel)
    {
        return new UserGetDto()
        {
            Id = userModel.Id,
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
            EmailAddress = userModel.EmailAddress,
            Username = userModel.Username,
            CreatedAt = userModel.CreatedAt,
            UpdatedAt = userModel.UpdatedAt
        };
    }

    public static UserModel MapToUserModel(this UserCreateDto dto)
    {
        return new UserModel()
        {
            Id = 0,
            FirstName = dto.FirstName ?? "",
            LastName = dto.LastName ?? "",
            EmailAddress = dto.EmailAddress ?? "",
            Username = dto.Username ?? ""
        };
    }

    public static UserModel MapUserUpdates(this UserModel userModel, UserUpdateDto dto)
    {
        userModel.Id = dto.Id;
        userModel.FirstName = dto.FirstName;
        userModel.LastName = dto.LastName;
        userModel.EmailAddress = dto.EmailAddress;
        userModel.Username = dto.UserName;
        userModel.UpdatedAt = DateTime.Now;

        return userModel;
    }
}
