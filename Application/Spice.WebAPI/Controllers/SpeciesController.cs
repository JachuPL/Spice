using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spice.Application.Species.Exceptions;
using Spice.Application.Species.Interfaces;
using Spice.Application.Species.Models;
using Spice.Domain.Plants;
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

        // GET api/Species
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpeciesIndexViewModel>>> Get()
        {
            IEnumerable<Species> Species = await _queries.GetAll();
            return Ok(_mapper.Map<IEnumerable<SpeciesIndexViewModel>>(Species));
        }

        // GET api/Species/F3694C70-AC96-4BBC-9D70-7C1AF728E93F
        [HttpGet("{id:guid}", Name = nameof(GetSpecies))]
        public async Task<ActionResult<SpeciesDetailsViewModel>> GetSpecies(Guid id)
        {
            Species species = await _queries.Get(id);
            if (species is null)
                return NotFound();

            return Ok(_mapper.Map<SpeciesDetailsViewModel>(species));
        }

        // POST api/Species
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateSpeciesViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                CreateSpeciesModel createSpeciesModel = _mapper.Map<CreateSpeciesModel>(model);
                Guid speciesId = await _commands.Create(createSpeciesModel);
                return CreatedAtRoute(nameof(GetSpecies), new { id = speciesId }, null);
            }
            catch (SpeciesWithNameAlreadyExistsException ex)
            {
                return Conflict(new
                {
                    Error = ex.Message
                });
            }
        }

        // PUT api/Species/F3694C70-AC96-4BBC-9D70-7C1AF728E93F
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] UpdateSpeciesViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                UpdateSpeciesModel updateSpeciesModel = _mapper.Map<UpdateSpeciesModel>(model);
                updateSpeciesModel.Id = id;

                Species species = await _commands.Update(updateSpeciesModel);
                if (species is null)
                    return NotFound();

                return Ok(_mapper.Map<SpeciesDetailsViewModel>(species));
            }
            catch (SpeciesDoesNotExistException)
            {
                return NotFound();
            }
            catch (SpeciesWithNameAlreadyExistsException ex)
            {
                return Conflict(new
                {
                    Error = ex.Message
                });
            }
        }

        // DELETE api/Species/F3694C70-AC96-4BBC-9D70-7C1AF728E93F
        [HttpDelete("{id:guid}")]
        public async Task<NoContentResult> Delete(Guid id)
        {
            await _commands.Delete(id);
            return NoContent();
        }
    }
}