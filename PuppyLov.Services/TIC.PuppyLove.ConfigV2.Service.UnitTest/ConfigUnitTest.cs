using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TIC.PuppyLove.Helpers;
using TIC.PuppyLove.Services.Logic.Config.Data;
using TIC.PuppyLove.Services.Logic.Config;
using TIC.PuppyLove.Services.Contracts.Common;
using TIC.PuppyLove.Services.Contracts.Config.Data;
using Ventera.VAF.Core.Configuration;
using Ventera.VAF.Core.Helpers;
using Newtonsoft.Json;

namespace TIC.PuppyLove.ConfigV2.Service.UnitTest
{
    [TestClass]
    public class ConfigUnitTest
    {
        #region Config Data

        [TestMethod]
        public void TestGetAllConfigurationsData()
        {
            List<ConfigEntry> results = new List<ConfigEntry>();
            LogHelper.LogInformation("Here we go...");
            try
            {
                var dmgr = new ConfigDataManager();
                results = dmgr.GetAllConfigurations();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "What Happened?");
                results = null;
            }

            Assert.IsFalse(results == null);
        }

        [TestMethod]
        public void TestGetSpecificConfigurationData()
        {
            List<ConfigEntry> results = new List<ConfigEntry>();

            var request = new ConfigEntry { Name = "BingApiKey" };

            //request.ID = 2;
            LogHelper.LogInformation("Here we go...");
            try
            {
                var dmgr = new ConfigDataManager();
                results = dmgr.GetSpecificConfiguration(request);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "What Happened?");
                results = null;
            }

            Assert.IsFalse(results == null);
        }

        #region Delete

        [TestMethod]
        public void TestDeleteConfigsData()
        {

            Int32 response = -1;
            ConfigMultiEntries request = new ConfigMultiEntries { ConfigEntries = new List<ConfigEntry>() };
            request.ConfigEntries.Add(new ConfigEntry { ID = 9 });
            request.ConfigEntries.Add(new ConfigEntry { ID = 10 });

            //request.ID = 2;
            LogHelper.LogInformation("Here we go...");
            try
            {
                var dmgr = new ConfigDataManager();
                response = dmgr.DeleteConfigurations(request);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "What Happened?");
            }

            Assert.IsTrue(response > 0);
        }

        [TestMethod]
        public void TestDeleteConfigsLogic()
        {
            ConfigMultiEntries request = new ConfigMultiEntries { ConfigEntries = new List<ConfigEntry>() };
            //request.ConfigEntries.Add(new ConfigEntry { ID = 15 });
            //request.ConfigEntries.Add(new ConfigEntry { ID = 16 });
            request.ConfigEntries.Add(new ConfigEntry { Name = "blah" });
            ConfigurationResponse response = new ConfigurationResponse { Status = true };

            LogHelper.LogInformation("Here we go...");
            try
            {
                var logic = new ConfigLogic();
                response = logic.DeleteConfigurations(request);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "What Happened?");
                response.Status = false;
            }

            Assert.IsTrue(response.Status);

        }

        #endregion

        #endregion

        #region Config Logic

        #region GET

        [TestMethod]
        public void TestGetAllConfigurationsLogic()
        {
            ConfigurationResponse results = new ConfigurationResponse();
            LogHelper.LogInformation("Here we go...");

            try
            {
                var logic = new ConfigLogic();
                results = logic.GetAllConfigurations();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "What Happened?");
                results.Status = false;
            }

            Assert.IsTrue(results.Status);

        }

        [TestMethod]
        public void TestGetSpecificConfigurationLogic()
        {
            ConfigurationResponse results = new ConfigurationResponse();

            var request = new ConfigEntry();

            request.Name = "BingApiKey";
            //request.ID = 2;
            LogHelper.LogInformation("Here we go...");
            try
            {
                var logic = new ConfigLogic();
                results = logic.GetSpecificConfiguration(request);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "What Happened?");
                results.Status = false;
            }

            Assert.IsTrue(results.Status);
        }

        #endregion

        #region AddUpdate

        [TestMethod]
        public void TestAddUpdateConfigLogic()
        {
            ConfigurationResponse result = new ConfigurationResponse { Status = true };

            var request = new ConfigMultiEntries { ConfigEntries = new List<ConfigEntry>() };

            request.ConfigEntries.Add(new ConfigEntry { ID = 4, Name = "JDKv3", Value = "1.8.0_44_3" });
            request.ConfigEntries.Add(new ConfigEntry { ID = 5, Name = "JBossv3", Value = "Wildfly-8.2.1_3" });
            request.ConfigEntries.Add(new ConfigEntry { Name = "MyEclipse" });

            LogHelper.LogInformation("Here we go...");
            try
            {
                var logic = new ConfigLogic();
                result = logic.AddUpdateConfigurations(request);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "What Happened?");
                result.Status = false;
            }

            Assert.IsTrue(result.Status);
        }

        #endregion

        #endregion

        #region Config Service

        [TestMethod]
        public void TestAddUpdateConfigsService ()
        {
            var result = new ConfigurationResponse();
            var request = new ConfigMultiEntries { ConfigEntries = new List<ConfigEntry>()};

            request.ConfigEntries.Add(new ConfigEntry { ID = 4, Name = "JDKv4", Value = "1.8.0_44_4" });
            request.ConfigEntries.Add(new ConfigEntry { ID = 5, Name = "JBossv4", Value = "Wildfly-8.2.1_4" });
            request.ConfigEntries.Add(new ConfigEntry { Name = "MyEclipse" , Value = "v15_4" });

            LogHelper.LogInformation("Here we go...");

            try
            {
                var ConfigUrl = VAFConfiguration.GetAppSettingValue("ConfigUrlCloud");
                var fullUrl = new StringBuilder().AppendFormat("{0}/{1}",
                    ConfigUrl,
                    VAFConfiguration.GetAppSettingValue("AddUpdateConfigsOp"));
                var jsonHelper = new JsonHelper();
                result = jsonHelper.MakePostRequest<ConfigMultiEntries, ConfigurationResponse>(fullUrl.ToString(), request);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, "What Happened?");
                result.Status = false;
            }


            Assert.IsTrue(result.Status);
        }
         

        #endregion
    }
}
