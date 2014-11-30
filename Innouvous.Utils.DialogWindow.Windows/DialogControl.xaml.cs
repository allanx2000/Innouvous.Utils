using System;
using System.Collections.Generic;
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

namespace Innouvous.Utils.DialogWindow.Windows
{
    
    public partial class DialogControl : UserControl
    {
        public Dictionary<string, string> Values { get; private set; }

        private DialogControlOptions options;

        public DialogControl()
        {
            InitializeComponent();
        }

        public void SetupControl(DialogControlOptions options)
        {
            this.options = options;

            switch (options.SelectedMode)
            {
                case DialogControlOptions.Mode.DataInput:

                    //Add Column
                    ContentGrid.ColumnDefinitions.Add(new ColumnDefinition(){Width= GridLength.Auto});
                    ContentGrid.ColumnDefinitions.Add(new ColumnDefinition(){Width= new GridLength(1, GridUnitType.Star)});

                    int rowCounter = -1;
                    //Add the Rows
                    foreach (ComponentArgs field in options.Fields)
                    {
                        ContentGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                        rowCounter++;

                        Label nameLabel = new Label() { Content = field.DisplayName};
                        nameLabel.SetValue(Grid.RowProperty, rowCounter);
                        nameLabel.SetValue(Grid.ColumnProperty, 0);

                        UserControl component = ComponentFactory.MakeComponent(field) as UserControl;
                        component.SetValue(Grid.RowProperty, rowCounter);
                        component.SetValue(Grid.ColumnProperty, 1);

                        ContentGrid.Children.Add(nameLabel);
                        ContentGrid.Children.Add(component);
                    }

                    break;
                case DialogControlOptions.Mode.TextBoxMessage:
                    TextBox message = new TextBox()
                    {
                        AcceptsReturn = true,
                        AcceptsTab = true,
                        Text = options.TextBoxMessageContents,
                        IsReadOnly = !options.TextBoxCanEdit
                    };

                    ContentGrid.Children.Add(message);
                    break;           
            }
        }

        public Dictionary<string, object> GetOptionsData()
        {
            if (options != null && options.SelectedMode == DialogControlOptions.Mode.DataInput)
            {
                Dictionary<string, object> values = new Dictionary<string, object>();

                foreach (object o in ContentGrid.Children)
                {
                    IValueComponent component = o as IValueComponent;

                    if (component != null)
                    {
                        values.Add(component.FieldName, component.Data);
                        //values.Add(tb.Tag.ToString(), tb.Text);
                    }
                }

                return values;
            }
            else return null;
        }
    }


    
}
