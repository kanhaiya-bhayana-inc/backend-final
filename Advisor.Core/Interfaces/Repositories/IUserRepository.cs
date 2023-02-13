using Advisor.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advisor.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Users? GetUserById(int id);
        List<Users>? GetAllUsers();
        Users CreateUser(Users users);
        Users? UpdateUser(Users users, int id);
        List<Users>? DeleteUser(int id);
    }
}
