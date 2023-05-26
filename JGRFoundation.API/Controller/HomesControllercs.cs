using JGRFoundation.API.Data;
using JGRFoundation.API.Helpers;
using JGRFoundation.Shared.DTOs;
using JGRFoundation.Shared.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JGRFoundation.API.Controller
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("/api/homes")]
    public class HomesControllercs : ControllerBase
    {
        private readonly IHomesHelper _homesHelper;
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public HomesControllercs(IHomesHelper homesHelper, DataContext context, IUserHelper userHelper)
        {
            _homesHelper = homesHelper;
            _context = context;
            _userHelper = userHelper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var home = await _context.Homes
                .Include(s => s.User!)
                .Include(s => s.HomeDetails!)
                .ThenInclude(sd => sd.Appliance)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (home is null)
            {
                return NotFound();
            }

            return Ok(home);
        }


        [HttpGet]
        public async Task<ActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.Identity!.Name);
            if (user == null)
            {
                return BadRequest("User not valid.");
            }

            var queryable = _context.Homes
                .Include(s => s.User!)
                .Include(s => s.HomeDetails!)
                .ThenInclude(sd => sd.Appliance)
                .AsQueryable();

            var isAdmin = await _userHelper.IsUserInRoleAsync(user, UserType.Admin.ToString());
            if (!isAdmin)
            {
                queryable = queryable.Where(s => s.User!.Email == User.Identity!.Name);
            }

            return Ok(await queryable
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == User.Identity!.Name);
            if (user == null)
            {
                return BadRequest("User not valid.");
            }

            var queryable = _context.Homes
                .AsQueryable();

            var isAdmin = await _userHelper.IsUserInRoleAsync(user, UserType.Admin.ToString());
            if (!isAdmin)
            {
                queryable = queryable.Where(s => s.User!.Email == User.Identity!.Name);
            }

            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(HomeDTO homeDTO)
        {
            var response = await _homesHelper.ProcessHomeAsync(User.Identity!.Name!, homeDTO.Coordinate);
            if (response.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest(response.Message);
        }
    }
}
