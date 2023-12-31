﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MovieApp.Core.Dtos;
using MovieApp.Core.Models;
using MovieApp.Core.Repositories;
using MovieApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Repository.Repositories
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly string MovieListCacheKey = "MovieList";
        public MovieRepository(AppDbContext context, IMemoryCache memoryCache) : base(context)
        {
            _memoryCache = memoryCache;
        }

        public async Task<List<Movie>> GetMovies()
        {
            var movies = await base.GetAllAsync();
            return movies.ToList();
        }
    }
}
