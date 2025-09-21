using DataAccounts.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccounts
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        public async Task Register(User user, string password, string role)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, role);

                    if (roleResult.Succeeded)
                    {
                        await _unitOfWork.CommitAsync();
                    }
                    else
                    {
                        throw new InvalidOperationException("Failed to assign role to user.");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Failed to create user.");
                }
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw new OperationCanceledException("Transaction was rolled back due to an error.");
            }
        }

        public async Task<bool> SignIn(string email, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
                return result.Succeeded;
            } 
            return false;
        }

        public async Task ChangePasswordAsync(string email, string oldPasswordld, string newPasswordld)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

            var result = await _userManager.ChangePasswordAsync(user, oldPasswordld, newPasswordld);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
            }
        }

        public async Task<IList<string>> GetUserRoles(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public Task<User?> GetByEmail(string email) =>
            _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }       
}