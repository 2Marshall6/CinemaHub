

using BusinessLogic.Model;
using BusinessLogic.Model.AuthenticationAndAuthorizationModel;

namespace BusinessLogic
{
    public interface IUserService
    {
        Task RegisterUserAsync(RegisterUserRequest model);
        Task<string> LoginUserAsync(LoginUserRequest model);
        Task ChangePasswordAsync(ChangePasswordModel model);
    }
}
