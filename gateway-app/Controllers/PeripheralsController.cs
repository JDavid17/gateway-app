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
    public class PeripheralsController : ControllerBase
    {
        private readonly GatewayAppContext _context;

        public PeripheralsController(GatewayAppContext context)
        {
            _context = context;
        }

        // GET: api/Peripherals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Peripheral>>> GetPeripherals()
        {
            return await _context.Peripherals.ToListAsync();
        }

        // GET: api/Peripherals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Peripheral>> GetPeripheral(int id)
        {
            var peripheral = await _context.Peripherals.FindAsync(id);

            if (peripheral == null)
            {
                return NotFound();
            }

            return peripheral;
        }

        // PUT: api/Peripherals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeripheral(int id, Peripheral peripheral)
        {
            if (id != peripheral.Id)
            {
                return BadRequest();
            }

            if (peripheral.GatewayId > 0)
            {
                var gateway = await _context.Gateways.Include(x => x.Peripherals).FirstOrDefaultAsync(g => g.Id == peripheral.GatewayId);

                if (gateway.Peripherals.Count >= 10)
                {
                    return BadRequest("Gateway already has the max number of allow peripherals");
                }
            }

            _context.Entry(peripheral).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeripheralExists(id))
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

        //// POST: api/Peripherals
        //[HttpPost]
        //public async Task<ActionResult<Peripheral>> PostPeripheral(Peripheral peripheral)
        //{
        //    _context.Peripherals.Add(peripheral);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetPeripheral", new { id = peripheral.Id }, peripheral);
        //}

        // DELETE: api/Peripherals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Peripheral>> DeletePeripheral(int id)
        {
            var peripheral = await _context.Peripherals.FindAsync(id);
            if (peripheral == null)
            {
                return NotFound();
            }

            _context.Peripherals.Remove(peripheral);
            await _context.SaveChangesAsync();

            return peripheral;
        }

        private bool PeripheralExists(int id)
        {
            return _context.Peripherals.Any(e => e.Id == id);
        }
    }
}
