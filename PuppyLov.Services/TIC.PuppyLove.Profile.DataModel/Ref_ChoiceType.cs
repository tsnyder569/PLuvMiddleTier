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
    
    public partial class Ref_ChoiceType
    {
        public Ref_ChoiceType()
        {
            this.ProfileChocies = new HashSet<ProfileChocy>();
        }
    
        public long ID { get; set; }
        public string MatchType { get; set; }
    
        public virtual ICollection<ProfileChocy> ProfileChocies { get; set; }
    }
}
