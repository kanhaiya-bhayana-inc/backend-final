using Advisor.Core.Domain.DTOS;
using Advisor.Core.Domain.Models;
using Advisor.Core.Interfaces.Repositories;
using Advisor.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
               var advObj = _userDbContext.Usersd.FirstOrDefault(x => x.UserID == id);

                if (advObj == null) { return "Some error has occured with investment information, try again."; }

                InvestorInfo obj1 = new InvestorInfo();
                InvestmentType obj2 = new InvestmentType();
                InvestmentStrategy obj3 = new InvestmentStrategy();

                var currTime = DateTime.Now;

                obj1.UserID= advObj.UserID;
                obj1.InvestmentName= request.InvestmentName;
                obj1.Active= request.Active;
                obj1.CreatedDate = currTime;
                obj1.ModifiedDate = null;
                obj1.ModifiedBy = null;
                obj1.DeletedFlag= 0;

                _userDbContext.InvestorInfos.Add(obj1);
                _userDbContext.SaveChanges();

                var typeID = _userDbContext.InvestmentTypes.FirstOrDefault(x => x.InvestmentTypeName.Equals(request.InvestmentTypeName));
               /* obj2.InvestmentTypeName = request.InvestmentTypeName;
                obj2.CreatedDate = currTime;
                obj2.ModifiedDate = null;
                obj2.ModifiedBy = null;
                obj2.DeletedFlag = 0;*/

                


                //InvestorInfo obj1n = _userDbContext.InvestorInfos.First();

                obj3.AccountID = request.AccountID;
                obj3.ModifiedBy = advObj.AdvisorID;
                obj3.StrategyName = request.StrategyName;
                obj3.DeletedFlag = 0;
                obj3.ModifiedDate = null;
                obj3.ModelAPLID = "------";
                obj3.InvestmentAmount = request.InvestmentAmount;
                obj3.InvestmentTypeID = typeID.InvestmentTypeID;
                obj3.InvestorInfoID = obj1.InvestorInfoID;
                _userDbContext.investmentStrategies.Add(obj3);
                _userDbContext.SaveChanges();


                return "Investment added successfully";
            }
            catch(Exception) { throw; }
        }

        public string UpdateInvestment(InvestmentDTO request, int infoID, int strtID, int advId)
        {
            try
            {
                var invstInfoOldData = _userDbContext.InvestorInfos.FirstOrDefault(x => x.InvestorInfoID == infoID);

                if (invstInfoOldData == null) { return "Some error has occured with client information, try again."; }

                InvestorInfo obj1 = new InvestorInfo();
                InvestmentType obj2 = new InvestmentType();
                InvestmentStrategy obj3 = new InvestmentStrategy();

                var currTime = DateTime.Now;

               
                invstInfoOldData.InvestmentName = request.InvestmentName;
                invstInfoOldData.Active = request.Active;
                invstInfoOldData.ModifiedDate = null;
                invstInfoOldData.ModifiedBy = advId.ToString();
               _userDbContext.SaveChanges();

                var typeID = _userDbContext.InvestmentTypes.FirstOrDefault(x => x.InvestmentTypeName.Equals(request.InvestmentTypeName));

                var strtOldData = _userDbContext.investmentStrategies.FirstOrDefault(x => x.InvestmentStrategyID == strtID);

                strtOldData.AccountID = request.AccountID;
                strtOldData.ModifiedBy = advId.ToString();
                strtOldData.StrategyName = request.StrategyName;
                strtOldData.ModifiedDate = currTime;
                strtOldData.InvestmentAmount = request.InvestmentAmount;
                strtOldData.InvestmentTypeID = typeID.InvestmentTypeID;
                _userDbContext.SaveChanges();


                return "Investment updated successfully";
            }
            catch (Exception) { throw; }
        }

        public string DeleteInvestment(int infoID, int strtID)
        {
            try
            {
                var info = _userDbContext.InvestorInfos.FirstOrDefault(c => c.InvestorInfoID == (infoID));
                var strtegy = _userDbContext.investmentStrategies.FirstOrDefault(c => c.InvestmentStrategyID == strtID);
                if(strtegy != null && info != null)
                {
                    info.DeletedFlag = 1;
                    info.Active = 0;
                    _userDbContext.SaveChanges();

                    strtegy.DeletedFlag = 1;

                    _userDbContext.SaveChanges();
                    return "Investment Deleted successfully";
                }
                return "Something went wrong, try again later or contact admin.";

            }
            catch(Exception) { throw; }
        }

        public List<GetInvestments>? GetuserInvestments(int uID)
        {
            try
            {
                var flag = _userDbContext.InvestorInfos.Any(x => x.UserID == uID);
                Console.WriteLine("1stjkldjfldksjf");
                if (!flag) { return null; }
                Console.WriteLine("uuuuuuuu"); ;

                List<InvestorInfo> userData = _userDbContext.InvestorInfos.Where(x =>x.UserID== uID).ToList();
                List<int> userIDs = new List<int>();
                List<GetInvestments> list = new List<GetInvestments>();

                foreach (var x in userData)
                {
                    if (x.DeletedFlag == 0)
                    {
                        userIDs.Add(x.InvestorInfoID);

                    }
                }

                foreach(int i in userIDs)
                {
                    Console.WriteLine("i-s->"+ i);
                    GetInvestments clientInvestmentInfo = new GetInvestments();
                    var strategy = _userDbContext.investmentStrategies.Where(x => x.InvestorInfoID == i).ToList();
                    foreach (var s in strategy) {
                        if (s.DeletedFlag == 0)
                        {
                            Console.WriteLine("jlfj"+s.InvestmentStrategyID);
                            var info = _userDbContext.InvestorInfos.FirstOrDefault(x => x.InvestorInfoID == i);
                            clientInvestmentInfo.investmentName = info.InvestmentName;
                            clientInvestmentInfo.active = info.Active;
                            clientInvestmentInfo.inofID = info.InvestorInfoID;

                            var type = _userDbContext.InvestmentTypes.First(x=>x.InvestmentTypeID==s.InvestmentTypeID).InvestmentTypeName;
                            clientInvestmentInfo.investmentName = type;
                            clientInvestmentInfo.investmentAmount = s.InvestmentAmount;
                            clientInvestmentInfo.strategyName = s.StrategyName;
                            clientInvestmentInfo.acountID = s.AccountID;
                            clientInvestmentInfo.totalAmount = 0;
                            clientInvestmentInfo.strategyID = s.InvestmentStrategyID;
                            list.Add(clientInvestmentInfo);
                        }

                    }
                }
                return list;
            }
            catch (Exception) { throw; }
        }

       
    }
}
