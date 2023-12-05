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

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
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
    }
}
