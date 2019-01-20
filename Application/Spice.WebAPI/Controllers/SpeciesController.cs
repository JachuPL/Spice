using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spice.Application.Common.Exceptions;
using Spice.Application.Species.Interfaces;
using Spice.Application.Species.Models;
using Spice.Domain;
using Spice.ViewModels.Common;
using Spice.ViewModels.Species;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly IQuerySpecies _queries;
        private readonly ICommandSpecies _commands;
        private readonly IMapper _mapper;

        public SpeciesController(IQuerySpecies queries, ICommandSpecies commands, IMapper mapper)
        {
            _queries = queries;
            _commands = commands;
            _mapper = mapper;
        }

        // GET api/species
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpeciesIndexViewModel>>> Get()
        {
            IEnumerable<Species> species = await _queries.GetAll();
            return Ok(_mapper.Map<IEnumerable<SpeciesIndexViewModel>>(species));
        }

        // GET api/species/F3694C70-AC96-4BBC-9D70-7C1AF728E93F
        [HttpGet("{id:guid}", Name = nameof(GetSpecies))]
        public async Task<ActionResult<SpeciesDetailsViewModel>> GetSpecies(Guid id)
        {
            Species species = await _queries.Get(id);
            if (species is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SpeciesDetailsViewModel>(species));
        }

        // POST api/species
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateSpeciesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                CreateSpeciesModel createSpeciesModel = _mapper.Map<CreateSpeciesModel>(model);
                Guid speciesId = await _commands.Create(createSpeciesModel);
                return CreatedAtRoute(nameof(GetSpecies), new { id = speciesId }, speciesId);
            }
            catch (Exception ex) when (ex is ResourceStateException)
            {
                return Conflict(new ErrorViewModel(ex));
            }
        }

        // PUT api/species/F3694C70-AC96-4BBC-9D70-7C1AF728E93F
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] UpdateSpeciesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                UpdateSpeciesModel updateSpeciesModel = _mapper.Map<UpdateSpeciesModel>(model);
                updateSpeciesModel.Id = id;

                Species species = await _commands.Update(updateSpeciesModel);
                if (species is null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<SpeciesDetailsViewModel>(species));
            }
            catch (Exception ex) when (ex is ResourceStateException)
            {
                return Conflict(new ErrorViewModel(ex));
            }
        }

        // DELETE api/species/F3694C70-AC96-4BBC-9D70-7C1AF728E93F
        [HttpDelete("{id:guid}")]
        public async Task<NoContentResult> Delete(Guid id)
        {
            await _commands.Delete(id);
            return NoContent();
        }

        // GET api/species/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/summary?fromDate=2018-12-01T00:00:00&toDate=2018-12-31T23:59:59
        [HttpGet("{id:guid}/summary")]
        public async Task<ActionResult<IEnumerable<SpeciesNutritionSummaryViewModel>>> GetSummary(Guid id, [FromQuery]DateTime? fromDate = null, [FromQuery]DateTime? toDate = null)
        {
            IEnumerable<SpeciesNutritionSummaryModel> nutritionSummary = await _queries.Summary(id, fromDate, toDate);
            if (nutritionSummary is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<SpeciesNutritionSummaryViewModel>>(nutritionSummary));
        }
    }
}