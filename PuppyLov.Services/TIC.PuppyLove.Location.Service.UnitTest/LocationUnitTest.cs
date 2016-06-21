using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TIC.PuppyLove.Services.Contracts.Common;
using TIC.PuppyLove.Services.Logic.Location;
using TIC.PuppyLove.Services.Contracts.Location.Data;
using TIC.PuppyLove.Services.Contracts.Config.Data;
using TIC.PuppyLove.Services.Logic.Location.Data;
using TIC.PuppyLove.Services.Logic.Config.Data;
using TIC.PuppyLove.Services.Logic.Config;
using TIC.PuppyLove.Helpers;
using Ventera.VAF.Core.Configuration;
using Ventera.VAF.Core.Helpers;
using Newtonsoft.Json;

namespace TIC.PuppyLove.Location.Service.UnitTest
{
    [TestClass]
    public class LocationUnitTest
    {
        #region Location Service

        [TestMethod]
        public void TestGetLocationsWithDistanceData()
        {
            var request = new LocationData
            {
                Latitude = Convert.ToDecimal(41.1209873650912),
                Longitude = Convert.ToDecimal(-108.0926732834745),
                UserID = 9
            };
            var dmgr = new DataManager();
            var response = new List<LocationData>();
            LogHelper.LogInformation("Here we go");

            try
            {
                response = dmgr.GetCurrentLocationsWithDistance(request);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "what happened?");
                //response.Status = false;
                response = null;
            }

            Assert.IsTrue(response != null);
        }

        [TestMethod]
        public void TestGetCurrentLocationsLogic()
        {
            var response = new LocationResponse { Status = true };
            var request = new LocationData 
            {
                Latitude = Convert.ToDecimal(43.879103),
                Longitude = Convert.ToDecimal(-103.459067)
                ,
                UserID = 9 
            };
            var logic = new LocationLogic();
            LogHelper.LogInformation("Here we go");

            try
            {
                response = logic.GetCurrentLocations(request);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "What happened?");
                response.Status = false;
            }

            Assert.IsTrue(response.Status);
        }

        [TestMethod]
        public void TestUpdateLocationServiceCall()
        {
            var response = new CommonResponse();
            LocationData request = new LocationData
            {
                Latitude = Convert.ToDecimal(43.87910312345),
                Longitude = Convert.ToDecimal(-103.4590679433833),
                UserID = 8,
                Accuracy = 122,
                Timestamp = 1460743105179
            };
            //var LocationUrl = VAFConfiguration.GetAppSettingValue("LocationUrl");
            var LocationUrl = VAFConfiguration.GetAppSettingValue("LocationUrlCloud");
            var fullUrl = new StringBuilder().AppendFormat("{0}/{1}",
                LocationUrl,
                VAFConfiguration.GetAppSettingValue("UpdateLocationOp"));

            var jsonHelper = new JsonHelper();
            try
            {
                response = jsonHelper.MakePostRequest<LocationData, CommonResponse>(fullUrl.ToString(), request);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "what happened?");
                response.Status = false;
            }

            //Successful if response object status = true
            Assert.IsTrue(response.Status);
        }

        [TestMethod]
        public void TestAddUpdateLocationLogic()
        {
            var response = new CommonResponse { Status = true };
            LocationData request = new LocationData
            {
                Latitude = Convert.ToDecimal(43.87910312345),
                Longitude = Convert.ToDecimal(-103.4590679433833),
                UserID = 8,
                Accuracy = 122,
                Timestamp = 1460743105179
            };

            var logic = new LocationLogic();

            try
            {
                response = logic.UpdateLocation(request);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "what happened?");
                response.Status = false;
            }

            //Successful if response object status = true
            Assert.IsTrue(response.Status);

        }

        [TestMethod]
        public void TestRemoveLocationViaServiceCall()
        {
            var response = new CommonResponse();
            LocationData request = new LocationData
            {
                Latitude = Convert.ToDecimal(42.12345),
                Longitude = Convert.ToDecimal(-110.765432),
                UserID = 5
            };

            //var LocationUrl = VAFConfiguration.GetAppSettingValue("LocationUrl");
            var LocationUrl = VAFConfiguration.GetAppSettingValue("LocationUrlCloud");
            var fullUrl = new StringBuilder().AppendFormat("{0}/{1}",
                LocationUrl,
                VAFConfiguration.GetAppSettingValue("RemoveLocationOp"));

            var jsonHelper = new JsonHelper();
            response = jsonHelper.MakePostRequest<LocationData, CommonResponse>(fullUrl.ToString(), request);

            //Successful if response object status = true
            Assert.IsTrue(response.Status);
        }

        [TestMethod]
        public void TestGetUserDataLogicClass()
        {
            LocationLogic logic = new LocationLogic();
            LogHelper.LogInformation("Here we go");
            LocationData request = new LocationData { Latitude = Convert.ToDecimal(38.8659233),
                Longitude = Convert.ToDecimal(-77.18818399999999), UserID = 34 };
            UserData response = new UserData();
            bool result = false;
            try
            {
                response = logic.GetUserData(request);
                result = (string.IsNullOrEmpty(response.UserName)) ? false : true;
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "What Happened?");
                if (ex.InnerException != null)
                {
                    LogHelper.LogWarning(ex.InnerException.Message);
                }
            }
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestGetSpecificLocationByUserIdDataManager()
        {
            var dmgr = new DataManager();
            LogHelper.LogInformation("Here we go");
            var request = new LocationData { UserID = 8 };
            var response = new LocationData();
            try
            {
                response = dmgr.GetLocationByUserID(request);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "What Happened?");
            }

            Assert.IsTrue(response != null &&response.UserID != null && response.UserID == request.UserID);
        }

        [TestMethod]
        public void TestGetNearestNeighborLogicClass ()
        {
            var dmgr = new DataManager();
            LogHelper.LogInformation("Here we go");
            var logic = new LocationLogic();
            var response = new LocationResponse { Status = true };
            var request = new NearestNeighborRequest 
            { 
                UserID = 9,
                //Latitude = Convert.ToDecimal(38.94490111),
                //Longitude = Convert.ToDecimal(-77.338770),
                Radius = 20000
            };

            try
            {
                response = logic.GetNearestNeighbors(request);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "What Happened?");
                response.Status = false;
            }

            Assert.IsTrue(response.Status);
         
        }

        [TestMethod]
        public void TestGetNearestNeighborServiceCall ()
        {
            var response = new LocationResponse(); 
            
            var request = new NearestNeighborRequest
            {
                UserID = 9,
                //Latitude = Convert.ToDecimal(38.94490111),
                //Longitude = Convert.ToDecimal(-77.338770),
                Radius = 20000
            };

            var LocationUrl = VAFConfiguration.GetAppSettingValue("LocationUrlCloud");
            var fullUrl = new StringBuilder().AppendFormat("{0}/{1}",
                LocationUrl,
                VAFConfiguration.GetAppSettingValue("GetNearestNeighborsOp"));

            var jsonHelper = new JsonHelper();
            try
            {
                response = jsonHelper.MakePostRequest<NearestNeighborRequest, LocationResponse>(fullUrl.ToString(), request);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "what happened?");
                response.Status = false;
            }

            //Successful if response object status = true
            Assert.IsTrue(response.Status);
        }

        #endregion
    }
}
