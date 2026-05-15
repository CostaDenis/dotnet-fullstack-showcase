using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Showcase.Api.Services.Abstractions;
using Showcase.Core.DTOs.Employee;

namespace Showcase.Api.Controllers;

[ApiController]
[Authorize]
[Route("employees")]
public class EmployeeController(IEmployeeService EmployeeService)
    : ControllerBase
{

    [HttpGet("{employeeId:guid}")]
    [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmployeeResponse>> GetById(
        [FromRoute] Guid employeeId,
        CancellationToken cancellationToken = default
    )
    {
        var employee = await EmployeeService.GetByIdAsync(employeeId, cancellationToken);

        if (employee is null)
            return NotFound();

        return Ok(employee);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmployeeResponse>> Create(
        [FromBody] EmployeeCreateRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var employee = await EmployeeService.CreateAsync(request, cancellationToken);

        if (employee is null)
            return BadRequest("Erro ao criar usuário!");

        return CreatedAtRoute(
            new { employee.Id }, employee
        );
    }

    [HttpPut("{employeeId:guid}")]
    [ProducesResponseType(typeof(EmployeeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmployeeResponse>> Update(
        [FromRoute] Guid employeeId,
        [FromBody] EmployeeUpdateRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var exists = await EmployeeService.GetByIdAsync(employeeId, cancellationToken);

        if (exists is null)
            return NotFound();

        var updatedEmployee = await EmployeeService.UpdateAsync(employeeId, request, cancellationToken);

        return Ok(updatedEmployee);
    }

}