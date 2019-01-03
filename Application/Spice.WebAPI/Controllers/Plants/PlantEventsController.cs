using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spice.Application.Common.Exceptions;
using Spice.Application.Plants.Events.Interfaces;
using Spice.Application.Plants.Events.Models;
using Spice.Application.Plants.Events.Models.Summary;
using Spice.Domain.Plants.Events;
using Spice.ViewModels.Common;
using Spice.ViewModels.Plants.Events;
using Spice.ViewModels.Plants.Events.Summary;
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
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<PlantEventsIndexViewModel>>(events));
        }

        // GET api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/events/DC117408-630E-459B-B16F-DE36EBC58E8F
        [HttpGet("{id:guid}", Name = nameof(GetEvent))]
        public async Task<ActionResult<PlantEventDetailsViewModel>> GetEvent([FromRoute] Guid plantId, Guid id)
        {
            Event @event = await _queries.Get(plantId, id);
            if (@event is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PlantEventDetailsViewModel>(@event));
        }

        // POST api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/events
        [HttpPost]
        public async Task<ActionResult> Post([FromRoute] Guid plantId, [FromBody] CreatePlantEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                CreatePlantEventModel createEventModel = _mapper.Map<CreatePlantEventModel>(model);
                Guid eventId = await _commands.Create(plantId, createEventModel);
                return CreatedAtRoute(nameof(GetEvent), new { plantId = plantId, id = eventId }, null);
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

        // PUT api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/events/DC117408-630E-459B-B16F-DE36EBC58E8F
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put([FromRoute] Guid plantId, Guid id, [FromBody] UpdatePlantEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                UpdatePlantEventModel updateEventModel = _mapper.Map<UpdatePlantEventModel>(model);
                updateEventModel.Id = id;

                Event @event = await _commands.Update(plantId, updateEventModel);
                if (@event is null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<PlantEventDetailsViewModel>(@event));
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

        // DELETE api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/events/DC117408-630E-459B-B16F-DE36EBC58E8F
        [HttpDelete("{id:guid}")]
        public async Task<NoContentResult> Delete([FromRoute] Guid plantId, Guid id)
        {
            await _commands.Delete(plantId, id);
            return NoContent();
        }

        // GET api/plants/F3694C70-AC96-4BBC-9D70-7C1AF728E93F/events/summary?fromDate=2018-12-01T00:00:00&toDate=2018-12-31T23:59:59
        [HttpGet("summary")]
        public async Task<ActionResult<IEnumerable<PlantEventOccurenceSummaryModel>>> GetSummary([FromRoute] Guid plantId,
                                                                                               [FromQuery] DateTime? fromDate = null,
                                                                                               [FromQuery] DateTime? toDate = null)
        {
            IEnumerable<PlantEventOccurenceSummaryModel> events = await _queries.Summary(plantId, fromDate, toDate);
            if (events is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<PlantEventOccurenceSummaryViewModel>>(events));
        }
    }
}