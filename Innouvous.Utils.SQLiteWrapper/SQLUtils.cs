using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.Data
{
    public class SQLUtils
    {
        private static Dictionary<string, string> sqlStatementDictionary = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scriptsPath"></param>
        /// <param name="fileName"></param>
        /// <param name="ext">Extension without .</param>
        /// <param name="args"></param>
        /// <returns></returns>
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

        private static string SQLEncode(string arg)
        {
            arg = arg.Replace("'", "''");

            return arg;
        }

        public static int GetLastInsertRow(SQLiteClient client)
        {
            string command = "select last_insert_rowid()";

            var result = client.ExecuteScalar(command);

            return Int32.Parse(result.ToString());
        }

        private const string DateTimeFormat = "yyyy-MM-dd HH:mm";
        public static string ToSQLDateTime(DateTime dateTime)
        {
            return dateTime.ToString(DateTimeFormat);
        }

        public static DateTime ToDateTime(string dateTimeString)
        {
            return DateTime.ParseExact(dateTimeString, DateTimeFormat, CultureInfo.InvariantCulture);
        }


    }
}
