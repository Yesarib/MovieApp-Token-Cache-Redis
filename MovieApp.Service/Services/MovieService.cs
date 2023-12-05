using Microsoft.Extensions.Caching.Memory;
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
        public MovieService(IUnitOfWork unitOfWork, IGenericRepository<Movie> genericRepository, IMovieRepository movieRepository, IMemoryCache memoryCache) : base(unitOfWork, genericRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<ResponseDto<IEnumerable<Movie>>> GetMovies()
        {
            var movies = await _movieRepository.GetMovies();
            return ResponseDto<IEnumerable<Movie>>.Success(movies, 200);
        }
    }
}
