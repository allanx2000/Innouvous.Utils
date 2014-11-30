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

namespace Innouvous.Utils.DialogWindow.Windows.Components
{
    public partial class TextBoxComponent : ValueComponent
    {

        public const string MAX_LENGTH = "MaxLength";


        public TextBoxComponent(ComponentArgs args) : base(args)
        {
            this.DataContext = this;

            InitializeComponent();

            var maxLength = args.GetCustomParameter(MAX_LENGTH);
            if (maxLength != null)
            {
                int l = (int)maxLength;

                CalculateAndSetTextBoxSize(l);
            }
        }

        private void CalculateAndSetTextBoxSize(int length)
        {

            ValueTextBox.Width = length * ValueTextBox.FontSize;
            ValueTextBox.MaxLength = length;
            ValueTextBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
        }

        public object Value
        {
            get
            {
                return Data;
            }
            set
            {
                Data = value;

                RaisePropertyChanged("Value");
            }
        }

    }
}
