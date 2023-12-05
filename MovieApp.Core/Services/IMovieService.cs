using MovieApp.Core.Dtos;
using MovieApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Services
{
    public interface IMovieService:IServiceGeneric<Movie,MovieDto>
    {
        Task<ResponseDto<IEnumerable<Movie>>> GetMovies();
    }
}
