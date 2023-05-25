﻿using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using JGRFoundation.API.Helpers;
using JGRFoundation.API.Services;
using JGRFoundation.Shared.Entities;
using JGRFoundation.Shared.Enums;
using JGRFoundation.Shared.Responses;

namespace JGRFoundation.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IApiService _apiService;
        private readonly IUserHelper _userHelper;
        private readonly IFileStorage _fileStorage;

        public SeedDb(DataContext context, IApiService apiService, IUserHelper userHelper, IFileStorage fileStorage)
        {
            _context = context;
            _apiService = apiService;
            _userHelper = userHelper;
            _fileStorage = fileStorage;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCategoriesAsync();
            await CheckInvestorsAsync();
            await CheckPanelsAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Ricardo", "Gómez Gallego", "Rico@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "admin.jpeg", UserType.Admin);
            await CheckUserAsync("1010", "Juan Guillermo", "Palma Cerón", "Juan@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "admin.jpeg", UserType.Admin);
            await CheckUserAsync("1010", "Guillermo Herney", "Carrillo Rivera", "Guillermo@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "admin.jpeg", UserType.Admin);
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

        private Task CheckInvestorsAsync()
        {
            if (!_context.Investors.Any())
            {
                _context.Investors.AddRange(new List<Investor>()
                {
                    new Investor() { InvestorReference = "Invest1200" ,RatedPower = 1200},
                    new Investor() { InvestorReference = "Invest3400" ,RatedPower = 3400}
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
                    new Panel() { PanelReference = "Panel400",Power = 400},
                    new Panel() { PanelReference = "Panel500",Power = 500 },
                    new Panel() { PanelReference = "Panel600",Power = 600 }
                });
            }

            return Task.CompletedTask;
        }
        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Apple" });
                _context.Categories.Add(new Category { Name = "Autos" });
                _context.Categories.Add(new Category { Name = "Belleza" });
                _context.Categories.Add(new Category { Name = "Calzado" });
                _context.Categories.Add(new Category { Name = "Comida" });
                _context.Categories.Add(new Category { Name = "Cosmeticos" });
                _context.Categories.Add(new Category { Name = "Deportes" });
                _context.Categories.Add(new Category { Name = "Erótica" });
                _context.Categories.Add(new Category { Name = "Ferreteria" });
                _context.Categories.Add(new Category { Name = "Gamer" });
                _context.Categories.Add(new Category { Name = "Hogar" });
                _context.Categories.Add(new Category { Name = "Jardín" });
                _context.Categories.Add(new Category { Name = "Jugetes" });
                _context.Categories.Add(new Category { Name = "Lenceria" });
                _context.Categories.Add(new Category { Name = "Mascotas" });
                _context.Categories.Add(new Category { Name = "Nutrición" });
                _context.Categories.Add(new Category { Name = "Ropa" });
                _context.Categories.Add(new Category { Name = "Tecnología" });
                await _context.SaveChangesAsync();
            }
        }      
       
        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }       
    }
}
