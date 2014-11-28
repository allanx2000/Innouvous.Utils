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

namespace Innouvous.Utils
{
    
    public partial class DialogControl : UserControl
    {
        public event RoutedEventHandler CloseClicked;

        public Dictionary<string, string> Values { get; private set; }

        private DialogControlOptions options;

        public DialogControl()
        {
            InitializeComponent();
        }

        public void SetupControl(DialogControlOptions options)
        {
            this.options = options;

            CloseClicked = options.GetCloseAction();

            switch (options.SelectedMode)
            {
                case DialogControlOptions.Mode.DataInput:

                    //Add Column
                    ContentGrid.ColumnDefinitions.Add(new ColumnDefinition(){Width= GridLength.Auto});
                    ContentGrid.ColumnDefinitions.Add(new ColumnDefinition(){Width= new GridLength(1, GridUnitType.Star)});

                    int rowCounter = -1;
                    //Add the Rows
                    foreach (string name in options.FieldNames)
                    {
                        ContentGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                        rowCounter++;

                        Label nameLabel = new Label() { Content = name };
                        nameLabel.SetValue(Grid.RowProperty, rowCounter);
                        nameLabel.SetValue(Grid.ColumnProperty, 0);


                        TextBox valueTextBox = new TextBox() { Tag = name };
                        valueTextBox.SetValue(Grid.RowProperty, rowCounter);
                        valueTextBox.SetValue(Grid.ColumnProperty, 1);

                        ContentGrid.Children.Add(nameLabel);
                        ContentGrid.Children.Add(valueTextBox);
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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (options.SelectedMode == DialogControlOptions.Mode.DataInput)
            {
                Dictionary<string, string> values = new Dictionary<string, string>();

                foreach (object o in ContentGrid.Children)
                {
                    TextBox tb = o as TextBox;

                    if (tb != null)
                    {
                        values.Add(tb.Tag.ToString(), tb.Text);
                    }
                }

                options.SetReturnValues(values);

            
            }

            CloseClicked.Invoke(sender, e);
        }
    }


    public class DialogControlOptions
    {
        public enum Mode
        {
            TextBoxMessage,
            DataInput
        }

        public Mode SelectedMode { get; private set; }

        public string TextBoxMessageContents { get; private set; }
        public bool TextBoxCanEdit { get; private set; }

        public string InstructionsLabel { get; private set; }

        public string Title { get; private set; }

        public event RoutedEventHandler CloseAction;

        public RoutedEventHandler GetCloseAction()
        {
            return CloseAction;
        }

        private DialogControlOptions()
        {   
        }

        public List<string> FieldNames { get; private set; }

        public static DialogControlOptions SetTextBoxMessageOptions(string title, string message, bool canEdit, RoutedEventHandler closeAction)
        {
            var opts = new DialogControlOptions()
            {
                 CloseAction = closeAction,
                 Title = title,
                 TextBoxMessageContents = message,
                 SelectedMode = Mode.TextBoxMessage,
                 TextBoxCanEdit = canEdit
            };

            return opts;
        }

        public static DialogControlOptions SetDataInputOptions(string title, string instructions, List<string> fields, RoutedEventHandler closeAction)
        {
            var opts = new DialogControlOptions()
            {
                CloseAction = closeAction,
                Title = title,
                InstructionsLabel = instructions,
                SelectedMode = Mode.DataInput,
                FieldNames = fields
            };

            return opts;
        }


        public Dictionary<string, string> ReturnValues {get; private set;}

        public void SetReturnValues(Dictionary<string, string> values)
        {
            ReturnValues = values;
        }
    }
}
