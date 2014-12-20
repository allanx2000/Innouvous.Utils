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
using System.Windows.Shapes;

namespace Innouvous.Utils.DialogWindow.Windows
{
    /// <summary>
    /// Simple container window for DialogControl
    /// 
    /// I guess more as a sample rather than adding anything useful
    /// </summary>
    public partial class SimpleDialogWindow : NonClosableWindow
    {
        private DialogControlOptions options;

        public DialogControl DataControl
        {
            get
            {
                return DialogControl;   
            }
        }

        private readonly SimpleDialogViewModel viewmodel;

        public SimpleDialogWindow(DialogControlOptions options)
        {
            InitializeComponent();

            viewmodel = new SimpleDialogViewModel(options, this);
            this.DataContext = viewmodel;

            //this.Title = options.Title;
            this.options = options;

            DialogControl.SetupControl(options);
        }
    }

    public class SimpleDialogViewModel : ViewModel
    {
        private NonClosableWindow window;

        public SimpleDialogViewModel(DialogControlOptions options, NonClosableWindow window)
        {
            this.window = window;

            if (options.SelectedMode == DialogControlOptions.Mode.DataInput)
            {
                SaveButtonVisibility = Visibility.Visible;
                CloseText = "Cancel";
                
            }
            else
            {
                SaveButtonVisibility = Visibility.Collapsed;
                CloseText = "Close";
            }
        }

        public Visibility SaveButtonVisibility {get; private set;}

        public string CloseText {get; private set;}

        public ICommand SaveCommand
        {
            get
            {
                return new CommandHelper(Save);
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return new CommandHelper(DoClose);
            }
        }

        private void Save(object sender)
        {
            //Validation logic?

            DoClose();
        }

        private void DoClose()
        {
            window.CanClose();
            window.Close();
        }


    }
}
