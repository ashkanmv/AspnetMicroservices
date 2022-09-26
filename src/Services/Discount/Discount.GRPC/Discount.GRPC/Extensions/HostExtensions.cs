using Npgsql;

namespace Discount.GRPC.Extensions
{
    public static class HostExtensions
    {
        public static void MigrateDatabase<TContext>(this IServiceCollection service, int? retry = 0)
        {
            var retryCount = retry.Value;

                //using var scope = host.Services.CreateScope();

                var services = service.BuildServiceProvider();
                var configuration = services.GetRequiredService<IConfiguration>();
                //var logger = services.GetRequiredService<ILogger>();

            try
            {
                //logger.LogInformation("Migrating postresql database.");

                using var connection =
                    new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionStrings"));
                connection.Open();

                using var command = new NpgsqlCommand() { Connection = connection };

                command.CommandText = "DROP TABLE IF EXISTS Coupon";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY,
                                                        ProductName VARCHAR(24) NOT NULL,
                                                        Description TEXT,
                                                        Amount INT)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X','IPhone X Discount', 150);";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10','Samsong 10 Discount', 100);";
                command.ExecuteNonQuery();

                //logger.LogInformation("Migrated postresql database.");

                
            }
            catch (NpgsqlException e)
            {
                //logger.LogError(e,"An Error occurred while migrating the postresql database.");
                if (retryCount < 50)
                {
                    retryCount++;
                    Thread.Sleep(2000);
                    MigrateDatabase<TContext>(service, retryCount);
                }
            };
        }
    }
}
