using AutoMapper;
using BusinessLogic;
using BusinessLogic.Model;
using BusinessLogic.Model.AuthenticationAndAuthorizationModel;
using DataAccounts;
using DataAccounts.Repositories;
using Microsoft.AspNetCore.Identity;
using System;

namespace BusinessLogic
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepositories;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        
        public UserService(IUserRepository userRepositories, IJwtService jwtService, IMapper mapper)
        {
            _userRepositories = userRepositories;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<string> LoginUserAsync(LoginUserRequest model)
        {
            var isPersistent = true;
            var lockoutOnFailure = false;
            var tokenLifeTime = 5;

            var user = await _userRepositories.GetByEmail(model.Email);
            var roles = await _userRepositories.GetUserRoles(user);

            var result = await _userRepositories.SignIn(model.Email,model.Password, isPersistent, lockoutOnFailure);

            if (result == false)
            {
                throw new InvalidOperationException("Password is not correct!");
            }

            return _jwtService.GenerateJwt(user.Id, user.UserName, roles);
        }

        public async Task RegisterUserAsync(RegisterUserRequest request)
        {
            string role = UserConstants.User;

            if (request.Email.Equals("admin@example.com", StringComparison.OrdinalIgnoreCase))
            {
                role = UserConstants.Admin;
            }

            var user = _mapper.Map<User>(request);

            await _userRepositories.Register(user,request.Password,role);
        }

        public async Task ChangePasswordAsync(ChangePasswordModel model)
        {
            await _userRepositories.ChangePasswordAsync(model.Email, model.OldPassword, model.NewPassword);
        }
    }
}
