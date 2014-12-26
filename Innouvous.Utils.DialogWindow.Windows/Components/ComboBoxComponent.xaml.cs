using Innouvous.Utils.MVVM;
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
    /// <summary>
    /// Interaction logic for ComboBoxComponent.xaml
    /// </summary>
    public partial class ComboBoxComponent : ValueComponent
    {
        private ComboBoxViewModel vm;
        public const string Values = "Values";
        public const string DisplayMemberPath = "DisplayMemberPath";


        public ComboBoxComponent(ComponentArgs options, List<object> values, string displayMemberPath = null) : base(options)
        {
            vm = new ComboBoxViewModel(values);
            vm.PropertyChanged += vm_PropertyChanged;

            InitializeComponent();

            this.DataContext = vm;

            if (displayMemberPath != null)
            {
                Box.DisplayMemberPath = displayMemberPath;
            }  
           
        }

        void vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Selected")
                SetData(vm.Selected);
        }

        

        class ComboBoxViewModel : ViewModel
        {
            public object selected;
            public object Selected
            {
                get
                {
                    return selected;
                }
                set
                {
                    selected = value;
                    
                    RaisePropertyChanged("Selected");
                }
            }

            public List<object> Options
            {
                get;
                set;
            }

            public ComboBoxViewModel(List<object> values)
            {
                this.Options = values;
                RaisePropertyChanged("Options");
            }
        }
    }
}
