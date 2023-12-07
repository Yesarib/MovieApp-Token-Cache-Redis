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
    public class MovieRepositoryWithRedis:IMovieRepositoryForRedis
    {
        private const string movieKey = "MovieCaches";
        private readonly IMovieRepositoryForRedis _movieRepositoryForRedis;
        private readonly RedisService _redisService;
        private readonly IDatabase _database;
        public MovieRepositoryWithRedis(IMovieRepositoryForRedis movieRepositoryForRedis, RedisService redisService)
        {
            _movieRepositoryForRedis = movieRepositoryForRedis;
            _redisService = redisService;
            _database = _redisService.GetDb(2);
        }

        public async Task<Movie> CreateAsync(Movie movie)
        {
            var newMovie = await _movieRepositoryForRedis.CreateAsync(movie);

            // Yeni film başarıyla oluşturulduysa Redis'e ekleyin.
            await _database.HashSetAsync(movieKey, newMovie.Id, JsonSerializer.Serialize(newMovie));

            return newMovie;
        }

        public async Task<List<Movie>> GetAsync()
        {
            if (!await _database.KeyExistsAsync(movieKey))
            {
                return await LoadToCacheFromDbAsync();
            }

            var products = new List<Movie>();

            var cacheProducts = await _database.HashGetAllAsync(movieKey);
            foreach (var item in cacheProducts.ToList())
            {
                var product = JsonSerializer.Deserialize<Movie>(item.Value);
                products.Add(product);
            }
            return products;
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            if (_database.KeyExists(movieKey))
            {
                var movie = await _database.HashGetAsync(movieKey, id);
                return movie.HasValue ? JsonSerializer.Deserialize<Movie>(movie) : null;
            }

            var movies = await LoadToCacheFromDbAsync();

            return movies.FirstOrDefault(x => x.Id == id);
        }

        private async Task<List<Movie>> LoadToCacheFromDbAsync()
        {
            var movies = await _movieRepositoryForRedis.GetAsync();

            movies.ForEach(movie =>
            {
                _database.HashSetAsync(
                    movieKey,
                    movie.Id,
                    JsonSerializer.Serialize(movie)
                    );
            });

            return movies;
        }
    }
}
