using SynapseAviationApp.Server.DTOModels;
using SynapseAviationApp.Server.Models;

namespace SynapseAviationApp.Server.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserListDto>> GetAllUsers();
        Task<UserListDto> UpdateUser(Guid id, UserEditDto updatedUserData);
    }
}
