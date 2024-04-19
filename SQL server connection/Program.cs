using System;
using System.Data.SqlClient;

namespace SQL_server_connection
{
    class Program
    {
        static void Main(string[] args)
        {
            // Corrected connection string
            string connectionString = @"Data Source=DESKTOP-FP6OQSN\SQLEXPRESS;Initial Catalog=Test_DB;Integrated Security=True";

            // SqlConnection is in the System.Data.SqlClient namespace
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                // Open the connection
                connection.Open();

                Console.WriteLine("Connected to SQL Server!");

                // Perform database operations here...

                // Create Employee Table

                /*var sql = "create table Employee(emp_id int IDENTITY(1,1) PRIMARY KEY, name varchar(50), department varchar(50), salary DECIMAL(10, 2))";
                var cmd = new SqlCommand(sql, connection);
                cmd.ExecuteNonQuery();

                sql = "insert into Employee(Name, Department, Salary) values ('Renuka','Development', 50000),('Nicy','HR', 70000),('Shari','Marketing', 30000)";
                new SqlCommand(sql, connection).ExecuteNonQuery();*/

                var query = "select * from Employee";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // Access columns by name
                    string name = reader["Name"].ToString();
                    string department = reader["Department"].ToString();
                    decimal salary = reader.GetDecimal(reader.GetOrdinal("Salary"));

                    Console.WriteLine($"Name: {name}, Department: {department}, Salary: {salary}");

                }
                connection.Close();

                connection.Open();

                var updateQuery = "update Employee set Name = 'Reshma' Where Emp_id = 1";
                new SqlCommand(updateQuery, connection).ExecuteNonQuery();

                // Don't forget to close the connection when done
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                // Always make sure to close the connection, even if an exception occurs
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }
    }
}
