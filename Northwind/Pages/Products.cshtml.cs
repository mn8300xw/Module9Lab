using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;
// Model class for the Products page
// Represents a product in the Northwind database
public class ProductsModel : PageModel
{
    // List to hold product records
    public required List<Product> Products { get; set; }
    // Method to handle GET requests
    [Obsolete]
    public void OnGet()
    {
        // Initialize the product list
        Products = new List<Product>();
        string connectionString = "Server=localhost;Database=Northwind;User Id=sa;Password=P@ssw0rd;TrustServerCertificate=True;";
        // Connect to the database
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Open the database connection
            connection.Open();
            // SQL query to retrieve product data along with category names
            string sql = @"SELECT p.ProductName, c.CategoryName, p.UnitPrice
                           FROM Products p
                           JOIN Categories c ON p.CategoryID = c.CategoryID";
            
            // Execute the SQL command
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                // Use a data reader to read the result set
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Loop through the records and add each product to the list
                    while (reader.Read())
                    {
                        // Create a new Product object and populate it with data from the current record
                        Products.Add(new Product
                        {
                            ProductName = reader.GetString(0),
                            CategoryName = reader.GetString(1),
                            UnitPrice = reader.GetDecimal(2)
                        });
                    }
                }
            }
        }
    }
}

// Product class representing a product record
public class Product
{
    // Attributes (properties of a product)
    public required string ProductName { get; set; }
    public required string CategoryName { get; set; }
    public decimal UnitPrice { get; set; }
}