using Advisor.Core.Domain.DTOS;
using Advisor.Core.Domain.Models;
using Advisor.Core.Interfaces.Repositories;
using Advisor.Infrastructure.Data;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Advisor.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _userDbContext;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static Random random = new Random();

        public UserRepository(UserDbContext userDbContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _userDbContext = userDbContext;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public Users CreateUser(AddUserDto request)
        {
            try
            {
                if (_userDbContext.Usersd.Any(u => u.Email == request.Email)) { return null; }
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var advId = CreateAdvId();
                Users users= new Users()
                {
                    Email = request.Email,
                    FirstName= request.FirstName,
                    LastName= request.LastName,
                    Address= request.Address,
                    State= request.State,
                    City= request.City,
                    Company= request.Company,
                    Phone= request.Phone,
                    PasswordHash= passwordHash,
                    PasswordSalt= passwordSalt,
                    RoleID = 2,
                    AdvisorID = advId,
                    AgentID =   CreateAgentId(),
                    Active = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = advId,
                    ModifiedDate = DateTime.Now,
                    DeletedFlag=0,
                    VerificationToken = CreateRandomToken()
                };
                _userDbContext.Usersd.Add(users);
                _userDbContext.SaveChanges();
                Console.WriteLine("agentID is " + CreateAgentId());
                return users;
            }
            catch(Exception) {
                throw;
            }
        }

        private string CreateToken(Users objUser)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, objUser.Email),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        private string CreateAgentId()
        {
            const string chars = "a1bc2de3fg5h6i7j4k8l9mn0opqrstuvwxyz";
            var newId =  new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            var res = _userDbContext.Usersd.Any(u => u.AgentID == newId);
            if (res == true)
            {
                CreateAgentId();
            }
            return newId;
        }


        private string CreateAdvId()
        {
            const string chars = "a1bc2de3fg5h6i7j4k8l9mn0opqrstuvwxyz";
            var newId = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            var res = _userDbContext.Usersd.Any(u => u.AdvisorID == newId);
            if (res == true)
            {
                CreateAdvId();
            }
            return newId;
        }

        public string UserAccVerify(string token)
        {
            var obj = _userDbContext.Usersd.FirstOrDefault(u => u.VerificationToken == token);
            if (obj == null)
            {
                return "Invalid Token";
            }

            obj.VerifiedAt = DateTime.Now;
            _userDbContext.SaveChanges();

            return "Advisor Verified Successfully";
        }

        public string Login(AuthAdvisorDto request)
        {
            try
            {
                Users advObj = _userDbContext.Usersd.FirstOrDefault(u => u.Email == request.Email);
                if (advObj is null)
                {
                    return "Advisor not found.";
                }
                if (!VerifyPasswordHash(request.Password, advObj.PasswordHash, advObj.PasswordSalt))
                {
                    return "Wrong password.";
                }
                if (advObj.VerifiedAt == null)
                {
                    return "Not Verified yet";
                }
                string token = CreateToken(advObj);
                //var refreshToken = GenerateRefreshToken();
                //SetRefreshToken(refreshToken);
                //var userName = _userDbContext.AdvisorInfos.Identity?.Name;
                return token;
            }
            catch (Exception) { throw; }

            
        }

        public string AdvisorAccVerify(string token)
        {
            try
            {
                var obj = _userDbContext.Usersd.FirstOrDefault(u => u.VerificationToken == token);
                if (obj == null)
                {
                    return "Invalid Token";
                }

                obj.VerifiedAt = DateTime.Now;
                _userDbContext.SaveChanges();

                return "Advisor Verified Successfully";
            }
            catch(Exception) { throw; } 
        }

        public UpdateUserDto? UpdateUser(string id, UpdateUserDto request)
        {
            try
            {
                var objuser = _userDbContext.Usersd.FirstOrDefault(x => x.AdvisorID == (id));
                if (objuser is null)
                    return null;

                objuser.FirstName = request.FirstName;
                objuser.LastName = request.LastName;
                objuser.Email = request.Email;
                objuser.Address= request.Address;
                objuser.City = request.City;
                objuser.State= request.State;
                objuser.Company = request.Company;
                objuser.Phone = request.Phone;
                objuser.ModifiedDate = DateTime.Now;

                _userDbContext.SaveChanges();
                /* AdvisorInfoToReturn toreturn = new AdvisorInfoToReturn();
                toreturn.Username = request.Username;
                toreturn.Email = request.Email;*/


                return request;
            }
            catch (Exception) { throw; }
        }

        public string ResetPasswordUser(UserResetPasswordDto request)
        {
            try
            {
                var obj = _userDbContext.Usersd.FirstOrDefault(u => u.PasswordResetToken == request.token);
                if (obj == null || obj.ResetTokenExpires < DateTime.Now)
                {
                    return "Token Invalid or expired, try again.";
                }

                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                obj.PasswordHash = passwordHash;
                obj.PasswordSalt = passwordSalt;
                obj.PasswordResetToken = null;
                obj.ResetTokenExpires = null;

                _userDbContext.SaveChanges();

                return "Password successfully reset.";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ForgotPasswordUser(string email)
        {
            try
            {
                var obj = _userDbContext.Usersd.FirstOrDefault(u => u.Email == email);
                if (obj == null)
                {
                    return "Advisor not found";
                }
                obj.PasswordResetToken = CreateRandomToken();
                obj.ResetTokenExpires = DateTime.Now.AddMinutes(2);
                _userDbContext.SaveChanges();
                return "Change your password within next 2 minutes.";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

