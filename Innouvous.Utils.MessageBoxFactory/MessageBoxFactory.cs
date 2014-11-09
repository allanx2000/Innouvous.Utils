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
        public static MessageBoxResult ShowError(Exception e, string title = "Error", MessageBoxImage icon = MessageBoxImage.Error)
        {
            return ShowError(e.Message, title, icon);
        }

        public static MessageBoxResult ShowError(string message, string title = "Error", MessageBoxImage icon = MessageBoxImage.Error)
        {
            return MessageBox.Show(message, title, MessageBoxButton.OK, icon);
        }

        public static MessageBoxResult ShowConfirm(string message, string title, MessageBoxImage icon = MessageBoxImage.Question)
        {
            return MessageBox.Show(message, title, MessageBoxButton.YesNo, icon); 
        }

        public static bool ShowConfirmAsBool(string message, string title, MessageBoxImage icon = MessageBoxImage.Question)
        {
            return ShowConfirm(message, title, icon) == MessageBoxResult.Yes;
        }

        public static void ShowInfo(string message, string title)
        {
            var result = MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information); 
        }

    }
}
