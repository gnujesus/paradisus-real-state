//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using RealStateApp.Core.Application.Dtos.Example;
//using RealStateApp.Core.Application.Interfaces.Services;

//namespace RealStateApp.WebApi.Controllers.v1
//{
//    [ApiVersion("1.0")]
//    [Authorize(Roles = "Admin")]
//    public class ExampleController : BaseApiController
//    {
//        private readonly IExampleService _exampleService;

//        public ExampleController(IExampleService exampleService)
//        {
//            _exampleService = exampleService;
//        }

//        [HttpGet]
//        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExampleDto))]
//        [ProducesResponseType(StatusCodes.Status204NoContent)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<IActionResult> List()
//        {
//            var examples = await _exampleService.ListAsync();

//            if (examples == null || (examples.Count() == 0))
//            {
//                return NotFound("No hay ejemplos");
//            }

//            return Ok(examples);
//        }

//        [HttpPost]
//        [ProducesResponseType(StatusCodes.Status201Created)]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        public async Task<IActionResult> Create(ExampleForCreationDto example)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest();
//            }

//            var createdExample = await _exampleService.CreateAsync(example);
//            return CreatedAtAction(nameof(GetById), new { id = createdExample.Id }, createdExample);
//        }

//        [HttpPut("{id}")]
//        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExampleDto))]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        [ProducesResponseType(StatusCodes.Status400BadRequest)]
//        public async Task<IActionResult> Update(Guid id, ExampleForUpdateDto example)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest();
//            }

//            var updatedExample = await _exampleService.UpdateAsync(id, example);

//            return Ok(updatedExample);
//        }

//        [HttpGet("{id}")]
//        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExampleDto))]
//        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
//        [ProducesResponseType(StatusCodes.Status204NoContent)]
//        public async Task<IActionResult> GetById(Guid id)
//        {
//            var example = await _exampleService.GetByIdAsync(id);

//            if (example == null)
//            {
//                return NotFound("No existe el ejemplo");
//            }

//            return Ok(example);
//        }
//    }
//}
