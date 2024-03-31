using System.Data.SqlClient;

namespace DB
{
    public static class Database
    {
        public static SqlConnection Connection { get; private set; }

        public static void Open(string connectionString)
        {
            try
            {
                Connection = new SqlConnection(connectionString);
                Connection.Open();
            }
            catch (System.Exception)
            {
                throw new System.Exception();
            }
        }
    }
}
