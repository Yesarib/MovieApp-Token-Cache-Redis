using Microsoft.Extensions.Caching.Memory;
using MovieApp.Cache;
using MovieApp.Core.Dtos;
using MovieApp.Core.Models;
using MovieApp.Core.Repositories;
using MovieApp.Core.Services;
using MovieApp.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Services
{
    public class MovieService : GenericService<Movie, MovieDto>, IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly CacheService _cacheService;
        private string MovieList => "MovieList";
        public MovieService(IUnitOfWork unitOfWork, IGenericRepository<Movie> genericRepository, IMovieRepository movieRepository, IMemoryCache memoryCache, CacheService cacheService) : base(unitOfWork, genericRepository)
        {
            _movieRepository = movieRepository;
            _cacheService = cacheService;
        }

        public async Task<ResponseDto<IEnumerable<Movie>>> GetMovies()
        {
            var movies = _cacheService.Get<List<Movie>>(MovieList);

            if (movies == null)
            {
                movies = await _movieRepository.GetMovies();
                _cacheService.Set(MovieList, movies, TimeSpan.FromMinutes(10));
            }

            return ResponseDto<IEnumerable<Movie>>.Success(movies, 200);
        }
    }
}
