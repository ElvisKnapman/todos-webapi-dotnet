using Todos.Api.Models;

namespace Todos.Api.Services;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
