using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gateway_app.Models
{
    public class Gateway
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        [RegularExpression("^(?:[0-9]{1,3}\\.){3}[0-9]{1,3}$")]
        public string Ipv4 { get; set; }
    }
}
