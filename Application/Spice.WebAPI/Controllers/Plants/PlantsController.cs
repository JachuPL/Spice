using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spice.Application.Common.Exceptions;
using Spice.Application.Plants.Interfaces;
using Spice.Application.Plants.Models;
using Spice.Domain.Plants;
using Spice.ViewModels.Common;
using Spice.ViewModels.Plants;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.WebAPI.Controllers.Plants
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantsController : ControllerBase
    {
        private readonly IQueryPlants _queries;
        private readonly ICommandPlants _commands;
        private readonly IMapper _mapper;

        public PlantsController(IQueryPlants queries, ICommandPlants commands, IMapper mapper)
        {
            _queries = queries;
            _commands = commands;
            _mapper = mapper;
        }

        // GET api/plants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantIndexViewModel>>> Get()
        {
            IEnumerable<Plant> plants = await _queries.GetAll();
            return Ok(_mapper.Map<IEnumerable<PlantIndexViewModel>>(plants));
        }

        // GET api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F
        [HttpGet("{id:guid}", Name = nameof(GetPlant))]
        public async Task<ActionResult<PlantDetailsViewModel>> GetPlant(Guid id)
        {
            Plant plant = await _queries.Get(id);
            if (plant is null)
                return NotFound();

            return Ok(_mapper.Map<PlantDetailsViewModel>(plant));
        }

        // POST api/plants
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreatePlantViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                CreatePlantModel createPlantModel = _mapper.Map<CreatePlantModel>(model);
                Guid plantId = await _commands.Create(createPlantModel);
                return CreatedAtRoute(nameof(GetPlant), new { id = plantId }, null);
            }
            catch (Exception ex) when (ex is ResourceNotFoundException)
            {
                return NotFound(new ErrorViewModel(ex));
            }
            catch (Exception ex) when (ex is ResourceStateException)
            {
                return Conflict(new ErrorViewModel(ex));
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
                UpdatePlantModel updatePlantModel = _mapper.Map<UpdatePlantModel>(model);
                updatePlantModel.Id = id;

                Plant plant = await _commands.Update(updatePlantModel);
                if (plant is null)
                    return NotFound();

                return Ok(_mapper.Map<PlantDetailsViewModel>(plant));
            }
            catch (Exception ex) when (ex is ResourceNotFoundException)
            {
                return NotFound(new ErrorViewModel(ex));
            }
            catch (Exception ex) when (ex is ResourceStateException)
            {
                return Conflict(new ErrorViewModel(ex));
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