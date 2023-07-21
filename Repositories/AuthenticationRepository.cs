using Dapper;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using System.Data.SqlClient;

namespace Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly string connectionString;

        public AuthenticationRepository()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            connectionString = configuration.GetConnectionString("sqlConnection");
            CheckUserTableExists();
        }

        public bool Register(User user)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var query = "INSERT INTO Users (username, password, Email, PhoneNumber, FirstName, LastName) " +
                        "VALUES (@username, @password, @email, @phoneNumber, @firstName, @lastName)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", user.UserName);
                        command.Parameters.AddWithValue("@password", user.Password);
                        command.Parameters.AddWithValue("@email", user.Email);
                        command.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);
                        command.Parameters.AddWithValue("@firstName", user.FirstName);
                        command.Parameters.AddWithValue("@lastName", user.LastName);

                        int rowsEffected = command.ExecuteNonQuery();

                        if (rowsEffected > 0)
                        {
                            Console.WriteLine("Veri Eklendi");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Veri ekleme başarısız oldu");
                        }
                    }

                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                if(ex.Number == 2627)
                {
                    Console.WriteLine($"Username eşsiz olmalıdır.");
                    throw new NotUniqueUsernameBadRequestException();
                }
            }
            return false;
        }

        public bool Login(User user)
        {
            var _user = FindByName(user.UserName);

            var result = !(_user == null || user.Password != _user.Password);

            if (!result)
            {
                Console.WriteLine("Doğrulama başarısız oldu. Kullanıcı yok yada şifre yanlış");
            }
            return result;
        }

        private User? FindByName(string userName)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM Users WHERE username=@username;";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", userName);
                    var reader = command.ExecuteReader();
                    
                    var parser = reader.GetRowParser<User>(typeof(User));
                    
                    reader.Read();

                    if (!reader.HasRows)
                    {
                        connection.Close();
                        return null;
                    }

                    var user = parser(reader);

                    connection.Close();
                    return user;
                }
            }
        }

        private void CheckUserTableExists()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var queryForCreateTable = "IF OBJECT_ID(N'dbo.Users', N'U') IS NULL CREATE TABLE Users " +
                    "(id INT IDENTITY(1,1) PRIMARY KEY, userName VARCHAR(50) UNIQUE, password VARCHAR(50), " +
                    "email VARCHAR(50), phoneNumber VARCHAR(50), firstName VARCHAR(50), lastName VARCHAR(50));";

                using (var command = new SqlCommand(queryForCreateTable, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Tablo kontrol edildi");
                }


                connection.Close();
            }
        }
    }
}
