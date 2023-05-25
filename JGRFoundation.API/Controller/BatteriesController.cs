using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JGRFoundation.API.Data;
using JGRFoundation.API.Helpers;
using JGRFoundation.Shared.DTOs;
using JGRFoundation.Shared.Entities;
namespace JGRFoundation.API.Controller
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/batteries")]
    public class BatteriesController : ControllerBase
    {
        private readonly DataContext _context;

        public BatteriesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Batteries
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.BatteryReference.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(x => x.BatteryReference)
                .Paginate(pagination)
                .ToListAsync());
        }


        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Batteries
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.BatteryReference.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var battery = await _context.Batteries
                .FirstOrDefaultAsync(x => x.Id == id);
            if (battery is null)
            {
                return NotFound();
            }

            return Ok(battery);
        }


        [HttpPost]
        public async Task<ActionResult> PostAsync(Battery battery)
        {
            _context.Add(battery);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(battery);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un registro con el mismo nombre.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(Battery battery)
        {
            _context.Update(battery);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(battery);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException == null)
                {
                    return BadRequest(dbUpdateException.Message);
                }
                else if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un registro con el mismo nombre.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var battery = await _context.Batteries.FirstOrDefaultAsync(x => x.Id == id);
            if (battery == null)
            {
                return NotFound();
            }

            _context.Remove(battery);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}