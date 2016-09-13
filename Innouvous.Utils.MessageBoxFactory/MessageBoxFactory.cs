using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Innouvous.Utils
{
    public static class MessageBoxFactory
    {
        
        /// <summary>
        /// Displays an Error MessageBox with a detailed error message
        /// </summary>
        /// <param name="e"></param>
        /// <param name="title"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static MessageBoxResult ShowError(Exception e, string title = "Error", MessageBoxImage icon = MessageBoxImage.Error, bool outputInnerExceptions = true, Window owner = null)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(e.Message);

            if (outputInnerExceptions)
            {
                int lvl = 0;
                Exception inner = e.InnerException;
                while (inner != null)
                {
                    string message = GeneratePadding("   ", ++lvl) + inner.Message;
                    sb.AppendLine(message);

                    inner = inner.InnerException;
                }
            }

            string str = sb.ToString();

            return ShowError(str, title, icon, owner);
        }

        private static string GeneratePadding(string padding, int number)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < number; i++)
            {
                sb.Append(padding);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Show an error MessageBox showing the message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static MessageBoxResult ShowError(string message, string title = "Error", MessageBoxImage icon = MessageBoxImage.Error, Window owner = null)
        {
            if (owner == null)
                return MessageBox.Show(message, title, MessageBoxButton.OK, icon);
            else
                return MessageBox.Show(owner, message, title, MessageBoxButton.OK, icon);
        }


        /// <summary>
        /// Show a confirmation MessageBox
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static MessageBoxResult ShowConfirm(string message, string title, MessageBoxImage icon = MessageBoxImage.Question)
        {
            return MessageBox.Show(message, title, MessageBoxButton.YesNo, icon);
        }

        /// <summary>
        /// Shows and returns the confirmation MessageBox as a bool
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static bool ShowConfirmAsBool(string message, string title, MessageBoxImage icon = MessageBoxImage.Question)
        {
            return ShowConfirm(message, title, icon) == MessageBoxResult.Yes;
        }

        /// <summary>
        /// Shows and informational MessageBox
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public static void ShowInfo(string message, string title)
        {
            var result = MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void ShowInfo(Window owner, string message, string title)
        {
            var result = MessageBox.Show(owner, message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
