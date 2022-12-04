using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Errors;
using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;

    public UserRepository(UserManager<User> userManager)
   {
        _userManager = userManager;
    }

    public async Task<IdentityResult> CreateUserAsync(User user, string password)
    {
        return await _userManager.CreateAsync(user, password);
        //return user;
    }

    public Task<User> DeleteUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> FilterManyUsers(Expression<Func<User, bool>> query = null)
    {
        throw new NotImplementedException();
    }

    public Task<User> FilterOneUser(Expression<Func<User, bool>> query = null)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<User> GetUserById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        return user;
    }

    public async Task<IEnumerable<User>> ListAllUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        return users;
    }

    public async Task<User> UpdateUser(User user)
    {
        var result = await _userManager.UpdateAsync(user);
        if(result.Errors.Count() > 0)
            throw new ApiException(result.Errors);
        return user;
    }

     public Task<int> Complete()
    {
        throw new NotImplementedException();
    }

    public async Task<User> UpdateUserPassword(User user, string password)
    {
        await _userManager.RemovePasswordAsync(user);
        var result = await _userManager.AddPasswordAsync(user, password);
        if(result.Errors.Count() > 0)
            throw new ApiException(result.Errors);
        return user;
    }
}