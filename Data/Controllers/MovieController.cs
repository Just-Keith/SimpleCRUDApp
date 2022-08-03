using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RazorPagesMovie : ControllerBase
    {

        // using the connection in the file Data/DataCharacterContext.cs

        private readonly RazorPagesMovieContext _razorpagesmovieContext;

        // asyning the connection 
        public RazorPagesMovie(RazorPagesMovieContext context)
        {
            _razorpagesmovieContext = context;
        }

        [HttpGet]
        // method to get all characters
        public async Task<ActionResult<List<Movie>>> Get()
        {
            // _characterContext.Characters calling the getters an setter from the context 

            return Ok(await _razorpagesmovieContext.Movie.ToListAsync());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<List<Movie>>> GetSingleMovie(int id)
        {
            var movie = await _razorpagesmovieContext.Movie.FindAsync(id);
            if (movie== null) 
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<List<Movie>>> AddMovie(Movie  movie)
        {
            try
            {
                _razorpagesmovieContext.Movie.Add(movie);
                await _razorpagesmovieContext.SaveChangesAsync();
                return Ok(await _razorpagesmovieContext.Movie.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]

        public async Task<ActionResult> UpdateMovie(Movie movie)
        {
            var specificMovie = await _razorpagesmovieContext.Movie.FindAsync(movie.ID);
            if(specificMovie == null) // if character is null return bad request error not found 
            {
                return BadRequest("No here found");

            }else
            {
                specificMovie.ID = movie.ID;
                specificMovie.Title = movie.Title;
                specificMovie.ReleaseDate = movie.ReleaseDate;
                specificMovie.Genre= movie.Genre;
                specificMovie.Price = movie.Price;
                specificMovie.Rating = movie.Rating;
                await _razorpagesmovieContext.SaveChangesAsync();
            }

            return Ok(await _razorpagesmovieContext.Movie.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Movie>>> DeleteMovie(int movie)
        {
            var specificMovie= await _razorpagesmovieContext.Movie.FindAsync(movie);

            if (specificMovie == null)
            {
                return BadRequest("No character found");
            }
            
            _razorpagesmovieContext.Movie.Remove(specificMovie);
            await _razorpagesmovieContext.SaveChangesAsync();

            return Ok(await _razorpagesmovieContext.Movie.ToListAsync());
        }
    }
}
