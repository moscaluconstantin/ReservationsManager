using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationsManager.BLL.Interfaces;
using ReservationsManager.Common;
using ReservationsManager.DAL.Interfaces;
using Action = ReservationsManager.Domain.Models.Action;

namespace ReservationsManager.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ActionsController : ControllerBase
    {
        private readonly IActionsRepository _repository;
        private readonly IActionEmployeesService _actionEmployeesService;

        public ActionsController(IActionsRepository repository, IActionEmployeesService actionEmployeesService)
        {
            _repository = repository;
            _actionEmployeesService = actionEmployeesService;
        }

        [HttpGet("Assigned")]
        public async Task<IActionResult> GetAllAssignedActions()
        {
            var actions = await _actionEmployeesService.GetActionsAsync();
            return Ok(actions);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var actions = await _repository.GetAllAsync();
            return Ok(actions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 0)
                return BadRequest(ActionControllerLogMessages.NegetiveId);

            var action = await _repository.GetByIdAsync(id);

            if (action == null)
                return NotFound(ActionControllerLogMessages.InexistentAction);

            return Ok(action);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string actionName)
        {
            if (string.IsNullOrEmpty(actionName))
                return BadRequest(ActionControllerLogMessages.EmptyName);

            await _repository.AddAsync(new Action { Name = actionName });
            await _repository.SaveAsync();

            return Ok(ActionControllerLogMessages.Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string actionName)
        {
            if (id < 0)
                return BadRequest(ActionControllerLogMessages.NegetiveId);

            if (string.IsNullOrEmpty(actionName))
                return BadRequest(ActionControllerLogMessages.EmptyName);

            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
                return BadRequest(ActionControllerLogMessages.InexistentAction);

            existing.Name = actionName;
            _repository.Update(existing);
            await _repository.SaveAsync();

            return Ok(ActionControllerLogMessages.Updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
                return BadRequest(ActionControllerLogMessages.NegetiveId);

            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
                return BadRequest(ActionControllerLogMessages.InexistentAction);

            _repository.Remove(existing);
            await _repository.SaveAsync();

            return Ok(ActionControllerLogMessages.Removed);
        }
    }
}
