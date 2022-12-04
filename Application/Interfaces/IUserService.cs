using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Application.Interfaces;

public interface IUserService
{

    public Task<User> AuthenticateUser(User user, string password);
    public string GetActualUserId();
    public Task<User> GetActualUser();
    public void SetActualUserId(string id);

}