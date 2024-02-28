using MetransHomework2.Models;

namespace MetransHomework2
{
    /// <summary>
    /// Class that allows to populate random employees into the database for testing purposes
    /// </summary>
    public class SeedDatabase
    {
        /// <summary>
        /// Runs the database seeding operation within current database context
        /// </summary>
        /// <param name="context">Current database context</param>
        internal static void DoSeedDatabase(ApplicationDbContext context)
        {
            if (!context.Employees.Any())
            {
                var random = new Random();
                var firstNames = new List<string> { "James", "Mary", "John", "Patricia", "Robert", "Jennifer", "Michael", "Linda", "William", "Elizabeth" };
                var lastNames = new List<string> { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez" };

                for (int i = 1; i <= 20; i++)
                {
                    var firstName = firstNames[random.Next(firstNames.Count)];
                    var lastName = lastNames[random.Next(lastNames.Count)];
                    var dateOfBirth = GenerateRandomDateOfBirth(random);
                    var employedFrom = GenerateRandomEmployedFromDate(random, dateOfBirth);

                    context.Employees.Add(new Employee
                    {
                        Id = i,
                        Name = firstName,
                        Surname = lastName,
                        DateOfBirth = dateOfBirth,
                        EmployedFrom = employedFrom,
                        EmployedTo = GenerateRandomEmployedToDate(random, employedFrom) // Default or random value
                    });
                }
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Generates random date of birth
        /// </summary>
        /// <param name="random">Random class instance</param>
        /// <returns></returns>
        private static DateTime GenerateRandomDateOfBirth(Random random)
        {
            var start = new DateTime(1950, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }

        /// <summary>
        /// Generates random employed from date. Ensures that result date is at least 18 years after the birth date.
        /// </summary>
        /// <param name="random">Random class instance</param>
        /// <param name="dateOfBirth">Date of birth of employee</param>
        /// <returns></returns>
        private static DateTime GenerateRandomEmployedFromDate(Random random, DateTime dateOfBirth)
        {
            var start = dateOfBirth.AddYears(18); // Assuming employment starts at least at 18 years old
            return start.AddDays(random.Next(3650));
        }

        /// <summary>
        /// Generates random end date of employment. Ensures that result date is after the employed from date.
        /// </summary>
        /// <param name="random">Random class instance</param>
        /// <param name="employedFrom">Employed from date</param>
        /// <returns></returns>
        private static DateTime GenerateRandomEmployedToDate(Random random, DateTime employedFrom)
        {
            var end = new DateTime(2099, 12, 31);
            int range = (end - employedFrom).Days;
            return employedFrom.AddDays(random.Next(range));
        }
    }
}
