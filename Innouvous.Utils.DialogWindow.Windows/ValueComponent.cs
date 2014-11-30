using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Innouvous.Utils.DialogWindow.Windows
{
    public interface IValueComponent : INotifyPropertyChanged
    {
        string FieldName { get; }
        string DisplayName { get; }
        string ComponentType { get; }
        ComponentArgs Options { get; }
        object Data { get; }
    }

    //Base class for Component objects 
    public abstract class ValueComponent : UserControl, IValueComponent
    {
        private ComponentArgs options;

        public ValueComponent(ComponentArgs options)
        {
            this.options = options;
            this.Data = options.InitialData;
        }

        public string FieldName
        {
            get
            {
                return options.FieldName;
            }
        }

        public string DisplayName
        {
            get
            {
                return options.DisplayName;
            }
        }

        public string ComponentType
        {
            get
            {
                return options.ComponentType.ToString();
            }
        }

        public ComponentArgs Options
        {
            get
            {
                return options;
            }
            private set
            {
                options = value;
            }
        }

        public object Data { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
