using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIC.PuppyLove.Profile.DataModel;
using TIC.PuppyLove.Services.Contracts.Common;
using TIC.PuppyLove.Services.Contracts.Config.Data;
namespace TIC.PuppyLove.Services.Logic.Config.Data
{
    public class ConfigDataManager
    {
        #region Constructor

        /// <summary>
        /// Static Constructor to load Entity framework required assemblies
        /// </summary>
        static ConfigDataManager()
        {
            var ensureDllIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        #endregion

        #region GET

        public List<ConfigEntry> GetAllConfigurations ()
        {
            
            List<ConfigEntry> response = new List<ConfigEntry>();
            var result = new ConfigDTO();
            using (var dbEntities = new TICPuppyLoveDbContext())
            {
                result.refConfigEntries = dbEntities.GetRefConfigData();
                response = result.CreateConfigCollection();
            }

            return response;
        }

        public List<ConfigEntry> GetSpecificConfiguration(ConfigEntry request)
        {
            List<ConfigEntry> response = GetAllConfigurations ();

            return FilterConfigurationList(request, response);
        }

        #endregion

        #region AddUpdate

        public void AddUpdateConfigEntries (ConfigMultiEntries request)
        {

            using (var context = new TICPuppyLoveDbContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    context.dbTransaction = dbContextTransaction;

                    ConfigDTO cdto = new ConfigDTO { configs = request.ConfigEntries };

                    //set inserts first
                    cdto.CreateRefConfigNewUserIntances();

                    if (cdto.refConfigEntries.Count() > 0)
                    {
                        context.Add(cdto);
                    }
                        
                    //now do updates
                    cdto.CreateRefConfigUpdateInstances();

                    if (cdto.refConfigEntries.Count() > 0)
                    {
                        context.Update(cdto);
                    }

                    // everything good, commit transaction
                    dbContextTransaction.Commit();
                }
            }
            
        }

        #endregion

        #region Delete

        public Int32 DeleteConfigurations(ConfigMultiEntries request)
        {
            Int32 response = -1;
            using (var context = new TICPuppyLoveDbContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    context.dbTransaction = dbContextTransaction;

                    ConfigDTO cdto = new ConfigDTO { configs = request.ConfigEntries };

                    cdto.refConfigEntries = context.GetRefConfigData();
                    cdto.refConfigEntries = cdto.GetSpecificRefConfigs();

                    context.Ref_Config.RemoveRange(cdto.refConfigEntries);
                    response =  context.SaveChanges();
                    
                    // everything good, commit transaction
                    dbContextTransaction.Commit();
                }
            }

            return response;
        }


        #endregion

        #region Helpers

        private List<ConfigEntry> FilterConfigurationList (ConfigEntry request, List<ConfigEntry> items)
        {

            if (request.ID.HasValue)
            {
                items = (from i in items where i.ID.Value == request.ID.Value
                         select i).ToList();
            }
            else
            {
                items = (from i in items 
                         where TrimAndUpper(i.Name) == TrimAndUpper(request.Name)
                         select i).ToList();
            }

            return items;
        }

        private string TrimAndUpper (string val)
        {
            if (val == null)
                val = string.Empty;

            return val.ToUpper().Trim();
        }


        #endregion   
    
    }
}
