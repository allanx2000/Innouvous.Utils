using Innouvous.Utils.DialogWindow.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Innouvous.Utils.DialogWindow.Windows.Components
{
    public class BooleanComponent : ValueComponent
    {

        public BooleanComponent(ComponentArgs options, bool dontWrapInLabel = false) : base(options)
        {
            Initialize(dontWrapInLabel);
        }

        
        private CheckBox box;
        private void Initialize(bool dontWrapInLabel)
        {
            box = new CheckBox();
            
            box.IsChecked = Options.InitialData != null ? (bool)Options.InitialData : false;

            //Need To Handle both
            box.Checked += box_Checked;
            box.Unchecked += box_Checked;


            var dont_wrap = dontWrapInLabel;
            if (dontWrapInLabel)
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
