using MetransHomework2.Models;
using Microsoft.EntityFrameworkCore;

namespace MetransHomework2
{
    /// <summary>
    /// Database context for solution
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Database context constructor
        /// </summary>
        /// <param name="options">An options instance containing the configuration data needed to initialize the context.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// DbSet providing access to the employees table in the database
        /// </summary>
        public DbSet<Employee> Employees { get; set; }
    }
}
