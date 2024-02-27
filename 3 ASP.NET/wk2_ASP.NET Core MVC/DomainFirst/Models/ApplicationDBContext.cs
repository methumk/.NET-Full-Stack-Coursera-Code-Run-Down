using Microsoft.EntityFrameworkCore;

namespace ORMApp.Models
{
/*
Examples: ORM
    - intellisense comes from Entity framework core nuget packages
    - We want to access (and scaffold) the customer model from the DB so we have to add that as a variable
 */
    public class ApplicationDBContext : DbContext
    {
        // Make sure to pluralize the db from the model (i.e s and ies)
        // Allows communication with the table from the DB through this attribute
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Override the default Onconfigure file from DbContext
            // This should read from the optionsBuilder

            // We are using the SQLServer db
            // Then we provide connection string
            // Database is your database name you want to create/add to?
            // Server name can be . to access your local DB server or you can get the name from the SQL Server management studio
            optionsBuilder.UseSqlServer("Server=DESKTOP-AJ3RPD9;Database=MyDB;Integrated Security=true;TrustServerCertificate=true;");
            // base.OnConfiguring(optionsBuilder);
        }

    }
}
