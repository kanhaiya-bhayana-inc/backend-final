using Advisor.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advisor.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<Users?> GetUserById(int id);
        Task<List<Users>?> GetAllUsers();
        Task<Users> CreateUser(Users users);
        Task<Users?> UpdateUser(Users users, int id);
        Task<List<Users>?> DeleteUser(int id);
    }
}
