﻿using System.Runtime.Serialization;
using TIC.PuppyLove.Services.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TIC.PuppyLove.Services.Contracts.Profile.Data
{

    [DataContract]
    [Serializable]
    public class ProfileChoiceRequest : UserBase
    {
        [DataMember]
        public UserBase MatchUserID { get; set; }
    }
}