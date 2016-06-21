//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TIC.PuppyLove.Profile.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Locations = new HashSet<Location>();
            this.ProfileChocies = new HashSet<ProfileChocy>();
            this.ProfileChocies1 = new HashSet<ProfileChocy>();
            this.UserResponses = new HashSet<UserRespons>();
            this.Photos = new HashSet<Photo>();
        }
    
        public long ID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string DisplayName { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string MiddleName { get; set; }
        public string Bio { get; set; }
        public string Photo { get; set; }
    
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<ProfileChocy> ProfileChocies { get; set; }
        public virtual ICollection<ProfileChocy> ProfileChocies1 { get; set; }
        public virtual ICollection<UserRespons> UserResponses { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}