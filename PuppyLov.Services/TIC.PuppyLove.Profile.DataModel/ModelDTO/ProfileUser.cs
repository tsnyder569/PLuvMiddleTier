using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIC.PuppyLove.Services.Contracts.Profile.Data;
using TIC.PuppyLove.Services.Contracts.Common;

namespace TIC.PuppyLove.Profile.DataModel
{
    public class ProfileUser
    {
        
        public User User { get; set; }
        public UserData ProlieUserData { get; set; }

        public UserData CreateProfileInstance()
        {

            return new UserData
                       {
                           UserID = User.ID,
                           UserName = User.UserName,
                           DisplayName = User.DisplayName,
                           FirstName = User.FirstName,
                           LastName = User.LastName,
                           Email = User.Email,
                           PhoneNumber = User.PhoneNumber,
                           Photo = User.Photo,
                           Bio = User.Bio,
                           Birthday = User.Birthday,
                           MiddleName = User.MiddleName
                       };

        }

        public User CreateUserModelInstance()
        {

            return new User
                    {
                        ID = ProlieUserData.UserID.Value,
                        DisplayName = ProlieUserData.DisplayName,
                        Email = ProlieUserData.Email,
                        FirstName = ProlieUserData.FirstName,
                        LastName = ProlieUserData.LastName,
                        PhoneNumber = ProlieUserData.PhoneNumber,
                        UserName = ProlieUserData.UserName,
                        Bio = ProlieUserData.Bio,
                        Birthday = ProlieUserData.Birthday,
                        MiddleName = ProlieUserData.MiddleName,
                        Photo = ProlieUserData.Photo
                    };

        }

        // Updates

        public User CreateUserModelInstance(long id)
        {

            return new User
            {
                ID = id
            };

        }

        public User SetUserModelInstance(User user)
        {
            user.DisplayName = ProlieUserData.DisplayName;
            user.Email = ProlieUserData.Email;
            user.FirstName = ProlieUserData.FirstName;
            user.LastName = ProlieUserData.LastName;
            user.PhoneNumber = ProlieUserData.PhoneNumber;
            user.UserName = ProlieUserData.UserName;
            user.Bio = ProlieUserData.Bio;
            user.Birthday = ProlieUserData.Birthday;
            user.MiddleName = ProlieUserData.MiddleName;
            user.Photo = ProlieUserData.Photo;

            return user;
        }
    }
}
