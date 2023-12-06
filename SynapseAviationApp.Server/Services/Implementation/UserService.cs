using Microsoft.EntityFrameworkCore;
using SynapseAviationApp.Server.Database;
using SynapseAviationApp.Server.DTOMappers;
using SynapseAviationApp.Server.DTOModels;
using SynapseAviationApp.Server.Models;
using SynapseAviationApp.Server.Services.Interfaces;
using System;

namespace SynapseAviationApp.Server.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<UserListDto>> GetAllUsers()
        {

            var usersCreated = await _applicationDbContext.Users.AnyAsync();
            if (!usersCreated)
            {
                List<User> usersToAdd = new List<User>
                {
                    new User { FirstName = "Alice", LastName = "Smith", Age = 20},
                    new User { FirstName = "Bob", LastName = "Johnson", Age = 25},
                    new User { FirstName = "Charlie", LastName = "Williams", Age = 30},
                    new User { FirstName = "David", LastName = "Brown", Age = 50},
                    new User { FirstName = "Eva", LastName = "Miller", Age = 55},
                    new User { FirstName = "Frank", LastName = "Wilson", Age = 70},
                    new User { FirstName = "Grace", LastName = "Moore", Age = 100},
                    new User { FirstName = "Henry", LastName = "Taylor", Age = 22},
                    new User { FirstName = "Ivy", LastName = "Anderson", Age = 28},
                    new User { FirstName = "Jack", LastName = "Martinez", Age = 33}
                };

                await _applicationDbContext.Users.AddRangeAsync(usersToAdd);
                await _applicationDbContext.SaveChangesAsync();
            }
            return (await _applicationDbContext.Users.ToListAsync()).Select(user => user.ToUserListDto()).ToList();
        }

        public async Task<UserListDto> UpdateUser(Guid id, UserEditDto updatedUserData)
        {
            var userData = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (userData != null)
            {
                userData.FirstName = updatedUserData.FirstName;
                userData.LastName = updatedUserData.LastName;
                userData.Age = updatedUserData.Age;
                await _applicationDbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Updated data is null!");
            }
            return userData.ToUserListDto();
        }
    }
}
