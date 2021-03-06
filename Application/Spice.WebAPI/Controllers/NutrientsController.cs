﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spice.Application.Common.Exceptions;
using Spice.Application.Nutrients.Interfaces;
using Spice.Application.Nutrients.Models;
using Spice.Domain;
using Spice.ViewModels.Common;
using Spice.ViewModels.Nutrients;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutrientsController : ControllerBase
    {
        private readonly IQueryNutrients _queries;
        private readonly ICommandNutrients _commands;
        private readonly IMapper _mapper;

        public NutrientsController(IQueryNutrients queries, ICommandNutrients commands, IMapper mapper)
        {
            _queries = queries;
            _commands = commands;
            _mapper = mapper;
        }

        // GET api/nutrients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NutrientIndexViewModel>>> Get()
        {
            IEnumerable<Nutrient> nutrients = await _queries.GetAll();
            return Ok(_mapper.Map<IEnumerable<NutrientIndexViewModel>>(nutrients));
        }

        // GET api/nutrients/F3694C70-AC96-4BBC-9D70-7C1AF728E93F
        [HttpGet("{id:guid}", Name = nameof(GetNutrient))]
        public async Task<ActionResult<NutrientDetailsViewModel>> GetNutrient(Guid id)
        {
            NutrientDetailsModel nutrient = await _queries.Get(id);
            if (nutrient is null)
            {
                return NotFound();
            }

            return Ok(nutrient);
        }

        // POST api/nutrients
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateNutrientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                CreateNutrientModel createNutrientModel = _mapper.Map<CreateNutrientModel>(model);
                Guid nutrientId = await _commands.Create(createNutrientModel);
                return CreatedAtRoute(nameof(GetNutrient), new { id = nutrientId }, null);
            }
            catch (Exception ex) when (ex is ResourceStateException)
            {
                return Conflict(new ErrorViewModel(ex));
            }
        }

        // PUT api/nutrients/F3694C70-AC96-4BBC-9D70-7C1AF728E93F
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] UpdateNutrientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                UpdateNutrientModel updateNutrientModel = _mapper.Map<UpdateNutrientModel>(model);
                updateNutrientModel.Id = id;

                Nutrient nutrient = await _commands.Update(updateNutrientModel);
                if (nutrient is null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<NutrientDetailsViewModel>(nutrient));
            }
            catch (Exception ex) when (ex is ResourceStateException)
            {
                return Conflict(new ErrorViewModel(ex));
            }
        }

        // DELETE api/nutrients/F3694C70-AC96-4BBC-9D70-7C1AF728E93F
        [HttpDelete("{id:guid}")]
        public async Task<NoContentResult> Delete(Guid id)
        {
            await _commands.Delete(id);
            return NoContent();
        }
    }
}