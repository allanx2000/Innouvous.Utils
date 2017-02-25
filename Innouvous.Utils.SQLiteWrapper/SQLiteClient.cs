using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.Data
{
    /// <summary>
    /// Encapsulates Data.SQLite commands and provides the familiar SQL interface used by other .NET SQLClients
    /// 
    /// </summary>
    public class SQLiteClient
    {
        private string filename;

        private SQLiteConnection connection;

        
        /// <summary>
        /// Create a SQLiteClient
        /// </summary>
        /// <param name="filename">Database file</param>
        /// <param name="isNew">Whether it exists or not</param>
        /// <param name="customArgs">Other SQLite-specific parameters</param>
        public SQLiteClient(string filename, bool isNew, Dictionary<string, string> customArgs = null)
        {
            if (customArgs == null)
                customArgs = new Dictionary<string, string>();

            string connectionString = CreateConnectionString(filename, isNew, customArgs);

            connection = new SQLiteConnection(connectionString);
            
            connection.Open();

            this.filename = filename;
        }

        public SQLiteConnection GetConnection()
        {
            return connection;
        }

        /// <summary>
        /// Executes INSERT, UPDATE, DELETE, CREATE TABLE commands
        /// </summary>
        /// <param name="sql">INSERT, UPDATE, DELETE, CREATE TABLE SQL query</param>
        public void ExecuteNonQuery(string sql)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Executes SELECT commands that return 1 value
        /// </summary>
        /// <param name="sql">SELECT string for 1 value</param>
        /// <returns>The value</returns>
        public object ExecuteScalar(string sql)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            return cmd.ExecuteScalar();
        }

        /// <summary>
        /// Executes a SELECT query and returns a DataTable
        /// </summary>
        /// <param name="sql">SELECT SQL string</param>
        /// <returns>Results in a DataTable</returns>
        public DataTable ExecuteSelect(string sql)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            SQLiteDataAdapter da = new SQLiteDataAdapter(sql, connection);

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.Reset();

            da.Fill(ds);

            dt = ds.Tables[0];

            return dt;
        }

        /// <summary>
        /// Close the file
        /// </summary>
        public void Close()
        {
            connection.Close();
        }

        /// <summary>
        /// Wraps the SQLite-specific connection string logic
        /// </summary>
        /// <param name="filename">Database file</param>
        /// <param name="isNew">Whether it exists or not</param>
        /// <param name="customArgs">Other SQLite-specific parameters</param>
        /// <returns>A connection string</returns>
        private string CreateConnectionString(string filename, bool isNew, Dictionary<string, string> customArgs)
        {
            Dictionary<string, string> allArgs = new Dictionary<string, string>(customArgs);

            allArgs["New"] = isNew.ToString();
            allArgs["Data Source"] = filename;

            if (!allArgs.ContainsKey("Version"))
                allArgs["Version"] = "3";

            if (!allArgs.ContainsKey("Compress"))
                allArgs["Compress"] = "True";

            return String.Join(";", from kv in allArgs select kv.Key + "=" + kv.Value);

        }
    }
}
