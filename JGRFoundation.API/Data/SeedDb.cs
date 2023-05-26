using JGRFoundation.API.Helpers;
using JGRFoundation.API.Services;
using JGRFoundation.Shared.Entities;
using JGRFoundation.Shared.Enums;
using System.Runtime.InteropServices;

namespace JGRFoundation.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IFileStorage _fileStorage;

        public SeedDb(DataContext context, IApiService apiService, IUserHelper userHelper, IFileStorage fileStorage)
        {
            _context = context;
            _userHelper = userHelper;
            _fileStorage = fileStorage;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckBatteriesAsync();
            await CheckHomeAppliancesAsync();
            await CheckInvestorsAsync();
            await CheckPanelsAsync();
            await CheckUserAsync("1010", "Ricardo", "Gómez Gallego", "Ricardo@yopmail.com", "3015927246", "Calle xxx", "admin.jpeg", UserType.Admin);
            await CheckUserAsync("1010", "Juan Guillermo", "Palma Cerón", "Juan@yopmail.com", "3223114620", "Calle Luna  ", "admin.jpeg", UserType.Admin);
            await CheckUserAsync("1010", "Guillermo Herney", "Carrillo Rivera", "Guillermo@yopmail.com", "3223114620", "Calle Sol", "admin.jpeg", UserType.Admin);
            await CheckUserAsync("1010", "Usuario", "Apellidos", "Usuario@yopmail.com", "3003114620", "Calle Sol", "admin.jpeg", UserType.User);
        }
        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, string image, UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);

            if (user == null)
            {
                string filePath;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    filePath = $"{Environment.CurrentDirectory}\\Images\\users\\{image}";
                }
                else
                {
                    filePath = $"{Environment.CurrentDirectory}/Images/users/{image}";
                }

                var fileBytes = File.ReadAllBytes(filePath);
                var imagePath = await _fileStorage.SaveFileAsync(fileBytes, "jpg", "users");

                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    UserType = userType,
                    Photo = imagePath,
                };

                await _userHelper.AddUserAsync(user, "admin123");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }
            return user;
        }

      
        private Task CheckHomeAppliancesAsync()
        {
            if (!_context.HomeAppliances.Any())
            {
                _context.HomeAppliances.AddRange(new List<Appliance>()
                {
                    new Appliance() { Name = "Nevera" ,AverageDailyConsumption = 4800},
                    new Appliance() { Name = "Lavadora" ,AverageDailyConsumption = 500 },
                    new Appliance() { Name = "Televisor" ,AverageDailyConsumption = 300},
                    new Appliance() { Name = "Ventilador" ,AverageDailyConsumption = 900},
                    new Appliance() { Name = "Computador" ,AverageDailyConsumption = 2100},
                    new Appliance() { Name = "Licuadora" ,AverageDailyConsumption = 60},
                    new Appliance() { Name = "Microondas" ,AverageDailyConsumption = 1450},
                    new Appliance() { Name = "Bombillo 60 watts" ,AverageDailyConsumption = 360},
                    new Appliance() { Name = "Bombillo 25 watts" ,AverageDailyConsumption = 150},
                    new Appliance() { Name = "Equipo de sonido" ,AverageDailyConsumption = 250},
                    new Appliance() { Name = "Electrobomba" ,AverageDailyConsumption = 1200}
                });
            }
            return Task.CompletedTask;
        }

        private Task CheckInvestorsAsync()
        {
            if (!_context.Investors.Any())
            {
                _context.Investors.AddRange(new List<Investor>()
                {
                    new Investor() { InvestorReference = "Invest1200" ,RatedPower = 1200},
                    new Investor() { InvestorReference = "Invest2400" ,RatedPower = 2400},
                    new Investor() { InvestorReference = "Invest3600" ,RatedPower = 3600}
                });
            }
            return Task.CompletedTask;
        }

        private Task CheckPanelsAsync()
        {
            if (!_context.Panels.Any())
            {
                _context.Panels.AddRange(new List<Panel>()
                {
                    new Panel() { PanelReference = "Panel400", Power = 400 },
                    new Panel() { PanelReference = "Panel500", Power = 500 },
                    new Panel() { PanelReference = "Panel600", Power = 600 },
                    new Panel() { PanelReference = "Panel800", Power = 800 },
                    new Panel() { PanelReference = "Panel1000", Power = 1000 },
                    new Panel() { PanelReference = "Panel1200", Power = 1200 },
                    new Panel() { PanelReference = "Panel1400", Power = 1400 },
                    new Panel() { PanelReference = "Panel1600", Power = 1600 },
                    new Panel() { PanelReference = "Panel1800", Power = 1800 },
                    new Panel() { PanelReference = "Panel2000", Power = 2000 },
                    new Panel() { PanelReference = "Panel2200", Power = 2200 },
                    new Panel() { PanelReference = "Panel2400", Power = 2400 },
                    new Panel() { PanelReference = "Panel2600", Power = 2600 },
                    new Panel() { PanelReference = "Panel2800", Power = 2800 },
                    new Panel() { PanelReference = "Panel3000", Power = 3000 }
                });
            }
            return Task.CompletedTask;
        }

        private Task CheckBatteriesAsync()
        {
            if (!_context.Batteries.Any())
            {
                _context.Batteries.AddRange(new List<Battery>()
                {
                    new Battery() { BatteryReference = "BatSolar100",Voltage=100, CapacityAmperageHour = 120 },
                    new Battery() { BatteryReference = "BatSolar200",Voltage=200, CapacityAmperageHour = 120 },
                    new Battery() { BatteryReference = "BatSolar300",Voltage=300, CapacityAmperageHour = 120 },
                    new Battery() { BatteryReference = "BatSolar400",Voltage=400, CapacityAmperageHour = 120 },
                    new Battery() { BatteryReference = "BatSolar500",Voltage=500, CapacityAmperageHour = 120 },
                    new Battery() { BatteryReference = "BatSolar600",Voltage=600, CapacityAmperageHour = 120 },
                    new Battery() { BatteryReference = "BatSolar700",Voltage=700, CapacityAmperageHour = 120 }
                });
            }
            return Task.CompletedTask;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }
    }
}
