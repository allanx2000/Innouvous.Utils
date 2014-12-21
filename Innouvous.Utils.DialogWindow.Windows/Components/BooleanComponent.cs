using Innouvous.Utils.DialogWindow.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Innouvous.Utils.DialogWindow.Windows.Components
{
    class BooleanComponent : ValueComponent
    {
        public const string DONT_WRAP_IN_LABEL = "DontWrap";
        public BooleanComponent(ComponentArgs options) : base(options)
        {
            Initialize();
        }

        
        private CheckBox box;
        private void Initialize()
        {
            box = new CheckBox();
            
            box.IsChecked = Options.InitialData != null ? (bool)Options.InitialData : false;

            box.Checked += box_Checked;

            var dont_wrap = Options.GetCustomParameter(DONT_WRAP_IN_LABEL);
            if (dont_wrap is bool && (bool)dont_wrap == true)
                this.Content = box;
            else
            {
                //Wrap it in a Label to get the Margin and Padding values
                Label lbl = new Label();
                lbl.Content = box;

                this.Content = lbl;
            }
        }

        void box_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            base.Data = box.IsChecked;

            e.Handled = true;
        }
    }
}
