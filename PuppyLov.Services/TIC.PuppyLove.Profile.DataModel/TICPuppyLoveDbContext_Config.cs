using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIC.PuppyLove.Profile.DataModel
{
    public partial class TICPuppyLoveDbContext : TICPuppyLoveDBEntities
    {
        #region GET

        public List<Ref_Config> GetRefConfigData()
        {
            return (from rc in base.Ref_Config
                          orderby rc.Name
                          select rc).ToList();

        }

        #endregion

        #region Add

        public void Add(ConfigDTO cdto)
        {
            foreach (Ref_Config rc in cdto.refConfigEntries)
            {
                base.Ref_Config.Add(rc);                
            }

            SaveChanges();
        }

        #endregion

        #region Update

        public void Update(ConfigDTO cdto)
        {
            foreach (Ref_Config rc in cdto.refConfigEntries)
            {
                base.Ref_Config.Attach(rc);
                base.Entry(rc).State = EntityState.Modified;
                
                //below is how you tell EF only to update specific columns - leaving in for reference.
                //base.Entry(rc).Property(a => a.Name).IsModified = true;
                //base.Entry(rc).Property(a => a.Value).IsModified = true;
            }

            SaveChanges();
        }

        
        #endregion

        #region Delete

        public void Delete(ConfigDTO cdto)
        {
            
            
        }


        #endregion
    }
}
