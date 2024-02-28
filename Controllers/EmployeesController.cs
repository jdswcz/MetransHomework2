using MetransHomework2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MetransHomework2.Controllers
{

    /// <summary>
    /// Holds methods for the REST API that works with employee records
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Controller's constructor. Requires database context (ApplicationDBContext) to be submitted.
        /// </summary>
        /// <param name="context">Current database context</param>
        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// In case the database is empty, this method creates 20 employees with random names and other properties.
        /// </summary>
        /// <response code="200">Database seeded successfully.</response>
        /// <response code="400">Database already contains records.</response>
        /// <returns></returns>
        [HttpPost("SeedDatabase")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult SeedDatabase()
        {
            if (_context.Employees.Any())
            {
                return BadRequest("The database has already been seeded.");
            }

            MetransHomework2.SeedDatabase.DoSeedDatabase(_context);

            return Ok("Database seeded successfully.");
        }

        /// <summary>
        /// Returns specified employee using the submitted ID.
        /// </summary>
        /// <param name="id">ID of employee</param>
        /// <response code="200">Employee found.</response>
        /// <response code="404">Employee with specified ID was not found.</response>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        /// <summary>
        /// Returns list employees sorted by ID, DateOfBirth or Surname column.
        /// </summary>
        /// <param name="sortBy">Specifies column to be used for sorting. Can be 'DateOfBirth" or 'Surname'. Any other string results in sorting by ID. Parameter is case insensitive.</param>
        /// <param name="sortType">Specifies the sorting order. Possible values: asc or desc. Any other value dwefaults to ascending order.</param>
        /// <response code="200">Employees listed successfully.</response>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees([FromQuery] string sortBy = "id", [FromQuery] string sortType = "asc")
        {
            IQueryable<Employee> query = _context.Employees;

            switch (sortBy.ToLower())
            {
                case "dateofbirth":
                    if (sortType.ToLower() == "desc")
                        query = query.OrderByDescending(e => e.DateOfBirth);               
                    else
                        query = query.OrderBy(e => e.DateOfBirth);
                    break;
                case "surname":
                    if (sortType.ToLower() == "desc")
                        query = query.OrderByDescending(e => e.Surname);
                    else
                        query = query.OrderBy(e => e.Surname);
                    break;
                default:
                    if (sortType.ToLower() == "desc")
                        query = query.OrderByDescending(e => e.Id);              
                    else
                        query = query.OrderBy(e => e.Id);
                    break;
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Creates new employee record in the database.
        /// </summary>
        /// <param name="employee">Employee to be created</param>
        /// <response code="201">Employee created successfully.</response>
        /// <response code="400">Validation failed.</response>
        /// <returns></returns>
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        /// <summary>
        /// Updates existing employee record in database
        /// </summary>
        /// <param name="id">ID of employee</param>
        /// <param name="employee">JSON object with employee information</param>
        /// <response code="204">Employee updated successfully.</response>
        /// <response code="400">Validation failed.</response>
        /// <response code="404">Employee not found using the submitted ID.</response>
        /// <returns></returns>
        [HttpPut("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest("Employee ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Employees.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes employee with specified ID from the database.
        /// </summary>
        /// <param name="id">ID of employee</param>
        /// <response code="204">Employee deleted successfully.</response>
        /// <response code="404">Employee not found.</response>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent(); // Standard response for a successful DELETE request
        }

    }
}