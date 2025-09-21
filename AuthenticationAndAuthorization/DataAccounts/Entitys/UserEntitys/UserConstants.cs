using System.Security.Claims;

namespace DataAccounts
{
    public static class UserConstants
    {
        public const string Admin = "Admin";
        public const string User = "User";

        public static readonly IReadOnlyList<string> Roles = new List<string> { Admin, User };
    }
}
