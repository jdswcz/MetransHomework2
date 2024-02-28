using System.ComponentModel.DataAnnotations;
using MetransHomework2.Validations;

namespace MetransHomework2.Models
{
    /// <summary>
    /// Employee model defining properties of an employee
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of employee. Must be between 3 and 20 characters.
        /// </summary>
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 20 characters")]
        public string? Name { get; set; }

        /// <summary>
        /// Surname of employee. Must be between 3 and 20 characters.
        /// </summary>
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Surname must be between 3 and 20 characters")]
        public string? Surname { get; set; }

        /// <summary>
        /// Birth date of employee. Must be earlier than EmployedFrom.
        /// </summary>
        [DateMustBeEarlier("EmployedFrom", ErrorMessage = "DateOfBirth must be earlier than EmployedFrom")]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Date on which was person employed. Must be earlier than EmployedTo
        /// </summary>
        [DateMustBeEarlier("EmployedTo", ErrorMessage = "EmployedFrom must be earlier than EmployedTo")]
        public DateTime EmployedFrom { get; set; }

        /// <summary>
        /// End date of person's employment. Defaults to 2099/12/31
        /// </summary>
        public DateTime EmployedTo { get; set; } = new DateTime(2099, 12, 31); // Setting default value directly
    }
}