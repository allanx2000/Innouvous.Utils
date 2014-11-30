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

namespace Innouvous.Utils.DialogWindow.Components
{
    /// <summary>
    /// Interaction logic for PathSelectComponent.xaml
    /// </summary>
    public partial class PathSelectComponent : UserControl, IValueComponent
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

        public string Value
        {
            get
            {
                return (string) component.Data;
            }
            set
            {
                component.Data = value;

                RaisePropertyChanged("Value");
            }
        }

        private static readonly List<ComponentFactory.Components> ValidTypes = new List<ComponentFactory.Components>() 
        {
            ComponentFactory.Components.FolderSelector,
            ComponentFactory.Components.OpenFileSelector,
            ComponentFactory.Components.SaveFileSelector,
        };

        public PathSelectComponent(ComponentArgs args)
        {
            //Validate Option
            if (!ValidTypes.Contains(args.ComponentType))
            {
                throw new Exception("Cannot use PathSelectComponent for: " + args.ComponentType);
            }


            component = new ValueComponent(args);

            InitializeComponent();
        }

        private CommandHelper browseCommand;

        public CommandHelper BrowseCommand
        {
            get
            {
                if (browseCommand == null)
                {
                    switch (Options.ComponentType)
                    {
                        case ComponentFactory.Components.FolderSelector:
                            browseCommand = new CommandHelper(BrowseForFolder);
                            break;
                    }
                }

                return browseCommand;
            }
        }

        private void BrowseForFolder()
        {
            var window = DialogsUtility.CreateFolderBrowser();

            if (!String.IsNullOrEmpty(window.SelectedPath))
            {
                Value = window.SelectedPath;
            }
        }
    }
}
