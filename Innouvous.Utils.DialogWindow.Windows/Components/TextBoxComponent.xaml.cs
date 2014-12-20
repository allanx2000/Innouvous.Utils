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
        public enum FieldType
        {
            Text,
            Integer,
            Double
        }


        public const string MAX_LENGTH = "MaxLength";
        public const string FIELD_TYPE = "FieldType";
        
        private FieldType type = FieldType.Text;

        private Brush color;

        public Brush Color
        {
            get
            {
                return color;
            }
            private set
            {
                color = value;
                RaisePropertyChanged("Color");
            }
        }


        public object Value
        {
            get
            {
                return Data;
            }
            set
            {
                if (IsValid(value))
                {
                    Data = value;
                }

                RaisePropertyChanged("Value");
            }
        }


        public TextBoxComponent(ComponentArgs args)
            : base(args)
        {
            this.DataContext = this;

            InitializeComponent();

            //Max Length
            var maxLength = args.GetCustomParameter(MAX_LENGTH);
            if (maxLength != null)
            {
                int l = (int)maxLength;

                CalculateAndSetTextBoxSize(l);
            }

            //Field Type
            var type = args.GetCustomParameter(FIELD_TYPE);

            if (type != null)
                this.type = (FieldType)type;

            //Set Data Field Name
            base.SetDataFieldAlias("Value");
        }


        private void CalculateAndSetTextBoxSize(int length)
        {
            ValueTextBox.Width = length * ValueTextBox.FontSize;
            ValueTextBox.MaxLength = length;
            ValueTextBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
        }

        private bool IsValid(object value)
        {
            switch (type)
            {
                case FieldType.Text:
                    return true;
                case FieldType.Double:
                    double dbl;
                    return Double.TryParse((string) value, out dbl);
                case FieldType.Integer:
                    int i;
                    return Int32.TryParse((string)value, out i);
                default:
                    return false;
            }

        }

    }
}
