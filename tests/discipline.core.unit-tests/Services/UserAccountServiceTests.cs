using discipline.core.Domain.UserAccounts.Entities;
using discipline.core.Persistence.Repositories.Abstractions;
using discipline.core.Services.Abstractions;
using discipline.core.Services.Commands;
using discipline.core.Services.Internals;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace discipline.core.unit_tests.Services;

public sealed class UserAccountServiceTests
{
    [Fact]
    public async Task AddAsync_GivenNotExistingUserAccount_ShouldAddByRepository()
    {
        //arrange
        var command = new NewUserCommand(Guid.NewGuid());
        _userAccountRepository
            .IsExistsAsync(command.UserId, default)
            .Returns(false);
        
        //act
        await _service.AddUserAsync(command, default);
        
        //assert
        await _userAccountRepository
            .Received(1)
            .AddAsync(Arg.Is<UserAccount>(arg
                => arg.UserId == command.UserId), default);
    }

    [Fact]
    public async Task AddAsync_GivenAlreadyExistingUserAccount_ShouldNotAddByRepository()
    {
        //arrange
        var command = new NewUserCommand(Guid.NewGuid());
        _userAccountRepository
            .IsExistsAsync(command.UserId, default)
            .Returns(true);
        
        //act
        await _service.AddUserAsync(command, default);
        
        //assert
        await _userAccountRepository
            .Received(0)
            .AddAsync(Arg.Any<UserAccount>(), default);
    }
    
    #region arrange
    private readonly ILogger<UserAccountService> _logger;
    private readonly IUserAccountRepository _userAccountRepository;
    private readonly IUserAccountService _service;

    public UserAccountServiceTests()
    {
        _logger = Substitute.For<ILogger<UserAccountService>>();
        _userAccountRepository = Substitute.For<IUserAccountRepository>();
        _service = new UserAccountService(_logger, _userAccountRepository);
    }
    #endregion
}