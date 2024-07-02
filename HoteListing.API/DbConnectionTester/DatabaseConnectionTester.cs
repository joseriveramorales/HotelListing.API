namespace HoteListing.API.DbConnectionTester
{



    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using System;

    public interface IDatabaseConnectionTester
    {
        void TestConnection();
    }

    public class DatabaseConnectionTester : IDatabaseConnectionTester
    {
        private readonly IConfiguration _configuration;

        public DatabaseConnectionTester(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void TestConnection()
        {
            var connectionString = _configuration.GetConnectionString("HotelListingDBConnectionString");

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connection successful!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
                throw;
            }
        }
    }

}
