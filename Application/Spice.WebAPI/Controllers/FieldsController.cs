using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spice.Application.Common.Exceptions;
using Spice.Application.Fields.Interfaces;
using Spice.Application.Fields.Models;
using Spice.Domain;
using Spice.ViewModels.Common;
using Spice.ViewModels.Fields;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        private readonly IQueryFields _queries;
        private readonly ICommandFields _commands;
        private readonly IMapper _mapper;

        public FieldsController(IQueryFields queries, ICommandFields commands, IMapper mapper)
        {
            _queries = queries;
            _commands = commands;
            _mapper = mapper;
        }

        // GET api/fields
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FieldIndexViewModel>>> Get()
        {
            IEnumerable<Field> fields = await _queries.GetAll();
            return Ok(_mapper.Map<IEnumerable<FieldIndexViewModel>>(fields));
        }

        // GET api/fields/F3694C70-AC96-4BBC-9D70-7C1AF728E93F
        [HttpGet("{id:guid}", Name = nameof(GetField))]
        public async Task<ActionResult<FieldDetailsViewModel>> GetField(Guid id)
        {
            Field field = await _queries.Get(id);
            if (field is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FieldDetailsViewModel>(field));
        }

        // POST api/fields
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateFieldViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                CreateFieldModel createFieldModel = _mapper.Map<CreateFieldModel>(model);
                Guid fieldId = await _commands.Create(createFieldModel);
                return CreatedAtRoute(nameof(GetField), new { id = fieldId }, null);
            }
            catch (Exception ex) when (ex is ResourceStateException)
            {
                return Conflict(new ErrorViewModel(ex));
            }
        }

        // PUT api/fields/F3694C70-AC96-4BBC-9D70-7C1AF728E93F
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] UpdateFieldViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                UpdateFieldModel updateFieldModel = _mapper.Map<UpdateFieldModel>(model);
                updateFieldModel.Id = id;

                Field field = await _commands.Update(updateFieldModel);
                if (field is null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<FieldDetailsViewModel>(field));
            }
            catch (Exception ex) when (ex is ResourceStateException)
            {
                return Conflict(new ErrorViewModel(ex));
            }
        }

        // DELETE api/fields/F3694C70-AC96-4BBC-9D70-7C1AF728E93F
        [HttpDelete("{id:guid}")]
        public async Task<NoContentResult> Delete(Guid id)
        {
            await _commands.Delete(id);
            return NoContent();
        }
    }
}