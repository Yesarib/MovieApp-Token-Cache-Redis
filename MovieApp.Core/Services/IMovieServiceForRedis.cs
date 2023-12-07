using MovieApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Core.Services
{
    public interface IMovieServiceForRedis
    {
        Task<List<Movie>> GetAsync();
        Task<Movie> GetByIdAsync(int id);
        Task<Movie> CreateAsync(Movie movie);
    }
}
