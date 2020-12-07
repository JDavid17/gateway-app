using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using gateway_app.CustomValidation;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace gateway_app.Models
{
    public class Gateway
    {
        //Properties
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        [ValidIpv4]
        public string Ipv4 { get; set; }

        //DB Relations
        [ValidPeripheralCount]
        public List<Peripheral> Peripherals { get; set; }
    }
}
