using Microsoft.EntityFrameworkCore;
using Showcase.Api.Data;
using Showcase.Api.Models;
using Showcase.Api.Repositories.Abstractions;

namespace Showcase.Api.Repositories;

public class EmployeeRepository(AppDbContext Context) : IEmployeeRepository
{

    public async Task<Employee?> GetByIdAsync(Guid employeeId, CancellationToken cancellationToken = default)
        => await Context.Employees
            .FirstOrDefaultAsync(x => x.Id == employeeId, cancellationToken);

    public async Task<Employee?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        => await Context.Employees
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

    public async Task<Employee> CreateAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        await Context.Employees.AddAsync(employee, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);

        return employee;
    }

    public async Task<Employee> UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        Context.Employees.Update(employee);
        await Context.SaveChangesAsync(cancellationToken);

        return employee;
    }
}