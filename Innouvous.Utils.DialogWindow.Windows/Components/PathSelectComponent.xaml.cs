using Innouvous.Utils.MVVM;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for PathSelectComponent.xaml
    /// </summary>
    public partial class PathSelectComponent :  ValueComponent //UserControl, IValueComponent
    {
        public const string WINDOW_TITLE = "WindowTitle";
        public const string EXTENSION = "Extension";
        public const string CONFIRM_OVERWRITE = "ConfirmOverwrite";

        
        public string Value
        {
            get
            {
                return (string) Data;
            }
            set
            {
                Data = value;

                RaisePropertyChanged("Value");
            }
        }

        public static string MakeExtension(string name, string extension, string existingFilter = "")
        {
            return DialogsUtility.AddExtension(existingFilter, name, extension);
        }

        private static readonly List<ComponentFactory.Components> ValidTypes = new List<ComponentFactory.Components>() 
        {
            ComponentFactory.Components.FolderSelector,
            ComponentFactory.Components.OpenFileSelector,
            ComponentFactory.Components.SaveFileSelector,
        };

        public PathSelectComponent(ComponentArgs args) : base(args)
        {
            //Validate Option
            if (!ValidTypes.Contains(args.ComponentType))
            {
                throw new Exception("Cannot use PathSelectComponent for: " + args.ComponentType);
            }


            //component = new ValueComponent(args);
            
            this.DataContext = this;

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
                        case ComponentFactory.Components.SaveFileSelector:
                            browseCommand = new CommandHelper(BrowseForSaveFile);
                            break;
                        case ComponentFactory.Components.OpenFileSelector:
                            browseCommand = new CommandHelper(BrowseForLoadFile);
                            break;
                    }
                }

                return browseCommand;
            }
        }

        private void BrowseForLoadFile()
        {
            var window = DialogsUtility.CreateOpenFileDialog();

            var title = (string) Options.GetCustomParameter(WINDOW_TITLE);
            if (title != null) window.Title = title;

            window.Filter = (string)Options.GetCustomParameter(EXTENSION);

            if (System.IO.Directory.Exists(Value))
                window.InitialDirectory = Value;
            else if (System.IO.File.Exists(Value))
            {
                var file = new FileInfo(Value);
                window.InitialDirectory = file.DirectoryName;
                window.FileName = file.Name;
            }

            window.ShowDialog();
        }

        private void BrowseForSaveFile()
        {
            var window = DialogsUtility.CreateSaveFileDialog();

            var title = Options.GetCustomParameter(WINDOW_TITLE);
            if (title != null) window.Title = (string) title;
            
            object confirmOverwrite = Options.GetCustomParameter(CONFIRM_OVERWRITE);

            if (confirmOverwrite != null)
                window.OverwritePrompt = (bool) confirmOverwrite;

            window.Filter = (string) Options.GetCustomParameter(EXTENSION);

            if (System.IO.Directory.Exists(Value))
                window.InitialDirectory = Value;
            else if (System.IO.File.Exists(Value))
            {
                var file = new FileInfo(Value);
                window.InitialDirectory = file.DirectoryName;
                window.FileName = file.Name;
            }

            window.ShowDialog();
        }

        private void BrowseForFolder()
        {
            var window = DialogsUtility.CreateFolderBrowser();

            if (System.IO.Directory.Exists(Value))
                window.SelectedPath = Value;

            window.ShowDialog();

            if (!String.IsNullOrEmpty(window.SelectedPath))
            {
                Value = window.SelectedPath;
            }
        }
    }
}
