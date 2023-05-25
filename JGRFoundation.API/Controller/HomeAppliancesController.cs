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
    [Route("/api/homeAppliances")]
    public class HomeAppliancesController : ControllerBase
    {
        private readonly DataContext _context;

        public HomeAppliancesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.HomeAppliances
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(x => x.Name)
                .Paginate(pagination)
                .ToListAsync());
        }


        [HttpGet("totalPages")]
        [AllowAnonymous]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.HomeAppliances
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAsync(int id)
        {
            var appliance = await _context.HomeAppliances
                .FirstOrDefaultAsync(x => x.Id == id);
            if (appliance is null)
            {
                return NotFound();
            }

            return Ok(appliance);
        }


        [HttpPost]
        public async Task<ActionResult> PostAsync(Appliance appliance)
        {
            _context.Add(appliance);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(appliance);
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
        public async Task<ActionResult> PutAsync(Appliance appliance)
        {
            _context.Update(appliance);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(appliance);
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
            var appliance = await _context.HomeAppliances.FirstOrDefaultAsync(x => x.Id == id);
            if (appliance == null)
            {
                return NotFound();
            }

            _context.Remove(appliance);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
