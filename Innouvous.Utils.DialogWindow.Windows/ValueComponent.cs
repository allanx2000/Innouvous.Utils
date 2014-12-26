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
        //string ComponentType { get; }
        ComponentArgs Options { get; }
        object Data { get; }

        void SetData(object data);
    }

    //Base class for Component objects 
    public abstract class ValueComponent : UserControl, IValueComponent, INotifyPropertyChanged
    {
        private ComponentArgs options;
        

        protected ValueComponent(ComponentArgs options)
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

        /*public string ComponentType
        {
            get
            {
                return options.ComponentType.ToString();
            }
        }*/

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
                if (dataFieldAlias != null && name == "Data")
                    name = dataFieldAlias;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }


        public void SetData(object data)
        {
            Data = data;

            RaisePropertyChanged("Data");
        }

        private string dataFieldAlias = null;
        protected void SetDataFieldAlias(string name)
        {
            dataFieldAlias = name;
        }
    }
}
