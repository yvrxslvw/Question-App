using System.Data.SqlClient;
using System.Linq;

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

        public static int Insert(string table, string columns, string values)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO @table (@columns) VALUES (@values);", Connection);
            cmd.Parameters.AddWithValue("table", table);
            cmd.Parameters.AddWithValue("columns", columns);
            cmd.Parameters.AddWithValue("values", values);

            var result = cmd.ExecuteScalar();

            return 71061; // TO CHANGE
        }

        public static void Delete(string table, string whereColumn, string whereValue)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM @table WHERE @column = '@value';", Connection);
            cmd.Parameters.AddWithValue("table", table);
            cmd.Parameters.AddWithValue("column", whereColumn);
            cmd.Parameters.AddWithValue("value", whereValue);

            cmd.ExecuteNonQuery();
        }

        public static void Update(string table, string[] columns, string[] values, int id)
        {
            string setters = MergeSetterArgs(columns, values);

            SqlCommand cmd = new SqlCommand("UPDATE @table SET @setters WHERE Id = '@id';", Connection);
            cmd.Parameters.AddWithValue("table", table);
            cmd.Parameters.AddWithValue("setters", setters);
            cmd.Parameters.AddWithValue("id", id);

            cmd.ExecuteNonQuery();
        }

        private static string MergeSetterArgs(string[] firstArgs, string[] secondArgs)
        {
            if (firstArgs.Length != secondArgs.Length) throw new System.Exception("Number of args is not equals.");
            int argsLen = firstArgs.Length;
            string result = $"{firstArgs[0]} = '{secondArgs[0]}'";
            if (argsLen > 1)
            {
                for (int i = 1; i < argsLen; i++)
                {
                    result += $", {firstArgs[i]} = '{secondArgs[i]}'";
                }
            }
            return result;
        }
    }
}
