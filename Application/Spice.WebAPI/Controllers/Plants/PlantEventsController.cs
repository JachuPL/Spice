﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spice.Application.Plants.Exceptions;
using Spice.Application.Plants.Interfaces;
using Spice.Application.Plants.Models;
using Spice.Domain.Plants.Events;
using Spice.ViewModels.Plants.OccuredEvents;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.WebAPI.Controllers.Plants
{
    [Route("api/plants/{plantId:guid}/events")]
    [ApiController]
    public class PlantEventsController : ControllerBase
    {
        private readonly IQueryPlantEvents _queries;
        private readonly ICommandPlantEvents _commands;
        private readonly IMapper _mapper;

        public PlantEventsController(IQueryPlantEvents queries, ICommandPlantEvents commands, IMapper mapper)
        {
            _queries = queries;
            _commands = commands;
            _mapper = mapper;
        }

        // GET api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantEventsIndexViewModel>>> Get([FromRoute] Guid plantId)
        {
            IEnumerable<Event> events = await _queries.GetByPlant(plantId);
            if (events is null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<PlantEventsIndexViewModel>>(events));
        }

        // GET api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/events/DC117408-630E-459B-B16F-DE36EBC58E8F
        [HttpGet("{id:guid}", Name = nameof(GetEvent))]
        public async Task<ActionResult<PlantEventDetailsViewModel>> GetEvent([FromRoute] Guid plantId, Guid id)
        {
            Event Event = await _queries.Get(plantId, id);
            if (Event is null)
                return NotFound();

            return Ok(_mapper.Map<PlantEventDetailsViewModel>(Event));
        }

        // POST api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/events
        [HttpPost]
        public async Task<ActionResult> Post([FromRoute] Guid plantId, [FromBody] CreatePlantEventViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                CreatePlantEventModel createEventModel = _mapper.Map<CreatePlantEventModel>(model);
                Guid eventId = await _commands.Create(plantId, createEventModel);
                return CreatedAtRoute(nameof(GetEvent), new { plantId = plantId, id = eventId }, null);
            }
            catch (PlantDoesNotExistException ex)
            {
                return Conflict(new
                {
                    Error = ex.Message
                });
            }
            catch (EventOccurenceDateBeforePlantDateOrInTheFutureException ex)
            {
                return Conflict(new
                {
                    Error = ex.Message
                });
            }
        }

        // PUT api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/events/DC117408-630E-459B-B16F-DE36EBC58E8F
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put([FromRoute] Guid plantId, Guid id, [FromBody] UpdatePlantEventViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                UpdatePlantEventModel updateEventModel = _mapper.Map<UpdatePlantEventModel>(model);
                updateEventModel.Id = id;

                Event Event = await _commands.Update(plantId, updateEventModel);
                if (Event is null)
                    return NotFound();

                return Ok(_mapper.Map<PlantEventDetailsViewModel>(Event));
            }
            catch (PlantDoesNotExistException ex)
            {
                return Conflict(new
                {
                    Error = ex.Message
                });
            }
            catch (EventDoesNotExistException ex)
            {
                return Conflict(new
                {
                    Error = ex.Message
                });
            }
            catch (EventOccurenceDateBeforePlantDateOrInTheFutureException ex)
            {
                return Conflict(new
                {
                    Error = ex.Message
                });
            }
        }

        // DELETE api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/events/DC117408-630E-459B-B16F-DE36EBC58E8F
        [HttpDelete("{id:guid}")]
        public async Task<NoContentResult> Delete([FromRoute]Guid plantId, Guid id)
        {
            await _commands.Delete(plantId, id);
            return NoContent();
        }

        // GET api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/events/sum
        [HttpGet("sum")]
        public async Task<ActionResult<IEnumerable<OccuredPlantEventsSummaryModel>>> GetSummary([FromRoute] Guid plantId)
        {
            try
            {
                IEnumerable<OccuredPlantEventsSummaryModel> Event = await _queries.Sum(plantId);

                return Ok(_mapper.Map<IEnumerable<OccuredPlantEventsSummaryViewModel>>(Event));
            }
            catch (PlantDoesNotExistException)
            {
                return NotFound();
            }
        }
    }
}