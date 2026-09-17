using Application.Categories.Create;
using Application.Categories.Delete;
using Application.Categories.GetAll;
using Application.Categories.GetById;
using Application.Categories.Update;
using Domain.Categories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace APIMOVIES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ISender _sender;

        public CategoryController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet(Name = "GetCategoriesAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategoryDTO>>> GetCategoriesAsync()
        {
            var categories = await _sender.Send(new GetAllCategoriesQuery());
            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategoryAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDTO>> GetCategoryAsync(int id)
        {
            try
            {
                var category = await _sender.Send(new GetCategoryByIdQuery { Id = id });
                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpPost(Name = "CreateCategoryAsync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryDTO>> CreateCategoryAsync([FromBody] CreateCategoryCommand command)
        {
            try
            {
                var created = await _sender.Send(command);
                return CreatedAtRoute("GetCategoryAsync", new { id = created.Id }, created);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpPut("{id:int}", Name = "UpdateCategoryAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDTO>> UpdateCategoryAsync([FromBody] UpdateCategoryCommand command, int id)
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

        [HttpDelete("{id:int}", Name = "DeleteCategoryAsync")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            try
            {
                var deleted = await _sender.Send(new DeleteCategoryCommand { Id = id });
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
