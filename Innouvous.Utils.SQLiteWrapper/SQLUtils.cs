using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.Data
{
    /// <summary>
    /// Utility class with SQL helper functions
    /// </summary>
    public class SQLUtils
    {
        //Cache SQL commands in memory
        private static Dictionary<string, string> sqlStatementDictionary = new Dictionary<string, string>();

        /// <summary>
        /// Loads a query from a SQL template file from the file system
        /// </summary>
        /// <param name="scriptsPath">Template files directory</param>
        /// <param name="fileName">Template file name</param>
        /// <param name="ext">Extension without .</param>
        /// <param name="args">Values for each of the placeholders in the template</param>
        /// <returns>A SQL query string</returns>
        public static string LoadCommandFromText(string scriptsPath, string fileName, string ext, params object[] args)
        {
            if (!sqlStatementDictionary.ContainsKey(fileName))
            {
                string file = Path.Combine(scriptsPath, fileName + "." + ext);

                using (StreamReader sr = new StreamReader(file))
                {
                    sqlStatementDictionary.Add(fileName, sr.ReadToEnd());
                }
            }

            string command = sqlStatementDictionary[fileName];

            for (int i = 0; i < args.Length; i++)
            {
                args[i] = SQLEncode(args[i].ToString());
            }

            return String.Format(command, args);
        }

        public static bool CheckTableExists(string table, SQLiteClient client)
        {
            try
            {
                string cmd = "select * from {0} limit 1";
                cmd = string.Format(cmd, table);
                client.ExecuteSelect(cmd);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public static bool IsNull(object item)
        {
            return item == null || item is DBNull;
        }

        public static string SQLEncode(string arg, bool returnNULL = false, bool quote = false)
        {   
            if (arg == null)
            {
                if (returnNULL)
                    return "NULL";
                else
                    return null;
            }

            arg = arg.Replace("'", "''");

            if (quote)
                arg = "'" + arg + "'";

            return arg;
        }

        /// <summary>
        /// Get the last insert row ID
        /// </summary>
        /// <param name="client"></param>
        /// <returns>Returns the ID or 0 if last query was not an insert</returns>
        public static int GetLastInsertRow(SQLiteClient client)
        {
            string command = "select last_insert_rowid()";

            var result = client.ExecuteScalar(command);

            return Int32.Parse(result.ToString());
        }

        private const string DateTimeFormat = "yyyy-MM-dd HH:mm";
        
        /// <summary>
        /// Converts dates to SQLite format
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns>Date string for use in query construction</returns>
        public static string ToSQLDateTime(DateTime dateTime)
        {
            return dateTime.ToString(DateTimeFormat);
        }

        //Doesn't seem to be used as queries return dates as DateTime object
        public static DateTime ToDateTime(string dateTimeString)
        {
            return DateTime.ParseExact(dateTimeString, DateTimeFormat, CultureInfo.InvariantCulture);
        }

        public static bool SafeBool(object v, bool nullValue = false)
        {
            if (v is bool)
                return (bool)v;
            else
                return nullValue;
        }
    }
}
