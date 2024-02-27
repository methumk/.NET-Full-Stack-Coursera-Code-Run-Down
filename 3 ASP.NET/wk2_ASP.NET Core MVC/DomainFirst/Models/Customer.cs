namespace ORMApp.Models
{
    /* 
    Examples: ORM
        - Step 1
        - This is code first approach (create model first -> migrate to DB -> manage with EF Core)
        - This is our customer which has fields that will show up in our DB table
     */
    public class Customer
    {
        // NOTE: field with <modelName>Id or Id will become primary key
        // NOTE: Every entity should have Primary key column
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public double CustomerAmount { get; set; }
    }
}
