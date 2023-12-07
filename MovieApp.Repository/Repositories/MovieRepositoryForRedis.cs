
using Microsoft.EntityFrameworkCore;
using MovieApp.Cache;
using MovieApp.Core.Models;
using MovieApp.Core.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieApp.Repository.Repositories
{
    public class MovieRepositoryForRedis : IMovieRepositoryForRedis
    {
        private readonly AppDbContext _appDbContextForRedis;

        public MovieRepositoryForRedis(AppDbContext appDbContextForRedis)
        {
            _appDbContextForRedis = appDbContextForRedis;
        }

        public async Task<Movie> CreateAsync(Movie movie)
        {
            _appDbContextForRedis.Movies.AddAsync(movie);
            await _appDbContextForRedis.SaveChangesAsync();
            return movie;
        }

        public async Task<List<Movie>> GetAsync()
        {
            return await _appDbContextForRedis.Movies.ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _appDbContextForRedis.Movies.FindAsync(id);
        }
    }
}
