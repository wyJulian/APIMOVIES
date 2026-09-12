using Application.Movies.Create;
using Application.Movies.Delete;
using Application.Movies.GetAll;
using Application.Movies.GetByCategory;
using Application.Movies.GetById;
using Application.Movies.Update;
using Domain.Movies;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APIMOVIES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ISender _sender;

        public MovieController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet(Name = "GetMoviesAsync")]
        public async Task<ActionResult<List<MovieDTO>>> GetMoviesAsync()
        {
            var movies = await _sender.Send(new GetAllMoviesQuery());
            return Ok(movies);
        }

        [HttpGet("{id:int}", Name = "GetMovieAsync")]
        public async Task<ActionResult<MovieDTO>> GetMovieAsync(int id)
        {
            try
            {
                var movie = await _sender.Send(new GetMovieByIdQuery { Id = id });
                if (movie == null)
                {
                    return NotFound();
                }

                return Ok(movie);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpGet("category/{category}", Name = "GetMoviesByCategoryAsync")]
        public async Task<ActionResult<List<MovieDTO>>> GetMoviesByCategoryAsync(string category)
        {
            try
            {
                var movies = await _sender.Send(new GetMoviesByCategoryQuery { Category = category });
                return Ok(movies);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpPost(Name = "CreateMovieAsync")]
        public async Task<ActionResult<MovieDTO>> CreateMovieAsync([FromBody] CreateMovieCommand command)
        {
            try
            {
                var created = await _sender.Send(command);
                return CreatedAtRoute("GetMovieAsync", new { id = created.Id }, created);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpPut("{id:int}", Name = "UpdateMovieAsync")]
        public async Task<ActionResult<MovieDTO>> UpdateMovieAsync([FromBody] UpdateMovieCommand command, int id)
        {
            command.Id = id;

            try
            {
                var updated = await _sender.Send(command);
                if (updated == null)
                {
                    return NotFound();
                }

                return Ok(updated);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteMovieAsync")]
        public async Task<ActionResult> DeleteMovieAsync(int id)
        {
            try
            {
                var deleted = await _sender.Send(new DeleteMovieCommand { Id = id });
                if (!deleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }
    }
}
