using Microsoft.Data.SqlClient;

namespace Backend.Config
{
    public class DbContext
    {
        public readonly string connectionString =
           "Server = tcp:lichvadbserver.database.windows.net,1433;" +
            "Initial Catalog = lichvaDB;" +
            "Persist Security Info=False;" +
            "User ID = APIuser;" +
            "Password=\"kurwodzialaj3!\";" +
            "MultipleActiveResultSets=False;" +
            "Encrypt=True;" +
            "TrustServerCertificate=False;" +
            "Connection Timeout = 30;";
    public DbContext() 
        {
            try
            {
                using SqlConnection db = new(connectionString);
                Console.WriteLine("\nQuery data exapmle");
                Console.WriteLine("=====================");
                //string sql = "SELECT * FROM users;";
                string sql = "INSERT INTO users VALUES (SYSDATE, 'dupa', '1', 1, 'dupa', 'wadwa', '213', 321, 'topa', 'kupa');";

                using SqlCommand command = new(sql, db);

                db.Open();
                using SqlDataReader reader = command.ExecuteReader();
                while(reader.Read()) 
                {
                    Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
