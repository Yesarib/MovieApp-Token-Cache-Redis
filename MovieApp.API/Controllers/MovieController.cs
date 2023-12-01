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
        private readonly IServiceGeneric<Movie,MovieDto> _serviceGeneric;

        public MovieController(IServiceGeneric<Movie, MovieDto> serviceGeneric)
        {
            _serviceGeneric = serviceGeneric;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            return ActionResultInstance(await _serviceGeneric.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie(MovieDto movieDto)
        {
            return ActionResultInstance(await _serviceGeneric.AddAsync(movieDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMovie(MovieDto movieDto)
        {
            return ActionResultInstance(await _serviceGeneric.Update(movieDto, movieDto.Id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            return ActionResultInstance(await _serviceGeneric.Remove(id));
        }
    }
}
