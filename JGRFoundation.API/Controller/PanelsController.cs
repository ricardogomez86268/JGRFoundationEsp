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
    [Route("/api/panels")]
    public class PanelsController : ControllerBase
    {
        private readonly DataContext _context;

        public PanelsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Panels
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.PanelReference.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(x => x.PanelReference)
                .Paginate(pagination)
                .ToListAsync());
        }


        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Panels
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.PanelReference.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var panel = await _context.Panels
                .FirstOrDefaultAsync(x => x.Id == id);
            if (panel is null)
            {
                return NotFound();
            }

            return Ok(panel);
        }


        [HttpPost]
        public async Task<ActionResult> PostAsync(Panel panel)
        {
            _context.Add(panel);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(panel);
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
        public async Task<ActionResult> PutAsync(Panel panel)
        {
            _context.Update(panel);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(panel);
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
            var panel = await _context.Panels.FirstOrDefaultAsync(x => x.Id == id);
            if (panel == null)
            {
                return NotFound();
            }

            _context.Remove(panel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
