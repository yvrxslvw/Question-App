using System.Data.SqlClient;

namespace DB
{
    public static class Database
    {
        public static SqlConnection Connection { get; private set; }

        public static void Open(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            Connection.Open();
        }

        public static void Insert(string table, string columns, string values)
        {
            SqlCommand cmd = new SqlCommand($"INSERT INTO {table} ({columns}) VALUES ({values});", Connection);

            cmd.ExecuteNonQuery();
        }

        public static void Delete(string table, string whereColumn, string whereValue)
        {
            SqlCommand cmd = new SqlCommand($"DELETE FROM {table} WHERE {whereColumn} = '{whereValue}';", Connection);

            cmd.ExecuteNonQuery();
        }

        public static void Update(string table, string[] columns, string[] values, int id)
        {
            string setters = MergeSetterArgs(columns, values);

            SqlCommand cmd = new SqlCommand($"UPDATE {table} SET {setters} WHERE Id = '{id}';", Connection);

            cmd.ExecuteNonQuery();
        }

        public static SqlDataReader Select(string table)
        {
            SqlCommand cmd = new SqlCommand($"SELECT * FROM {table};", Connection);

            return cmd.ExecuteReader();
        }

        public static SqlDataReader Select(string table, string whereColumn, string whereValue)
        {
            SqlCommand cmd = new SqlCommand($"SELECT * FROM {table} WHERE {whereColumn} = '{whereValue}';", Connection);

            return cmd.ExecuteReader();
        }

        private static string MergeSetterArgs(string[] firstArgs, string[] secondArgs)
        {
            if (firstArgs.Length != secondArgs.Length) throw new System.Exception("Number of args is not equals.");
            int argsLen = firstArgs.Length;
            string result = $"{firstArgs[0]} = N'{secondArgs[0]}'";
            if (argsLen > 1)
            {
                for (int i = 1; i < argsLen; i++)
                {
                    result += $", {firstArgs[i]} = N'{secondArgs[i]}'";
                }
            }
            return result;
        }
    }
}
