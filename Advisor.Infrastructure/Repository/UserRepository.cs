using Advisor.Core.Domain.Models;
using Advisor.Core.Interfaces.Repositories;
using Advisor.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advisor.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _userDbContext;

        public UserRepository(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        public Users CreateUser(Users users)
        {
            try
            {
                var user = new Users()
                {
                    UserID = users.UserID,
                    FirstName = users.FirstName,
                    LastName = users.LastName,
                    Email = users.Email,
                    RoleID = users.RoleID,
                    AdvisorID = users.AdvisorID,
                    AgentID = users.AgentID,
                    ClientID = users.ClientID,
                    City = users.City,
                    State = users.State,
                    Active = users.Active,
                    Address = users.Address,
                    Company = users.Company,
                    CreatedDate = users.CreatedDate,
                    ModifiedDate = users.ModifiedDate,
                    ModifiedBy = users.ModifiedBy,
                    DeletedFlag = users.DeletedFlag,
                    Phone = users.Phone
                };

                _userDbContext.Usersd.Add(user);
                _userDbContext.SaveChanges();
                return users;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Users>? DeleteUser(int id)
        {
            try
            {
                Users user = _userDbContext.Usersd.FirstOrDefault(u => u.UserID == id);
                _userDbContext.Usersd.Remove(user);
                return _userDbContext.Usersd.ToList();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<Users>? GetAllUsers()
        {
            try
            {
                return _userDbContext.Usersd.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Users? GetUserById(int id)
        {
            try
            {
                return _userDbContext.Usersd.FirstOrDefault(x => x.UserID == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Users? UpdateUser(Users users, int id)
        {
            try
            {
                Users user = _userDbContext.Usersd.FirstOrDefault(x => x.UserID == id);
                user.FirstName = users.FirstName;
                user.LastName = users.LastName;
                user.Email = users.Email;
                user.Address = users.Address;
                user.City = users.City;
                user.State = users.State;
                user.Active = users.Active;
                user.AdvisorID = users.AdvisorID;
                user.AgentID = users.AgentID;
                user.Company = users.Company;
                user.ClientID = users.ClientID;
                user.Phone = users.Phone;
                user.CreatedDate = users.CreatedDate;
                user.ModifiedDate = users.ModifiedDate;
                user.RoleID = users.RoleID;
                user.ModifiedBy = users.ModifiedBy;
                user.DeletedFlag = users.DeletedFlag;
                _userDbContext.SaveChanges();
                return users;
            }
            catch (Exception) { throw; }
        }
    }
}

