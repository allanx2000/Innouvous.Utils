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

namespace Innouvous.Utils.DialogWindow.Components
{
    public partial class TextBoxComponent : UserControl, IValueComponent
    {
        #region Component Stuff
        private ValueComponent component;

        public string FieldName
        {
            get { return component.FieldName; }
        }

        public string DisplayName
        {
            get { return component.DisplayName; }
        }

        public string ComponentType
        {
            get { return component.ComponentType.ToString(); }
        }

        public ComponentArgs Options
        {
            get { return component.Options; }
        }

        public object Data
        {
            get
            {
                return component.Data;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(name));
            }
        }
        #endregion

        public TextBoxComponent(ComponentArgs args)
        {
            component = new ValueComponent(args);

            InitializeComponent();
        }

        public string Value
        {
            get
            {
                return (string)component.Data;
            }
            set
            {
                component.Data = value;

                RaisePropertyChanged("Value");
            }
        }

    }
}
