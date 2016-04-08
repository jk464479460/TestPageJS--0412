using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceLib.Model
{
    [Serializable]
    [DataContract]
    public class Test
    {
        [DataMember(Name="Name")]
        public string Name { get; set; }
    }
}
