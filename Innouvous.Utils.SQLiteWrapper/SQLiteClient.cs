using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.Data
{
    public class SQLiteClient
    {
        private string filename;

        private SQLiteConnection connection;
        //private SQLiteCommand cmd;

        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();

        public SQLiteClient(string filename, bool isNew, Dictionary<string, string> customArgs = null)
        {
            if (customArgs == null)
                customArgs = new Dictionary<string, string>();

            string connectionString = CreateConnectionString(filename, isNew, customArgs);

            connection = new SQLiteConnection(connectionString);
            
            connection.Open();

            this.filename = filename;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql">INSERT, UPDATE, DELETE, CREATE TABLE</param>
        public void ExecuteNonQuery(string sql)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            cmd.ExecuteNonQuery();
        }

        public object ExecuteScalar(string sql)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            return cmd.ExecuteScalar();
        }

        public DataTable ExecuteSelect(string sql)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = sql;

            SQLiteDataAdapter da = new SQLiteDataAdapter(sql, connection);

            ds.Reset();

            da.Fill(ds);

            dt = ds.Tables[0];

            return dt;
        }

        public void Close()
        {
            connection.Close();
        }

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
