using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Innouvous.Utils.DialogWindow.Windows.Components
{
    /// <summary>
    /// Interaction logic for DateTimePicker.xaml
    /// </summary>
    public partial class DateTimeComponent : ValueComponent
    {
        public const string DateTimeFormat = "DateTimeFormat";

        private string dateTimeFormat;

        public DateTimeComponent(ComponentArgs options) : base(options)
        {
            InitializeComponent();

            dateTimeFormat = (string) options.GetCustomParameter(DateTimeFormat);
        }

        private void TimeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TimeTextBox.Text))
            {
                base.Data = null;
            }
            else
            {
                DateTime date;
                bool success;
                if (dateTimeFormat != null)
                {
                    var dts = DateTimeStyles.AllowWhiteSpaces & DateTimeStyles.AssumeLocal;
                    success = DateTime.TryParseExact(TimeTextBox.Text, dateTimeFormat, CultureInfo.CurrentCulture, dts, out date);
                }
                else
                    success = DateTime.TryParse(TimeTextBox.Text, out date);

                if (success)
                {
                    base.Data = date;
                }
                else
                {
                    base.Data = null;
                    TimeTextBox.Text = "";
                }
            }
        }
    }
}
