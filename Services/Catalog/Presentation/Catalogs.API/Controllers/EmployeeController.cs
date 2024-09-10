 
using Microsoft.AspNetCore.Mvc;
using Catalog.API.Execution;
using Catalog.API.Execution.Answers.Contracts;
using Catalog.Application.DTOs.Employee;
using Catalog.Application.Services.Contracts;
using System.Net;

namespace OxxoGas.Catalogos.Retail.API.Areas.Employee
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployeeService service) : ControllerBase
    {
        [HttpGet]
        [Route("GetEmployeeDataList")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetEmployeeData()
        {
            async Task<IEnumerable<EmployeeDTO>> predicate() => await service.GetEmployeeData();
            var response = await SafeExecutor<IEnumerable<EmployeeDTO>>.ExecAsync(predicate);
            return ProcessResponse(response);
        }

        [HttpPost]
        [Route("AddEmployee")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetEmployeeData(EmployeeDTO employee)
        {
            async Task predicate() => await service.PostEmployee(employee);
            var response = await SafeExecutor.ExecAsync(predicate);
            return ProcessResponse(response);
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateEmployee(Guid employeeId, [FromBody] EmployeeDTO employee)
        {
            async Task predicate() => await service.UpdteEmployee(employeeId, employee);
            var response = await SafeExecutor.ExecAsync(predicate);
            return ProcessResponse(response);       
        }

        protected ActionResult ProcessResponse<T>(IAnswerBase<T> response) where T : class
        {
            if (response.Success) return Ok(response);
            else return BadRequest(response);
        }
    }
}
