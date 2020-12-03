using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gateway_app.Models
{
    public class GatewayAppContext : DbContext
    {
        public GatewayAppContext(DbContextOptions<GatewayAppContext> options) : base(options){

        }

        public DbSet<Gateway> Gateways { get; set; }
    }
}
