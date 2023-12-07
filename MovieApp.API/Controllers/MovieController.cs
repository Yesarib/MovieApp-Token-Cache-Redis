using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Core.Dtos;
using MovieApp.Core.Models;
using MovieApp.Core.Services;

namespace MovieApp.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : CustomBaseController
    {
        private readonly IMovieService _movieService;
        private readonly IMovieServiceForRedis _movieServiceForRedis;
        public MovieController(IMovieService movieService, IMovieServiceForRedis movieServiceForRedis)
        {
            _movieService = movieService;
            _movieServiceForRedis = movieServiceForRedis;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            return ActionResultInstance(await _movieService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie(MovieDto movieDto)
        {
            return ActionResultInstance(await _movieService.AddAsync(movieDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMovie(MovieDto movieDto)
        {
            return ActionResultInstance(await _movieService.Update(movieDto, movieDto.Id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            return ActionResultInstance(await _movieService.Remove(id));
        }

        [HttpGet("getMoviesFromCache")]
        public async Task<IActionResult> GetMoviesFromCache()
        {
            return ActionResultInstance(await _movieService.GetMovies());
        }

        [HttpPost("addRedis")]
        public async Task<IActionResult> AddMovieToRedis(Movie movie)
        {
            return Ok(await _movieServiceForRedis.CreateAsync(movie));
        }
        [HttpGet("getMoviesFromRedis")]
        public async Task<IActionResult> GetMoviesFromRedis()
        {
            return Ok(await _movieServiceForRedis.GetAsync());
        }
        [HttpGet("getByIdFromRedis/{id}")]
        public async Task<IActionResult> GetMoviesByIdFromRedis(int id)
        {
            return Ok(await _movieServiceForRedis.GetByIdAsync(id));
        }
    }
}
