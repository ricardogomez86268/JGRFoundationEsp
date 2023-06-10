using JGRFoundation.API.Data;
using JGRFoundation.API.Helpers;
using JGRFoundation.API.Helpers.Builder;
using JGRFoundation.Shared.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JGRFoundation.API.Controller
{
    [ApiController]
    [Route("/api/General")]
    public class GeneralController : ControllerBase
    {
        private readonly DataContext _context;

        public GeneralController(DataContext context)
        {
            _context = context;
        }

        [HttpPut("PutInvestors")]
        public async Task<ActionResult> PutInvestorsAsync(QueryUpdateDTO queryUpdateDTO)
        {
            try
            {
                var directorQuery = new DirectorQuery();
                var queryBuilder = new QueryBuilderConcrete();
                directorQuery.constructInvestors(queryBuilder);
                queryBuilder.Where(queryUpdateDTO.condition);
                foreach (string columns in queryUpdateDTO.columns)
                {
                    queryBuilder.Set(columns);
                }
                var queryUpdate = queryBuilder.Build();
                return await Task.FromResult(Ok(_context.Database.ExecuteSqlRaw(queryUpdate.Query)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("PutPanels")]
        public async Task<ActionResult> PutPanelsAsync(QueryUpdateDTO queryUpdateDTO)
        {
            try
            {
                var directorQuery = new DirectorQuery();
                var queryBuilder = new QueryBuilderConcrete();
                directorQuery.constructPanels(queryBuilder);
                queryBuilder.Where(queryUpdateDTO.condition);
                foreach (string columns in queryUpdateDTO.columns)
                {
                    queryBuilder.Set(columns);
                }
                var queryUpdate = queryBuilder.Build();
                return await Task.FromResult(Ok(_context.Database.ExecuteSqlRaw(queryUpdate.Query)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("PutBatteries")]
        public async Task<ActionResult> PutBatteriesAsync(QueryUpdateDTO queryUpdateDTO)
        {
            try
            {
                var directorQuery = new DirectorQuery();
                var queryBuilder = new QueryBuilderConcrete();
                directorQuery.constructBatteries(queryBuilder);
                queryBuilder.Where(queryUpdateDTO.condition);
                foreach (string columns in queryUpdateDTO.columns)
                {
                    queryBuilder.Set(columns);
                }
                var queryUpdate = queryBuilder.Build();
                return await Task.FromResult(Ok(_context.Database.ExecuteSqlRaw(queryUpdate.Query)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
