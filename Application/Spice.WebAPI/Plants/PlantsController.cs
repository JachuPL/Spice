using Microsoft.AspNetCore.Mvc;
using Spice.Application.Plants;
using Spice.Application.Plants.Exceptions;
using Spice.Application.Plants.Models;
using Spice.Domain;
using Spice.WebAPI.Plants.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.WebAPI.Plants
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantsController : ControllerBase
    {
        private readonly IQueryPlants _queries;
        private readonly ICommandPlants _commands;

        public PlantsController(IQueryPlants queries, ICommandPlants commands)
        {
            _queries = queries;
            _commands = commands;
        }

        // GET api/plants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plant>>> Get()
        {
            return Ok(await _queries.GetAll());
        }

        // GET api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Plant>> Get(Guid id)
        {
            Plant plant = await _queries.Get(id);
            if (plant is null)
                return NotFound();

            return Ok(plant);
        }

        // POST api/plants
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreatePlantViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createPlantModel = new CreatePlantModel(); // TODO: map viewmodel to models
                Guid id = await _commands.Create(createPlantModel);
                return CreatedAtAction("Get", new { id = id });
            }
            catch (PlantExistsAtCoordinatesException ex)
            {
                return Conflict(new
                {
                    Error = ex.Message
                });
            }
        }

        // PUT api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] UpdatePlantViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatePlantModel = new UpdatePlantModel(); // TODO: map viewmodel to models
                Plant plant = await _commands.Update(updatePlantModel);
                if (plant is null)
                    return NotFound();

                return Ok(plant);
            }
            catch (PlantExistsAtCoordinatesException ex)
            {
                return Conflict(new
                {
                    Error = ex.Message
                });
            }
        }

        // DELETE api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F
        [HttpDelete("{id:guid}")]
        public async Task<NoContentResult> Delete(Guid id)
        {
            await _commands.Delete(id);
            return NoContent();
        }
    }
}