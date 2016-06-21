using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIC.PuppyLove.Services.Contracts.Profile.Data;

namespace TIC.PuppyLove.Profile.DataModel
{
    public class PhotoTypeDTO
    {
        public Ref_PhotoType refPhotoType { get; set; }
        public List<Ref_PhotoType> refPhotoTypes { get; set; }
        public ProfilePhotoType profilePhotoType { get; set; }
        public List<ProfilePhotoType> profilePhotoTypes { get; set; }

        #region GET

        public List<ProfilePhotoType> CreateProfilePhotoTypeCollection ()
        {
            return (from rpt in refPhotoTypes
                    orderby rpt.ID
                    select new ProfilePhotoType { ID = rpt.ID, PhotoType = rpt.PhotoType }
                            ).ToList();
        }

        public void SetProfilePhotoypes ()
        {
            this.profilePhotoTypes = this.CreateProfilePhotoTypeCollection();
        }

        #endregion
    
    }
}
