using AutoMapper;
using MovieApp.Core.Dtos;
using MovieApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service
{
    public class DtoMapper:Profile
    {
        public DtoMapper()
        {
            CreateMap<MovieDto, Movie>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
