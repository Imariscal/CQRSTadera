 
using Microsoft.AspNetCore.Mvc;
using Catalog.API.Execution;
using Catalog.API.Execution.Answers.Contracts;
using Catalog.Application.DTOs.Employee;
using Catalog.Application.Services.Contracts;
using System.Net;

namespace OxxoGas.Catalogos.Retail.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController] 
    public class EmployeeController(IEmployeeService service) : ControllerBase
    {
        [HttpGet] 
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetEmployeeData()
        {
            async Task<IEnumerable< EmployeeDTO>> predicate() => await service.GetEmployeeData();
            var response = await SafeExecutor<IEnumerable<EmployeeDTO>>.ExecAsync(predicate);
            return ProcessResponse(response);
        }

        protected ActionResult ProcessResponse<T>(IAnswerBase<T> response) where T : class
        {
            if (response.Success) return Ok(response);
            else return BadRequest(response);
        }
    }
}
