using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ServiceModel.Activation;
using Ventera.VAF.Core.Helpers;
using Ventera.VAF.Core.Configuration;
using TIC.PuppyLove.Services.Contracts.Config.Data;
using TIC.PuppyLove.Services.Contracts.Config.Service;
using TIC.PuppyLove.Services.Logic.Config.Data;
using TIC.PuppyLove.Services.Contracts.Common;

namespace TIC.PuppyLove.Services.Logic.Config
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ConfigLogic : IConfigService
    {        
        #region GET

        public ConfigurationResponse GetAllConfigurations ()
        {
            var response = new ConfigurationResponse
            {
                Status = true,
                ReturnMessage = ConfigResource.GetAllConfigurationsSuccess,
                Configurations = new List<ConfigEntry>()
            };

            var dmgr = new ConfigDataManager();

            try
            {
                LogHelper.LogInformation(string.Concat(ConfigResource.GetAllConfigEntriesOp, 
                    MethodBase.GetCurrentMethod().Name));
                response.Configurations = dmgr.GetAllConfigurations();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex,
                    string.Format(ConfigResource.ErrorInOperation, MethodBase.GetCurrentMethod().Name));
                // Additional EF info if it is there
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    !string.IsNullOrWhiteSpace(ex.InnerException.InnerException.Message))
                {
                    LogHelper.LogWarning(string.Concat(ConfigResource.InnerExceptionReceived, 
                                ex.InnerException.InnerException.Message));
                }

                response.Status = false;
                response.ReturnMessage = ConfigResource.GetAllConfigurationsFailed;
            }
            finally
            {
                dmgr = null;
                LogHelper.LogInformation(string.Concat(ConfigResource.EndOfOperation, 
                    MethodBase.GetCurrentMethod().Name));
            }

            EchoResponseStatus(response);

            return response;
        }

        public ConfigurationResponse GetSpecificConfiguration(ConfigEntry request)
        {
            EchoConfigEntryRequest(request);

            if (request.ID.HasValue && !string.IsNullOrWhiteSpace(request.Name))
            {
                LogHelper.LogInformation(ConfigResource.BothIdAndNameReceived);
            }

            var response = new ConfigurationResponse
            {
                Status = true,
                ReturnMessage = ConfigResource.GetSpecificConfigurationsSuccess,
                Configurations = new List<ConfigEntry>()
            };

            var dmgr = new ConfigDataManager();

            try
            {
                LogHelper.LogInformation(string.Concat(ConfigResource.GetSpecificConfigEntriesOp, 
                    MethodBase.GetCurrentMethod().Name));

                if (request.ID.HasValue || !string.IsNullOrWhiteSpace(request.Name))
                {
                    response.Configurations = dmgr.GetSpecificConfiguration(request);

                    if (response.Configurations.Count() == 0)
                    {
                        response.Status = false;
                        response.ReturnMessage = ConfigResource.NoConfigEntriesFound;
                    }
                }
                else
                {
                    response.Status = false;
                    response.ReturnMessage = ConfigResource.BadRequestMsg;
                }
                
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex,
                    string.Format(ConfigResource.ErrorInOperation, MethodBase.GetCurrentMethod().Name));
                // Additional EF info if it is there
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    !string.IsNullOrWhiteSpace(ex.InnerException.InnerException.Message))
                {
                    LogHelper.LogWarning(string.Concat(ConfigResource.InnerExceptionReceived,
                                ex.InnerException.InnerException.Message));
                }

                response.Status = false;
                response.ReturnMessage = ConfigResource.GetSpecificConfigurationsFailed;
            }
            finally
            {
                dmgr = null;
                LogHelper.LogInformation(string.Concat(ConfigResource.EndOfOperation,
                    MethodBase.GetCurrentMethod().Name));
            }


            EchoResponseStatus(response);

            return response;
        }

        #endregion

        #region AddUpdate

        public ConfigurationResponse AddUpdateConfigurations(ConfigMultiEntries request)
        {
            var response = new ConfigurationResponse
            {
                Status = true,
                ReturnMessage = ConfigResource.AddUpdateConfigurationsSuccess,
                Configurations = new List<ConfigEntry>()
            };

            var dmgr = new ConfigDataManager();

            try
            {
                LogHelper.LogInformation(string.Concat(ConfigResource.AddUpdateConfigurationsOp,
                    MethodBase.GetCurrentMethod().Name));
                if (request.ConfigEntries != null && request.ConfigEntries.Count() > 0)
                {
                    EchoMultiRequest(request.ConfigEntries);

                    int orig = request.ConfigEntries.Count();

                    request.ConfigEntries = FilterIncompleteAddUpdateItems(request.ConfigEntries);

                    if (orig > request.ConfigEntries.Count())
                    {
                        LogHelper.LogWarning(ConfigResource.WarnInputRecsDiscarded);
                    }

                    if (request.ConfigEntries.Count() > 0)
                    {
                        dmgr.AddUpdateConfigEntries(request);
                    }
                    else
                    {
                        response.Status = false;
                        response.ReturnMessage = ConfigResource.OperationAborted;
                    }
                }
                else
                {
                    response.Status = false;
                    response.ReturnMessage = ConfigResource.OperationAborted;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex,
                    string.Format(ConfigResource.ErrorInOperation, MethodBase.GetCurrentMethod().Name));
                // Additional EF info if it is there
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    !string.IsNullOrWhiteSpace(ex.InnerException.InnerException.Message))
                {
                    LogHelper.LogWarning(string.Concat(ConfigResource.InnerExceptionReceived,
                                ex.InnerException.InnerException.Message));
                }

                response.Status = false;
                response.ReturnMessage = ConfigResource.AddUpdateConfigurationsFailed;
            }
            finally
            {
                dmgr = null;
                LogHelper.LogInformation(string.Concat(ConfigResource.EndOfOperation,
                    MethodBase.GetCurrentMethod().Name));
            }

            EchoResponseStatus(response);

            return response;
        }


        #endregion

        #region Delete

        public ConfigurationResponse DeleteConfigurations(ConfigMultiEntries request)
        {
            var response = new ConfigurationResponse
            {
                Status = true,
                ReturnMessage = ConfigResource.DeleteConfigurationsSuccess,
                Configurations = new List<ConfigEntry>()
            };

            var dmgr = new ConfigDataManager();
            
            try
            {
                LogHelper.LogInformation(string.Concat(ConfigResource.AddUpdateConfigurationsOp,
                    MethodBase.GetCurrentMethod().Name));
                if (request.ConfigEntries != null && request.ConfigEntries.Count() > 0)
                {
                    EchoMultiRequest(request.ConfigEntries);

                    int orig = request.ConfigEntries.Count();

                    request.ConfigEntries = FilterIncompleteDeleteItems(request.ConfigEntries);

                    if (orig > request.ConfigEntries.Count())
                    {
                        LogHelper.LogWarning(ConfigResource.WarnInputRecsDiscarded);
                    }

                    if (request.ConfigEntries.Count > 0)
                    {
                        int i = dmgr.DeleteConfigurations(request);
                        if (i < request.ConfigEntries.Count)
                        {
                            LogHelper.LogWarning(string.Format(
                                ConfigResource.WarnNotAllDeleted,
                                request.ConfigEntries.Count.ToString(),
                                i.ToString()));
                        }
                        else
                        {
                            LogHelper.LogInformation(string.Format(ConfigResource.NumRowsDeleted,
                                i.ToString()));
                        }
                    }
                    else
                    {
                        response.Status = false;
                        response.ReturnMessage = ConfigResource.OperationAborted;
                    }
                }
                else
                {
                    response.Status = false;
                    response.ReturnMessage = ConfigResource.OperationAborted;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex,
                    string.Format(ConfigResource.ErrorInOperation, MethodBase.GetCurrentMethod().Name));
                // Additional EF info if it is there
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    !string.IsNullOrWhiteSpace(ex.InnerException.InnerException.Message))
                {
                    LogHelper.LogWarning(string.Concat(ConfigResource.InnerExceptionReceived,
                                ex.InnerException.InnerException.Message));
                }

                response.Status = false;
                response.ReturnMessage = ConfigResource.DeleteConfigurationsFailed;
            }
            finally
            {
                dmgr = null;
                LogHelper.LogInformation(string.Concat(ConfigResource.EndOfOperation,
                    MethodBase.GetCurrentMethod().Name));
            }
            
            EchoResponseStatus(response);

            return response;
        }

        #endregion

        #region helpers

        private void EchoMultiRequest(List<ConfigEntry> request)
        {
            foreach (ConfigEntry ce in request)
            {
                EchoConfigEntryRequest(ce);
            }
        }

        private void EchoResponseStatus(CommonResponse response)
        {
            LogHelper.LogInformation(string.Format(
                ConfigResource.EchoResponseObj,
                response.Status.ToString(), response.ReturnMessage));
        }

        private void EchoConfigEntryRequest (ConfigEntry request)
        {
            LogHelper.LogInformation(string.Format(
                ConfigResource.EchoConfigEntry,
                (request.ID.HasValue) ? request.ID.Value.ToString() : ConfigResource.strNULL,
                (!string.IsNullOrWhiteSpace(request.Name) ? request.Name : string.Empty),
                (!string.IsNullOrWhiteSpace(request.Value) ? request.Value : string.Empty)
                ));
        }

        private List<ConfigEntry> FilterIncompleteAddUpdateItems (List<ConfigEntry> items)
        {
            return (from i in items
                    where !string.IsNullOrWhiteSpace(i.Name) &&
                    !string.IsNullOrWhiteSpace(i.Value)
                    select i).ToList();
        }

        private List<ConfigEntry> FilterIncompleteDeleteItems(List<ConfigEntry> items)
        {
            return (from i in items
                    where i.ID.HasValue
                    select i).ToList();
        }

        #endregion
    
    }
}
