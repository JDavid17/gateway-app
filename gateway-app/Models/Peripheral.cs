using gateway_app.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace gateway_app.Models
{
    public class Peripheral
    {
        // Properties
        public int Id { get; set; }
        public int UID { get; set; }
        public string Vendor { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }

        //Relations
        [ForeignKey("Gateway")]
        [JsonIgnore]
        public int GatewayId { get; set; }
        [JsonIgnore]
        public Gateway Gateway { get; set; }
    }
}
