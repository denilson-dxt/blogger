using System;
using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private string _actualUserId;
    private readonly UserManager<User> _userManager;
    private readonly IUserRepository _userRepository;

    public UserService(UserManager<User> userManager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }
    public async Task<User> AuthenticateUser(User user, string password)
    {
        var result = await _userManager.CheckPasswordAsync(user, password);
        if(!result) return null;
        return user;
    }

    public async Task<User> GetActualUser()
    {
        var user = await _userRepository.GetUserById(_actualUserId);
        return user;
    }

    public string GetActualUserId()
    {
        return _actualUserId;
    }

    public void SetActualUserId(string id)
    {
        _actualUserId = id;
    }
}