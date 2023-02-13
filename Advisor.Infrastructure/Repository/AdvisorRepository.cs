using Advisor.Core.Domain.DTOS;
using Advisor.Core.Domain.Models;
using Advisor.Core.Interfaces.Repositories;
using Advisor.Infrastructure.Data;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

    public class AdvisorRepository : IAdvisorRepository
    {

        private readonly UserDbContext _userDbContext;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdvisorRepository(UserDbContext userDbContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _userDbContext = userDbContext;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public AdvisorInfo CreateAdvisor(AdviserDto request)
        {
            try
            {
                //AdvisorInfo advisorInfo = new AdvisorInfo();
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                AdvisorInfo advisorInfo = new AdvisorInfo()
                {
                    Username = request.Username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Email = request.Email
                };
                _userDbContext.AdvisorInfos.Add(advisorInfo);
                _userDbContext.SaveChanges();
                return advisorInfo;
            }
        
            catch (Exception)
            {
                throw;
            }
        }

        public List<AdvisorInfo>? DeleteAdvisor(int id)
        {
            try
            {
                AdvisorInfo user = _userDbContext.AdvisorInfos.FirstOrDefault(u => u.AdvisorId == id);
                if (user == null) { return null; }
                _userDbContext.AdvisorInfos.Remove(user);
                _userDbContext.SaveChanges();
                return _userDbContext.AdvisorInfos.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AdvisorInfoToReturn? GetAdvisorById(int id)
        {
            try
            {
                var res = _userDbContext.AdvisorInfos.FirstOrDefault(x => x.AdvisorId == id);
                if (res is null) { return null; }
                AdvisorInfoToReturn obj=new AdvisorInfoToReturn();
                obj.Email = res.Email;
                obj.Username = res.Username;
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AdvisorInfoToReturn>? GetAllAdvisors()
        {
            try
            {

                var dbset = _userDbContext.AdvisorInfos.Select(x => new AdvisorInfoToReturn
                {
                    Username = x.Username,
                    Email = x.Email

                }).ToList();
                return dbset;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AdvisorInfoToReturn? UpdateAdvisor(int id, AdvisorInfoToReturn request)
        {
            try
            {
                
                var hero =  _userDbContext.AdvisorInfos.Find(id);
                if (hero is null)
                    return null;
                
                hero.Username = request.Username;
                hero.Email = request.Email;
                
                 _userDbContext.SaveChangesAsync();
                /* AdvisorInfoToReturn toreturn = new AdvisorInfoToReturn();
                toreturn.Username = request.Username;
                toreturn.Email = request.Email;*/


                return request;


            }
            catch (Exception) { throw; }
        }

        public string GetMyName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString();
                result = result.Split(' ')[1];
            }
            return result;
        }


        public string Login(AuthAdvisorDto requrest)
        {
            AdvisorInfo advObj = _userDbContext.AdvisorInfos.FirstOrDefault(u => u.Email == requrest.Email);
            if (advObj is null)
            {
                return "Advisor not found.";
            }
            if (!VerifyPasswordHash(requrest.Password, advObj.PasswordHash, advObj.PasswordSalt))
            {
                return "Wrong password.";
            }
            string token = CreateToken(advObj);

            //var refreshToken = GenerateRefreshToken();
            //SetRefreshToken(refreshToken);
            //var userName = _userDbContext.AdvisorInfos.Identity?.Name;
            return token;
        }

        private string CreateToken(AdvisorInfo advisor)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, advisor.Email),
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
    }
}
