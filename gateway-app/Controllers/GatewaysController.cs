using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gateway_app.Models;

namespace gateway_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewaysController : ControllerBase
    {
        private readonly GatewayAppContext _context;

        public GatewaysController(GatewayAppContext context)
        {
            _context = context;
        }

        // GET: api/gateways
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gateway>>> GetGateways()
        {
            return await _context.Gateways.Include(x => x.Peripherals).ToListAsync();
        }

        // GET: api/gateways/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gateway>> GetGateway(int id)
        {
            var gateway = await _context.Gateways.FindAsync(id);

            if (gateway == null)
            {
                return NotFound();
            }

            return gateway;
        }

        // PUT: api/gateways/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGateway(int id, Gateway gateway)
        {
            if (id != gateway.Id)
            {
                return BadRequest();
            }

            _context.Entry(gateway).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GatewayExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //POST: api/gateways/1/add_peripheral
        [HttpPost("{id}/add_peripheral")]
        public async Task<ActionResult<Gateway>> AddPeripheral(int id, Peripheral peripheral)
        {
            peripheral.GatewayId = id;

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Peripherals.Add(peripheral);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGateway", new { id = id }, peripheral.Gateway);
        }

        // POST: api/gateways
        [HttpPost]
        public async Task<ActionResult<Gateway>> PostGateway(Gateway gateway)
        {
            _context.Gateways.Add(gateway);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGateway", new { id = gateway.Id }, gateway);
        }

        // DELETE: api/gateways/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Gateway>> DeleteGateway(int id)
        {
            var gateway = await _context.Gateways.FindAsync(id);
            if (gateway == null)
            {
                return NotFound();
            }

            _context.Gateways.Remove(gateway);
            await _context.SaveChangesAsync();

            return gateway;
        }

        private bool GatewayExists(int id)
        {
            return _context.Gateways.Any(e => e.Id == id);
        }
    }
}
