using Microsoft.AspNetCore.Mvc.RazorPages;

   using System.Collections.Generic;

   using System.Data.SqlClient;

 

   public class CustomersModel : PageModel

   {

       public List<Customer> Customers { get; set; }

 

       public void OnGet()

       {
            // Create and populate a list of customers with data from the database
           Customers = new List<Customer>();

        // Make a connection to the database
        string connectionString = "Server=localhost;Database=Northwind;User Id=sa;Password=P@ssw0rd; ; TrustServerCertificate=True;";

          // Connect to the database

           using (SqlConnection connection = new SqlConnection(connectionString))

           {

            connection.Open();

                //Create a SQL statement to get the customers records

               string sql = "SELECT CustomerID, CompanyName, ContactName, Country FROM Customers";
                // Execute the SQL command
               using (SqlCommand command = new SqlCommand(sql, connection))

               {
                    // use a data reader to read the resultset (the records) retrieved from the database
                   using (SqlDataReader reader = command.ExecuteReader())

                {
                        // Loop through all of the records in the resultset
                        // and add each customer record to my customer list
                       while (reader.Read())

                       {

                        Customers.Add(new Customer

                        {

                            CustomerID = reader.GetString(0),

                            CompanyName = reader.GetString(1),

                            ContactName = reader.GetString(2),

                            Country = reader.GetString(3)

                        });

                       }

                   }

               }

           }

       }

   }

 

   public class Customer

   {
        // Atributes (properties of a customer)
       public string CustomerID { get; set; }

       public string CompanyName { get; set; }

       public string ContactName { get; set; }

       public string Country { get; set; }

   }