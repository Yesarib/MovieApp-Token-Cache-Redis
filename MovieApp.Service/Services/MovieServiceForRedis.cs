using MovieApp.Core.Models;
using MovieApp.Core.Repositories;
using MovieApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Services
{
    public class MovieServiceForRedis : IMovieServiceForRedis
    {
        private readonly IMovieRepositoryForRedis _movieRepositoryForRedis;

        public MovieServiceForRedis(IMovieRepositoryForRedis movieRepositoryForRedis)
        {
            _movieRepositoryForRedis = movieRepositoryForRedis;
        }

        public async Task<Movie> CreateAsync(Movie movie)
        {
            return await _movieRepositoryForRedis.CreateAsync(movie);
        }

        public async Task<List<Movie>> GetAsync()
        {
            return await _movieRepositoryForRedis.GetAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _movieRepositoryForRedis.GetByIdAsync(id);
        }
    }
}
