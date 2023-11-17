using MovieApp.Core.Dtos;
using MovieApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Services
{
    public interface IUserService:IServiceGeneric<User,UserDto>
    {
        Task<ResponseDto<UserDto>> CreateUserAync(RegisterDto registerDto);
        Task<ResponseDto<UserDto>> GetUserByName(string userName);
    }
}
