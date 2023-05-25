using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JGRFoundation.API.Data;
using JGRFoundation.Shared.DTOs;
using JGRFoundation.Shared.Entities;

namespace JGRFoundation.API.Controller
{

    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/temporalHomes")]
    public class TemporalHomesController : ControllerBase
    {
        private readonly DataContext _context;

        public TemporalHomesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(TemporalHomeDTO temporalHomeDTO)
        {
            var appliance = await _context.HomeAppliances.FirstOrDefaultAsync(x => x.Id == temporalHomeDTO.ApplianceId);
            if (appliance == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.Identity!.Name);
            if (user == null)
            {
                return NotFound();
            }

            var temporalHome = new TemporalHome
            {
                Appliance = appliance,
                QuantityByAppliance = temporalHomeDTO.Quantity,
                User = user,
                Coordinate = "0, 0"
            };

            try
            {
                _context.Add(temporalHome);
                await _context.SaveChangesAsync();
                return Ok(temporalHomeDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            return Ok(await _context.TemporalHomes
                .Include(ts => ts.User!)
                .Include(ts => ts.Appliance!)
                .Where(x => x.User!.Email == User.Identity!.Name)
                .ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            return Ok(await _context.TemporalHomes
                .Include(ts => ts.User!)
                .Include(ts => ts.Appliance!)
                .FirstOrDefaultAsync(x => x.Id == id));
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(TemporalHomeDTO temporalHomeDTO)
        {
            var currentTemporalHome = await _context.TemporalHomes.FirstOrDefaultAsync(x => x.Id == temporalHomeDTO.Id);
            if (currentTemporalHome == null)
            {
                return NotFound();
            }
            currentTemporalHome.QuantityByAppliance = temporalHomeDTO.Quantity;

            _context.Update(currentTemporalHome);
            await _context.SaveChangesAsync();
            return Ok(temporalHomeDTO);
        }

        [HttpGet("count")]
        public async Task<ActionResult> GetCount()
        {
            return Ok(await _context.TemporalHomes
                .Where(x => x.User!.Email == User.Identity!.Name)
                .SumAsync(x => x.QuantityByAppliance));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var temporalHome = await _context.TemporalHomes.FirstOrDefaultAsync(x => x.Id == id);
            if (temporalHome == null)
            {
                return NotFound();
            }

            _context.Remove(temporalHome);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
