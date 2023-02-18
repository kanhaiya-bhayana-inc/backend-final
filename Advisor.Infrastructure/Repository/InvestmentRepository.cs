using Advisor.Core.Domain.DTOS;
using Advisor.Core.Domain.Models;
using Advisor.Core.Interfaces.Repositories;
using Advisor.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advisor.Infrastructure.Repository
{
    public class InvestmentRepository : IInvestmentRepository
    {
        private readonly UserDbContext _userDbContext;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static Random random = new Random();

        public InvestmentRepository(UserDbContext userDbContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _userDbContext = userDbContext;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public string AddInvestment(InvestmentDTO request, int id)
        {
            try
            {
               var advObj = _userDbContext.Usersd.FirstOrDefault(x => x.UserID== id);

                if (advObj == null) { return "Some error has occured, try again."; }

                InvestorInfo obj1 = new InvestorInfo();
                InvestmentType obj2 = new InvestmentType();
                InvestmentStrategy obj3 = new InvestmentStrategy();

                var currTime = DateTime.Now;

                obj1.UserID= advObj.UserID;
                obj1.InvestmentName= request.InvestmentName;
                obj1.Active= request.Active;
                obj1.CreatedDate = currTime;
                obj1.ModifiedDate = currTime;
                obj1.ModifiedBy = advObj.AdvisorID;
                obj1.DeletedFlag= 0;

                _userDbContext.InvestorInfos.Add(obj1);
                _userDbContext.SaveChanges();


                obj2.InvestmentTypeName = request.InvestmentTypeName;
                obj2.CreatedDate = currTime;
                obj2.ModifiedDate = currTime;
                obj2.ModifiedBy = advObj.AdvisorID;
                obj2.DeletedFlag = 0;

                _userDbContext.InvestmentTypes.Add(obj2);
                _userDbContext.SaveChanges();


                InvestorInfo obj1n = _userDbContext.InvestorInfos.First();

                obj3.AccountID = request.AccountID;
                obj3.ModifiedBy = advObj.AdvisorID;
                obj3.StrategyName = request.StrategyName;
                obj3.DeletedFlag = 0;
                obj3.ModifiedDate = currTime;
                obj3.ModelAPLID = "------";
                obj3.InvestmentAmount = request.InvestmentAmount;
                obj3.InvestmentTypeID = obj2.InvestmentTypeID;
                obj3.InvestorInfoID = obj1.InvestorInfoID;
                _userDbContext.investmentStrategies.Add(obj3);
                _userDbContext.SaveChanges();


                return "Investment added successfully";
            }
            catch(Exception) { throw; }
        }
    }
}
