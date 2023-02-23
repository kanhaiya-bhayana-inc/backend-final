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
                    SortName = request.LastName + ", " + request.FirstName,
                DeletedFlag =0,
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
                //new Claim(ClaimTypes.Sid, objUser.AdvisorID),
                new Claim(ClaimTypes.Role, "Advisor")
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

        private string createClientID()
        {
            const string chars = "a1bc2de3fg5h6i7j4k8l9mn0opqrstuvwxyz";
            var newId = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            var res = _userDbContext.Usersd.Any(u => u.AdvisorID == newId);
            if (res == true)
            {
                createClientID();
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

        public string LoginAdvisor(AuthAdvisorDto request)
        {
            try
            {
                Users advObj = _userDbContext.Usersd.FirstOrDefault(u => u.Email == request.Email);
                if (advObj != null && advObj.AdvisorID != null)
                {


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
                else
                {
                    return "Advisor not found.";
                }
            }
            catch (Exception) { throw; }

            
        }

        public string LoginClient(AuthAdvisorDto request)
        {
            try
            {
                Users advObj = _userDbContext.Usersd.FirstOrDefault(u => u.Email == request.Email);
                if (advObj != null && advObj.ClientID != null)
                {


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
                else
                {
                    return "Client not found.";
                }
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
                objuser.SortName = request.LastName + ", " + request.FirstName;

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

                return "Password successfully updated.";
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
                    return "Email not found";
                }
                obj.PasswordResetToken = CreateRandomToken();
                obj.ResetTokenExpires = DateTime.Now.AddMinutes(20);
                _userDbContext.SaveChanges();
                return "Change your password within 20 minutes.";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ChangePassword()
        {
            try
            {
                var email = string.Empty;
                if (_httpContextAccessor.HttpContext != null)
                {
                    email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString();
                    email = email.Split(' ')[1];
                }
                var obj = _userDbContext.Usersd.FirstOrDefault(u => u.Email == email);
                if (obj == null)
                {
                    return "Advisor email is not found in the database or some error has occured!";
                }
                obj.PasswordResetToken = CreateRandomToken();
                obj.ResetTokenExpires = DateTime.Now.AddMinutes(20);
                _userDbContext.SaveChanges();
                return $"Check you email: {email} inbox for changing the password.";
                
            }
            catch (Exception) { throw; }
        }

        public string CreateClient(AddUserDto request)
        {
            try
            {
                string errMsg = "Without advisor client cann't be created.";
                if (_userDbContext.Usersd.Any(u => u.Email == request.Email)) { return "this client already created by another advisor, try another one."; }
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                var advId = string.Empty;
                if (_httpContextAccessor.HttpContext != null)
                {
                    advId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString();
                    advId = advId.Split(' ')[1];
                }
                var newObjadvId = _userDbContext.Usersd.FirstOrDefault(x => x.Email == advId);
                if (newObjadvId == null) { return errMsg; }
                advId = newObjadvId.AdvisorID;
                var clntID = createClientID();
                Users users = new Users()
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Address = request.Address,
                    State = request.State,
                    City = request.City,
                    Company = request.Company,
                    Phone = request.Phone,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    RoleID = 3,
                    ClientID = clntID,
                    AgentID = CreateAgentId(),
                    Active = 1,
                    CreatedDate = DateTime.Now,
                    ModifiedBy = advId,
                    ModifiedDate = DateTime.Now,
                    SortName = request.LastName + ", " + request.FirstName,
                    DeletedFlag = 0,
                    VerificationToken = CreateRandomToken()
                };
                _userDbContext.Usersd.Add(users);
                _userDbContext.SaveChanges();
                var clintId = _userDbContext.Usersd.FirstOrDefault(x => x.AdvisorID == advId);
                if (clintId == null) { return errMsg; }
                var nclintId = clintId.UserID;
                var advIdn = _userDbContext.Usersd.FirstOrDefault(x => x.ClientID == clntID);
                if (advIdn == null) { return errMsg; }
                var nadvId = advIdn.UserID;

                AdvisorClient advisorClients = new AdvisorClient();
                advisorClients.AdvisorID = newObjadvId.UserID;
                advisorClients.ClientID = users.UserID;
                _userDbContext.advisorClients.Add(advisorClients);
                _userDbContext.SaveChanges();

                //Console.WriteLine("agentID is " + CreateAgentId());
                return "Client Created Successfully.";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GetUserDto? GetUserByAuth()  // shivam
        {
            try
            {
                var email = string.Empty;
                if (_httpContextAccessor.HttpContext != null)
                {
                    email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).ToString();
                    email = email.Split(' ')[1];
                }
                var objn = _userDbContext.Usersd.FirstOrDefault(u => u.Email == email);
                if (objn == null)
                {
                    return null;
                }
                var res = _userDbContext.Usersd.FirstOrDefault(x => x.Email == email);
                if (res == null) { return null; }
                GetUserDto obj = new GetUserDto();
                obj.UserID = res.UserID;
                obj.SortName = res.SortName;
                obj.Email = res.Email;
                obj.AdvisorID = res.AdvisorID;
                obj.Phone = res.Phone;
                obj.Address = res.Address;
                obj.FirstName= res.FirstName;
                obj.LastName = res.LastName;
                obj.City = res.City;
                obj.State = res.State;
                obj.Company = res.Company;

                return obj;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GetAllClientDTOs>? GetAllClients(int id)
        {
            try
            {
                /*var adv = _context.Users.First(x => x.Email == email);
                var advid = adv.UserID;*/
                var flag = _userDbContext.advisorClients.Any(x => x.AdvisorID == id);
                if (!flag) { return null; }

                List<AdvisorClient> clients = _userDbContext.advisorClients.Where(x => x.AdvisorID == id).ToList();
                
                List<int> clientids = new List<int>();
                foreach (var x in clients)
                {
                    clientids.Add(x.ClientID);
                }
                List<GetAllClientDTOs> list = new List<GetAllClientDTOs>();
                foreach (int i in clientids)
                {
                    GetAllClientDTOs clientInfo = new GetAllClientDTOs();
                    Users Client = _userDbContext.Usersd.First(c => c.UserID == i);
                    if (Client.DeletedFlag == 0)
                    {
                        clientInfo.UserID = i;
                        clientInfo.Address = Client.Address;
                        clientInfo.ClientID = Client.ClientID;
                        clientInfo.Email = Client.Email;
                        clientInfo.SortName = Client.SortName;
                        clientInfo.Phone = Client.Phone;
                        list.Add(clientInfo);
                    }
                    
                }
                
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<int>? GetAllIDS(int id)
        {

            try
            {
                List<int> ids = new List<int>();
                return ids;
            }
            catch (Exception)
            {
                throw;
            }


        }


        public UpdateUserDto? UpdateClientUser(int id, UpdateUserDto request) // shivam
        {
            try
            {
                var objuser = _userDbContext.Usersd.FirstOrDefault(x => x.UserID == (id));
                if (objuser != null && objuser.ClientID != null)
                {



                    objuser.FirstName = request.FirstName;
                    objuser.LastName = request.LastName;
                    objuser.Email = request.Email;
                    objuser.Address = request.Address;
                    objuser.City = request.City;
                    objuser.State = request.State;
                    objuser.Company = request.Company;
                    objuser.Phone = request.Phone;
                    objuser.ModifiedDate = DateTime.Now;
                    objuser.SortName = request.LastName + ", " + request.FirstName;

                    _userDbContext.SaveChanges();
                    /* AdvisorInfoToReturn toreturn = new AdvisorInfoToReturn();
                    toreturn.Username = request.Username;
                    toreturn.Email = request.Email;*/


                    return request;
                }
                return null;


            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DeleteUser(int id)  // shivam
        {
            try
            {
                var obj = _userDbContext.Usersd.FirstOrDefault(x => x.UserID == (id));

                if (obj != null && obj.AdvisorID != null)
                {


                    _userDbContext.Usersd.Remove(obj);
                    _userDbContext.SaveChanges();

                    return "User Deleted!";
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DeleteClientUser(int id)   // shivam
        {
            try
            {
                var obj = _userDbContext.Usersd.FirstOrDefault(x => x.UserID == (id));

                if (obj != null && obj.ClientID != null)
                {

                    //_userDbContext.Usersd.Remove(obj);
                    obj.DeletedFlag = 1;
                    obj.Active = 0;
                    _userDbContext.SaveChanges();

                    return "Client Deleted!";
                }
                return null;
            

            }
            catch (Exception)
            {
                throw;
            }
        }

        public GetClientUserDto? GetClientUserById(int id) // shivam 19Feb
        {
            try
            {
                var res = _userDbContext.Usersd.FirstOrDefault(x => x.UserID == (id));
                if (res != null && res.ClientID != null)
                {

                    GetClientUserDto obj = new GetClientUserDto();
                    obj.ClientID = res.ClientID;
                    obj.SortName = res.SortName;
                    obj.Email = res.Email;
                    obj.Phone = res.Phone;

                    return obj;
                }
                return null;
            



            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

