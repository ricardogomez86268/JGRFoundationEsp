using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JGRFoundation.API.Data;
using JGRFoundation.API.Helpers;
using JGRFoundation.Shared.DTOs;
using JGRFoundation.Shared.Entities;
using JGRFoundation.API.Helpers.Adapter;

namespace JGRFoundation.API.Controller
{
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/investors")]
    public class InvestorsController : ControllerBase
    {
        private readonly DataContext _context;

        public InvestorsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Investors
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.InvestorReference.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return Ok(await queryable
                .OrderBy(x => x.InvestorReference)
                .Paginate(pagination)
                .ToListAsync());
        }


        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Investors
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.InvestorReference.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var investor = await _context.Investors
                .FirstOrDefaultAsync(x => x.Id == id);
            if (investor is null)
            {
                return NotFound();
            }

            return Ok(investor);
        }

        [HttpGet("Old{id:int}")]
        public async Task<ActionResult> GetOldAsync(int id)
        {
            var investor = await _context.Investors
                .FirstOrDefaultAsync(x => x.Id == id);
            if (investor is null)
                return NotFound();

            var investorOldDto = new InvestorAdapter(investor);
            return Ok(investorOldDto);
        }


        [HttpPost]
        public async Task<ActionResult> PostAsync(Investor investor)
        {
            _context.Add(investor);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(investor);
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
        public async Task<ActionResult> PutAsync(Investor investor)
        {
            _context.Update(investor);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(investor);
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
            var investor = await _context.Investors.FirstOrDefaultAsync(x => x.Id == id);
            if (investor == null)
            {
                return NotFound();
            }

            _context.Remove(investor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
