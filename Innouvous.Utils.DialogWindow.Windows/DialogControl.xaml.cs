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
        private const int DataInputControlHeight = 30;

        public DialogControl()
        {
            InitializeComponent();
        }

        public void SetupControl(DialogControlOptions options)
        {
            this.options = options;

            if (String.IsNullOrEmpty(options.InstructionsLabel))
                InstructionsTextBlock.Visibility = System.Windows.Visibility.Collapsed;

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

                        if (options.BoldLabels)
                        {
                            nameLabel.FontWeight = FontWeights.Bold;
                        }

                        UserControl component;

                        if (field.CustomComponent == null)
                            component = ComponentFactory.MakeComponent(field) as UserControl;
                        else
                            component = field.CustomComponent;

                        component.SetValue(Grid.RowProperty, rowCounter);
                        component.SetValue(Grid.ColumnProperty, 1);

                        component.Height = DataInputControlHeight;
                        
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

        public bool SetFieldValue(string name, object value)
        {
            foreach (Control c in ContentGrid.Children)
            {
                var vc = c as IValueComponent;

                if (vc != null && name == vc.FieldName)
                {
                    vc.SetData(value);

                    return true;
                }
            }

            return false;
            //throw new Exception("Field Name not found: " + name);
        }
    }


    
}
