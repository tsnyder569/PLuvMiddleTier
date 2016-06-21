using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Web;

namespace TestWcfService.Items
{
    [DataContract]
    public class TestItem
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Color { get; set; }
    }
}