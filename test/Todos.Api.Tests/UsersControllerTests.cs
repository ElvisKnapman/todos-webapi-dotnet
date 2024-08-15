using NSubstitute;
using Todos.Api.Controllers;
using Todos.Api.Logging;
using Todos.Api.Services;

namespace Todos.Api.Tests.Unit;
public class UsersControllerTests
{
    private readonly ILoggerAdapter<UsersController> _logger = Substitute.For<ILoggerAdapter<UsersController>>();
    private readonly IUserService _userService = Substitute.For<IUserService>();
    private readonly ITodoService _todoService = Substitute.For<ITodoService>();
    private readonly UsersController _sut;

    public UsersControllerTests()
    {
        _sut = new UsersController(_logger, _userService, _todoService);
    }
}
