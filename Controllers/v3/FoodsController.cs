using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodApi.Controllers.v3
{
    /// <summary>
    /// Manages food-related operations.
    /// </summary>
    [ApiController]
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FoodController : ControllerBase
    {
        /// <summary>
        /// Retrieves a list of all food items.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Get all food items", Description = "Retrieves a list of all food items.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FoodItemDto>))]
        public ActionResult<IEnumerable<FoodItemDto>> GetAllFoods()
        {
            return Ok(new List<FoodItemDto>
            {
                new FoodItemDto { Id = 1, Name = "Apple", Calories = 95 },
                new FoodItemDto { Id = 2, Name = "Banana", Calories = 105 }
            });
        }

        /// <summary>
        /// Retrieves a specific food item by ID.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a food item by ID", Description = "Retrieves a specific food item by its unique ID.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FoodItemDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<FoodItemDto> GetFoodById(int id)
        {
            if (id <= 0) return NotFound();

            return Ok(new FoodItemDto { Id = id, Name = "Apple", Calories = 95 });
        }

        /// <summary>
        /// Retrieves food details (deprecated method).
        /// </summary>
        /// <remarks>
        /// **This method is obsolete. Use `GetFoodById` instead.**
        /// Example usage:
        /// ```http
        /// GET /api/v1/Food/details/1
        /// ```
        /// **Response:**
        /// ```json
        /// { "id": 1, "name": "Apple", "calories": 95 }
        /// ```
        /// **Error Response:**
        /// ```json
        /// { "error": "Not Found" }
        /// ```
        /// </remarks>
        /// <param name="id">The unique identifier of the food item.</param>
        /// <response code="200">The food item was successfully retrieved.</response>
        /// <response code="404">The food item with the given ID was not found.</response>
        [Obsolete("This method is deprecated. Use GetFoodById instead.")]
        [HttpGet("details/{id}")]
        [SwaggerOperation(Summary = "Get food details (deprecated)", Description = "Retrieves detailed food information (use `GetFoodById` instead).")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FoodItemDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<FoodItemDto> GetFoodDetails(int id)
        {
            if (id <= 0) return NotFound();

            return Ok(new FoodItemDto { Id = id, Name = "Apple", Calories = 95 });
        }

        /// <summary>
        /// Creates a new food item.
        /// </summary>
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new food item", Description = "Creates a new food item in the system.")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FoodItemDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<FoodItemDto> CreateFood(FoodItemDto foodItem)
        {
            if (string.IsNullOrEmpty(foodItem.Name) || foodItem.Calories <= 0)
            {
                return BadRequest("Invalid food item details.");
            }

            foodItem.Id = new Random().Next(1, 1000); // Simulate ID generation
            return CreatedAtAction(nameof(GetFoodById), new { id = foodItem.Id }, foodItem);
        }

        /// <summary>
        /// Updates an existing food item.
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an existing food item", Description = "Updates a specific food item.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FoodItemDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<FoodItemDto> UpdateFood(int id, FoodItemDto foodItem)
        {
            if (id != foodItem.Id) return BadRequest("Food item ID mismatch.");
            if (id <= 0) return NotFound();

            return Ok(foodItem); // Return the updated food item
        }

        /// <summary>
        /// Deletes a food item by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a food item", Description = "Deletes a specific food item.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteFood(int id)
        {
            if (id <= 0) return NotFound();

            return Ok(new { Message = "Food item deleted successfully." });
        }

        /// <summary>
        /// Partially updates a food item (e.g., updating only calories).
        /// </summary>
        [HttpPatch("{id}")]
        [SwaggerOperation(Summary = "Partially update a food item", Description = "Partially updates an existing food item.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FoodItemDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<FoodItemDto> PatchFood(int id, [FromBody] JsonPatchDocument<FoodItemDto> patchDoc)
        {
            if (id <= 0) return NotFound();
            if (patchDoc == null) return BadRequest("Invalid patch data.");

            var foodItem = new FoodItemDto { Id = id, Name = "Apple", Calories = 95 };

            patchDoc.ApplyTo(foodItem, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(foodItem);
        }
    }

    /// <summary>
    /// Represents a food item.
    /// </summary>
    public class FoodItemDto
    {
        /// <summary>
        /// Unique identifier of the food item.
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// The name of the food item.
        /// </summary>
        /// <example>Apple</example>
        public string Name { get; set; }

        /// <summary>
        /// The number of calories in the food item.
        /// </summary>
        /// <example>95</example>
        public int Calories { get; set; }
    }
}