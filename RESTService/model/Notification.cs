using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RESTService.model
{
    public class Notification
    {
        public int NotId { get; set; }
        public DateTime NotDate { get; set; }
        public DateTime NotTime { get; set; }
    }
}