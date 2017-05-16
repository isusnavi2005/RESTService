using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RESTService.model
{
    [DataContract]
    public class Job
    {
        [DataMember]
        public int JobId { get; set; }
        [DataMember]
        public DateTime StarTime { get; set; }
        [DataMember]
        public DateTime EndTime { get; set; }
        [DataMember]
        public int UserId { get; set; }

        
    }
}