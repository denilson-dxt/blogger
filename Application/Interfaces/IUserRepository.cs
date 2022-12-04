using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces;

public interface IUserRepository
{
    public Task<IdentityResult> CreateUserAsync(User user, string password);
    public Task<User> GetUserById(string id);
    public Task<User> UpdateUser(User user);
    public Task<User> UpdateUserPassword(User user, string password);

    public Task<User> DeleteUser(User user);
    public Task<IEnumerable<User>> ListAllUsers();
    public Task<User> GetUserByEmail(string email);
    public Task<User> FilterOneUser(Expression<Func<User, bool>> query = null);
    public Task<IEnumerable<User>> FilterManyUsers(Expression<Func<User, bool>> query = null);
    public Task<int> Complete();
}
