using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIC.PuppyLove.Services.Contracts.Profile.Data;

namespace TIC.PuppyLove.Profile.DataModel
{
    public class PhotoDTO
    {
        public Photo Photo { get; set; }
        public List<Photo> Photos { get; set; }
        public ProfilePhoto profilePhoto { get; set; }
        public List<ProfilePhoto> profilePhotos { get; set; }

        #region add

        public void CreateNewDBPhotoInstances(List<ProfilePhotoType> ppt)
        {
            Photos = (from pp in profilePhotos
                      where !pp.ID.HasValue
                      select new Photo
                      {
                          PhotoTypeID = GetPhotoTypeId(pp.PhotoType, ppt),
                          IsPrimary = pp.IsPrimary,
                          PhotoImage = pp.PhotoImage,
                          UserID = pp.UserID
                      }).ToList();
        }

        public void CreateUpdateDBPhotoInstances(List<ProfilePhotoType> ppt)
        {
            Photos = (from pp in profilePhotos
                      where pp.ID.HasValue
                      select new Photo
                      {
                          ID = pp.ID.Value,
                          IsPrimary = pp.IsPrimary,
                      }).ToList();
        }

        private Int64 GetPhotoTypeId (string phototype, List<ProfilePhotoType> ppt)
        {
            Int64 defaultResponse = 0;

            var items = (from p in ppt
                        where p.PhotoType != string.Empty &&
                        TrimAndUpper(p.PhotoType) == TrimAndUpper(phototype)
                        select p
                            ).ToList();
            if (items.Count == 0)
            {
                items = (from p in ppt
                         where p.PhotoType == "Unassigned"
                         select p
                             ).ToList();
            }

            return (items.Count > 0) ? items[0].ID : defaultResponse;
        }

        private string TrimAndUpper (string val)
        {
            return (!string.IsNullOrWhiteSpace(val)) ? val.Trim().ToUpper() : string.Empty;
        }

        #endregion

        #region GET

        private List<ProfilePhoto> CreateProfilePhotoCollection (List<ProfilePhotoType> ppt)
        {
            return (from p in Photos
                    join pt in ppt on p.PhotoTypeID equals pt.ID
                    select new ProfilePhoto
                    {
                        ID = p.ID,
                        IsPrimary = p.IsPrimary,
                        PhotoImage = p.PhotoImage,
                        PhotoType = pt.PhotoType,
                        UserID = p.UserID
                    }).ToList();
        }

        public void SetProfilePhotos (List<ProfilePhotoType> ppt)
        {
            this.profilePhotos = this.CreateProfilePhotoCollection(ppt);
        }

        public void SetPrimaryProfilePhoto(List<ProfilePhotoType> ppt)
        {
            var items = this.CreateProfilePhotoCollection(ppt);
            this.profilePhotos = (from i in items
                                  where i.IsPrimary == true
                                  select i).ToList();
            this.profilePhoto = (this.profilePhotos != null && this.profilePhotos.Count > 0) ?
                this.profilePhotos[0] : new ProfilePhoto();
        }

        #endregion

        #region DELETE

        private List<Photo> GetPhotosForRemoveRangeFromDB ()
        {
            return (from pp in profilePhotos
                    select new Photo { ID = pp.ID.Value }).ToList();
        }

        public void GetPhotosForRemoveRange ()
        {
            this.Photos = this.GetPhotosForRemoveRangeFromDB();
        }


        #endregion
    }
}
