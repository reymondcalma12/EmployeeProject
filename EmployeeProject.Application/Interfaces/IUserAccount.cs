using EmployeeProject.Application.DTOs.Account;
using EmployeeProject.Core.Entities.Model;
using EmployeeProject.Core.Entities.User;

namespace EmployeeProject.Application.Interfaces
{
    public interface IUserAccount
    {
        Task<bool> CreateAccount(RegisterDTO userDTO);

        Task<(bool, string, bool)> LoginAccount(LoginDTO loginDTO);

        Task<(bool, string)> ChangePassword(ChangePasswordDTO changePasswordDTO);
 
        Task<bool> Logout();

    }
}
