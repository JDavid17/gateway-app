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
    [Route("api/gateway")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private readonly GatewayAppContext _context;

        public GatewayController(GatewayAppContext context)
        {
            _context = context;
        }

        // GET: api/gateway
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gateway>>> GetGateways()
        {
            return await _context.Gateways.ToListAsync();
        }

        // GET: api/gateway/5
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

        // PUT: api/gateway/5
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

        // POST: api/gateway
        [HttpPost]
        public async Task<ActionResult<Gateway>> PostGateway(Gateway gateway)
        {
            _context.Gateways.Add(gateway);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGateway", new { id = gateway.Id }, gateway);
        }

        // DELETE: api/gateway/5
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
