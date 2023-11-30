using AutoMapper.Internal.Mappers;
using Microsoft.AspNetCore.Identity;
using MovieApp.Core.Dtos;
using MovieApp.Core.Models;
using MovieApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResponseDto<UserDto>> CreateUserAync(RegisterDto registerDto)
        {
            var user = new User { Email= registerDto.Email, UserName = registerDto.UserName};

            var result = await _userManager.CreateAsync(user,registerDto.Password);
            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(x=>x.Description).ToList();
                return ResponseDto<UserDto>.Fail(new ErrorDto(errors, true), 400);
            }
            return ResponseDto<UserDto>.Success(ObejctMapper.Mapper.Map<UserDto>(user),200);
        }

        public async Task<ResponseDto<UserDto>> GetUserByName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return ResponseDto<UserDto>.Fail("UserName not found", 404, true);
            }

            return ResponseDto<UserDto>.Success(ObejctMapper.Mapper.Map<UserDto>(user), 200);
        }
    }
}
