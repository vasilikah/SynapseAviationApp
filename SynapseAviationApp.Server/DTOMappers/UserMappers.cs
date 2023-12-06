using SynapseAviationApp.Server.Database;
using SynapseAviationApp.Server.DTOModels;
using SynapseAviationApp.Server.Models;

namespace SynapseAviationApp.Server.DTOMappers
{

    public static class UserMappers
    {
        public static UserListDto ToUserListDto(this User user)
        {
            return new UserListDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age
            };

        }

    }
}
