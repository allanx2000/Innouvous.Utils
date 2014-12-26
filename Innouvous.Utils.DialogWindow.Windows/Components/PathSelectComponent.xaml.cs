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
    public partial class PathSelectComponent : ValueComponent //UserControl, IValueComponent
    {
        /*public const string WINDOW_TITLE = "WindowTitle";
        public const string EXTENSION = "Extension";
        public const string CONFIRM_OVERWRITE = "ConfirmOverwrite";
        */

        private bool confirmOverwrite;
        private string windowTitle;
        private string extension;
        private DialogType type;

        public string Value
        {
            get
            {
                return (string)Data;
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

        public enum DialogType
        {
            SaveFile,
            OpenFile,
            SelectFolder
        }

        public static PathSelectComponent SaveFileComponent(ComponentArgs args, string title = "Save File", string ext = null, bool confirmOverwrite = true)
        {
            var obj = new PathSelectComponent(args, DialogType.SaveFile, ext, title);
            obj.confirmOverwrite = confirmOverwrite;

            return obj;
        }

        public static PathSelectComponent OpenFileComponent(ComponentArgs args, string title = "Open File", string ext = null)
        {
            return new PathSelectComponent(args, DialogType.SaveFile, ext, title);
        }

        public static PathSelectComponent SelectFolderComponent(ComponentArgs args, string title = "Select Folder")
        {
            return new PathSelectComponent(args, DialogType.SaveFile, null, title);
        }



        private PathSelectComponent(ComponentArgs args, DialogType type, string ext, string windowTitle = null)
            : base(args)
        {
            this.DataContext = this;

            this.type = type;
            this.extension = String.IsNullOrEmpty(ext) ? MakeExtension("All Files", "*.*") : ext;
            this.windowTitle = windowTitle;

            InitializeComponent();
        }

        private CommandHelper browseCommand;

        public CommandHelper BrowseCommand
        {
            get
            {
                if (browseCommand == null)
                {
                    switch (type)
                    {
                        case DialogType.SelectFolder:
                            browseCommand = new CommandHelper(BrowseForFolder);
                            break;
                        case DialogType.SaveFile:
                            browseCommand = new CommandHelper(BrowseForSaveFile);
                            break;
                        case DialogType.OpenFile:
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

            var title = windowTitle;

            window.Filter = extension;

            if (System.IO.Directory.Exists(Value))
                window.InitialDirectory = Value;
            else if (System.IO.File.Exists(Value))
            {
                var file = new FileInfo(Value);
                window.InitialDirectory = file.DirectoryName;
                window.FileName = file.Name;
            }

            window.ShowDialog();

            if (!String.IsNullOrEmpty(window.FileName))
            {
                Value = window.FileName;
            }
        }

        private void BrowseForSaveFile()
        {
            var window = DialogsUtility.CreateSaveFileDialog();

            var title = windowTitle;

            window.Filter = extension;

            window.OverwritePrompt = confirmOverwrite;
                        
            if (System.IO.Directory.Exists(Value))
                window.InitialDirectory = Value;
            else if (System.IO.File.Exists(Value))
            {
                var file = new FileInfo(Value);
                window.InitialDirectory = file.DirectoryName;
                window.FileName = file.Name;
            }

            window.ShowDialog();

            if (!String.IsNullOrEmpty(window.FileName))
            {
                Value = window.FileName;
            }
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
