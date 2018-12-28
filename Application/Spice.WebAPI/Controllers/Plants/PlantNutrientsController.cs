﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spice.Application.Nutrients.Exceptions;
using Spice.Application.Plants.Exceptions;
using Spice.Application.Plants.Interfaces;
using Spice.Application.Plants.Models;
using Spice.Domain.Plants;
using Spice.ViewModels.Plants.AdministeredNutrients;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.WebAPI.Controllers.Plants
{
    [Route("api/plants/{plantId:guid}/nutrients")]
    [ApiController]
    public class PlantNutrientsController : ControllerBase
    {
        private readonly IQueryPlantNutrients _queries;
        private readonly ICommandPlantNutrients _commands;
        private readonly IMapper _mapper;

        public PlantNutrientsController(IQueryPlantNutrients queries, ICommandPlantNutrients commands, IMapper mapper)
        {
            _queries = queries;
            _commands = commands;
            _mapper = mapper;
        }

        // GET api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/nutrients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdministeredNutrientsIndexViewModel>>> Get([FromRoute] Guid plantId)
        {
            IEnumerable<AdministeredNutrient> administeredNutrients = await _queries.GetByPlant(plantId);
            return Ok(_mapper.Map<IEnumerable<AdministeredNutrientsIndexViewModel>>(administeredNutrients));
        }

        // GET api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/nutrients/DC117408-630E-459B-B16F-DE36EBC58E8F
        [HttpGet("{id:guid}", Name = nameof(GetAdministeredNutrient))]
        public async Task<ActionResult<AdministeredNutrientDetailsViewModel>> GetAdministeredNutrient([FromRoute] Guid plantId, Guid id)
        {
            AdministeredNutrient administeredNutrient = await _queries.Get(plantId, id);
            if (administeredNutrient is null)
                return NotFound();

            return Ok(_mapper.Map<AdministeredNutrientDetailsViewModel>(administeredNutrient));
        }

        // POST api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/nutrients
        [HttpPost]
        public async Task<ActionResult> Post([FromRoute] Guid plantId, [FromBody] CreateAdministeredNutrientViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                CreateAdministeredNutrientModel createAdministeredNutrientModel = _mapper.Map<CreateAdministeredNutrientModel>(model);
                Guid nutrientId = await _commands.Create(createAdministeredNutrientModel);
                return CreatedAtRoute(nameof(GetAdministeredNutrient), new { plantId = plantId, id = nutrientId }, null);
            }
            catch (PlantDoesNotExistException ex)
            {
                return Conflict(new
                {
                    Error = ex.Message
                });
            }
            catch (NutrientDoesNotExistException ex)
            {
                return Conflict(new
                {
                    Error = ex.Message
                });
            }
            catch (NutrientApplicationDateBeforePlantDateException ex)
            {
                return Conflict(new
                {
                    Error = ex.Message
                });
            }
        }

        // PUT api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/nutrients/DC117408-630E-459B-B16F-DE36EBC58E8F
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put([FromRoute] Guid plantId, Guid id, [FromBody] UpdateAdministeredNutrientViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                UpdateAdministeredNutrientModel updateAdministeredNutrientModel = _mapper.Map<UpdateAdministeredNutrientModel>(model);
                updateAdministeredNutrientModel.Id = id;

                AdministeredNutrient administeredNutrient = await _commands.Update(updateAdministeredNutrientModel);
                if (administeredNutrient is null)
                    return NotFound();

                return Ok(_mapper.Map<UpdateAdministeredNutrientViewModel>(administeredNutrient));
            }
            catch (PlantDoesNotExistException ex)
            {
                return Conflict(new
                {
                    Error = ex.Message
                });
            }
            catch (NutrientDoesNotExistException ex)
            {
                return Conflict(new
                {
                    Error = ex.Message
                });
            }
            catch (NutrientApplicationDateBeforePlantDateException ex)
            {
                return Conflict(new
                {
                    Error = ex.Message
                });
            }
        }

        // DELETE api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/nutrients/DC117408-630E-459B-B16F-DE36EBC58E8F
        [HttpDelete("{id:guid}")]
        public async Task<NoContentResult> Delete([FromRoute]Guid plantId, Guid id)
        {
            await _commands.Delete(plantId, id);
            return NoContent();
        }

        // GET api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/nutrients/sum
        [HttpGet("sum")]
        public async Task<ActionResult<IEnumerable<AdministeredPlantNutrientsSummaryViewModel>>> GetSummary([FromRoute] Guid plantId)
        {
            try
            {
                IEnumerable<AdministeredPlantNutrientsSummaryModel> administeredNutrient = await _queries.Sum(plantId);

                return Ok(_mapper.Map<IEnumerable<AdministeredPlantNutrientsSummaryViewModel>>(administeredNutrient));
            }
            catch (PlantDoesNotExistException)
            {
                return NotFound();
            }
        }
    }
}