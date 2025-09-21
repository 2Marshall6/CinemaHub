using Microsoft.AspNetCore.Identity;
using DataAccounts;
using System.Threading.Tasks;

namespace AuthenticationAndAuthorization
{
    public class RoleProvider
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public RoleProvider(RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task AddRoles()
        {
            var roles = UserConstants.Roles;

            foreach (var roleName in roles)
            {
                try
                {
                    var roleExists = await _roleManager.RoleExistsAsync(roleName);
                    if (!roleExists)
                    {
                        var result = await _roleManager.CreateAsync(new IdentityRole<Guid> { Name = roleName });
                        if (!result.Succeeded)
                        {
                            Console.WriteLine($"Error creating role {roleName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception while creating role {roleName}: {ex.Message}");
                }
            }
        }

    }
}