using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils
{
    public class DialogsUtility
    {
        private static void CheckValues(string name, string extension)
        {
            if (String.IsNullOrEmpty(name) || string.IsNullOrEmpty(extension))
                throw new Exception("Name or extension is empty");
            else if (!extension.Contains('.'))
                throw new Exception("Extension should be in the form *.ext");
        }
        public static void AddExtension(SaveFileDialog dialog, string name, string extension)
        {
            dialog.Filter = AddExtension(dialog.Filter, name, extension);
        }

        public static System.Windows.Forms.FolderBrowserDialog CreateFolderBrowser()
        {
            return new System.Windows.Forms.FolderBrowserDialog();
        }

        private static string AddExtension(string existingFilterString, string name, string extension)
        {
            CheckValues(name, extension);

            extension = extension.ToLower();

            var values = GetFiltersFromString(existingFilterString);

            if (values.ContainsKey(extension))
                return existingFilterString;
            else
            {
                values.Add(extension, name);

                return ToFilter(values);
            }
        }

        public static void AddExtension(OpenFileDialog dialog, string name, string extension)
        {
            dialog.Filter = AddExtension(dialog.Filter, name, extension);
            
        }

        private const char FilterDelim = '|';

        private static string ToFilter(Dictionary<string, string> values)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var kv in values)
            {
                sb.Append(kv.Value + FilterDelim + kv.Key + FilterDelim);
            }

            return sb.ToString().TrimEnd(FilterDelim);
        }

        /// <summary>
        /// Breaks the filters string into extension, name dictionary
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        private static Dictionary<string,string> GetFiltersFromString(string filterString)
        {
            //extension, name
            Dictionary<string, string> map = new Dictionary<string,string>();

            string[] parts = filterString.Split(new char[]{'|'}, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < parts.Length; i++ )
            {
                string name = parts[i];
                string ext = parts[++i];

                map.Add(ext, name);
            }

                return map;
        }

        public static SaveFileDialog CreateSaveFileDialog(string title = "Save File", string initialDirectory = null, 
            bool addExtension = true, bool overwritePrompt= true, bool validateNames = true)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = title;
            sfd.AddExtension = addExtension;
            sfd.InitialDirectory = initialDirectory;
            sfd.OverwritePrompt = overwritePrompt;
            sfd.ValidateNames = validateNames;

            return sfd;
        }

        public static OpenFileDialog CreateOpenFileDialog(string title = "Open File", string initialDirectory = null,
            bool addExtension = true, bool checkFileExists = true, bool followLinks = true, bool multiselect=false, bool validateNames = true)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = title;
            ofd.AddExtension = addExtension;
            ofd.InitialDirectory = initialDirectory;
            ofd.Multiselect = multiselect;
            ofd.ValidateNames = validateNames;
            ofd.CheckFileExists = checkFileExists;
            ofd.DereferenceLinks = followLinks;
            
            return ofd;
        }
    }
}
