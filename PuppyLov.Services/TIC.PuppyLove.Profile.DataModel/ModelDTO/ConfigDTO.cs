using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIC.PuppyLove.Services.Contracts.Config.Data;

namespace TIC.PuppyLove.Profile.DataModel
{
    public class ConfigDTO
    {
        public Ref_Config refconfig { get; set; }
        public List<Ref_Config> refConfigEntries { get; set; }
        public ConfigEntry config { get; set; }
        public List<ConfigEntry> configs { get; set; }

        #region Get

        public List<ConfigEntry> CreateConfigCollection()
        {
            return  (from rfc in refConfigEntries
                            orderby rfc.Name
                            select new ConfigEntry { ID = rfc.ID, Name = rfc.Name, Value = rfc.Value }
                            ).ToList();
        }

        #endregion

        #region Add

        public void CreateRefConfigNewUserIntances ()
        {
            refConfigEntries = (from c in configs
                                where !c.ID.HasValue
                                select new Ref_Config
                                {
                                    Name = c.Name,
                                    Value = c.Value
                                }).ToList();
        }

        #endregion

        #region Update

        public void CreateRefConfigUpdateInstances ()
        {
            refConfigEntries = (from c in configs
                                where c.ID.HasValue
                                select new Ref_Config
                                {
                                    ID = c.ID.Value,
                                    Name = c.Name,
                                    Value = c.Value
                                }).ToList();
        }

        #endregion

        #region Delete

        public List<Ref_Config> GetSpecificRefConfigs ()
        {
            return (from rfc in refConfigEntries
                    join c in configs on rfc.ID equals c.ID
                       select rfc).ToList();
        }


        #endregion

    }
}
