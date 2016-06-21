using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using TIC.PuppyLove.Services.Contracts.Common;
using TIC.PuppyLove.Services.Logic.Profile;
using TIC.PuppyLove.Services.Contracts.Profile.Data;
using Ventera.VAF.Core.Configuration;
using Newtonsoft.Json;
using TIC.PuppyLove.Helpers;

namespace TIC.PuppyLove.Profile.Service.UnitTest
{
    [TestClass]
    public class ProfileUnitTest
    {
        [TestMethod]
        public void TestLogin ()
        {
           

            var response = new LoginResponse();
            UserProfileRequest request = new UserProfileRequest { UserName = "DPuddy", ProfileAttributeType = ProfileAttributeTypeEnum.All };

            var dispatchUrl = VAFConfiguration.GetAppSettingValue("ProfileUrl");
            var fullUrl = new StringBuilder().AppendFormat("{0}/{1}",
                dispatchUrl,
                VAFConfiguration.GetAppSettingValue("ProfileProcessOp"));

            var jsonHelper = new JsonHelper();
            response = jsonHelper.MakePostRequest<UserProfileRequest, LoginResponse>(fullUrl.ToString(),request);

            //Successful if response object status = true
            Assert.IsTrue(response.Status);

        }
    }
}
