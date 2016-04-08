using System;
using System.Runtime.Serialization;

namespace WebReport.Model
{
    [Serializable]
    [DataContract]
    public class Test
    {
        [DataMember(Name="Name")]
        public string Name { get; set; }
    }
}
