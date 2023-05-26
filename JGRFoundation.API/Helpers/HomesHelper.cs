using JGRFoundation.API.Data;
using JGRFoundation.Shared.Entities;
using JGRFoundation.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace JGRFoundation.API.Helpers
{
    public class HomesHelper : IHomesHelper
    {
        private readonly DataContext _context;

        public HomesHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<Response> ProcessHomeAsync(string email, string coordinate)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Usuario no válido"
                };
            }

            var temporalHomes = await _context.TemporalHomes
                .Include(x => x.Appliance)
                .Where(x => x.User!.Email == email)
                .ToListAsync();
            Response response = await CheckHomeAppliancesAsync(temporalHomes);
            if (!response.IsSuccess)
            {
                return response;
            }

            Home home = new()
            {
                User = user,
                Coordinate = coordinate,
                HomeDetails = new List<HomeDetail>(),
            };

            foreach (var temporalHome in temporalHomes)
            {
                home.HomeDetails.Add(new HomeDetail
                {
                    Appliance = temporalHome.Appliance,
                    QuantityByAppliance = temporalHome.QuantityByAppliance,
                });
                _context.TemporalHomes.Remove(temporalHome);
            }

            _context.Homes.Add(home);
            await _context.SaveChangesAsync();
            return response;
        }

        private async Task<Response> CheckHomeAppliancesAsync(List<TemporalHome> temporalHomes)
        {
            Response response = new() { IsSuccess = true };
            foreach (var temporalHome in temporalHomes)
            {
                Appliance? appliance = await _context.HomeAppliances.FirstOrDefaultAsync(x => x.Id == temporalHome.Appliance!.Id);
                if (appliance == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"El electrodomestico {temporalHome.Appliance!.Name}, ya no está disponible";
                    return response;
                }
            }
            return response;
        }
    }
}
