# Metrans Homework 2 (C#/ASP.NET Core/REST API)

Create a REST API using ASP.NET Core by following these steps:

## Task request

### 1. Solution Components
In your solution, use the following components:
- `Microsoft.EntityFrameworkCore.InMemory`
- Swagger UI (`Swashbuckle.Core`)

### 2. Project Table Addition
Add an `Employees` table to the project with the following columns:
- `id` (Int32) - primary key
- `name` (String[20])
- `surname` (String[20])
- `dateOfBirth` (DateTime)
- `employedFrom` (DateTime)
- `employedTo` (DateTime) - default value is 31.12.2099 if not filled

### 3. Implement Validations in Models
Implement validations for:
- `name` and `surname` - maximum string length is 20 characters
- `employedFrom` date must be before `employedTo` date

### 4. CRUD Operations Over the Table
Create CRUD operations (create a new record, read, edit, delete a record):
- `GET Employees/{id}`
- `POST Employees/Create`
- `PUT Employees/Update`
- `DELETE Employees/Delete`
- `GET Employees` - returns a list of employees sorted by the parameters `sortBy` and `sortType`. By default, the list is sorted by the employee's ID. Implement sorting options by date of birth or surname as well.
Example request: `Employees?sortBy=dateOfBirth&sortType=descending`

### 5. Documentation
Document the code (functions, parameters, variables).

## Solution details

The solution was implemented according to initial task information provided.

### 1. Database seeding

On top of the requested operations, you can use database seeding feature. Purpose of this feature is make testing of this solution easier.

- `POST Employees/SeedDatabase`

This will create 20 employee records with random data in the database. This endpoint will display an error if any records are present in database.

### 2. Validation implementation

There are multiple possible approaches to implement validation. As the task request specified to do validation in model, I've used DataValidation attributes.

The basic data validation attributes do not support date comparison. Thus, I added the DateMustBeEarlierAttribute.cs class that adds support of date comparison in model definition. Usage:

- `[DateMustBeEarlier("EmployedTo", ErrorMessage = "EmployedFrom must be earlier than EmployedTo")]`

On top of the request to limit max number of characters for name and surname in database model, I added min length for Name and Surname.

### 3. CRUD operations

You can find documentation, example requests, possible responses when you run the solution on `/swagger` URL.

### 4. Code documentation and structure

You can find detailed comments in each class, for each method and most of variables. Code is structured to

- Controllers
- Models
- Validations

In root directory, you can find:

- ApplicationDbContext.cs
- SeedDatabase.cs

and other files mostly generated by Visual Studio.

## More information

For any inquiries about the solution please write to `info@jandrozd.eu`.