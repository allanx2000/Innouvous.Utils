using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Innouvous.Utils
{
    /// <summary>
    /// Adds functionality to disable the X button on the Window
    /// </summary>
    public class DialogWindow : Window
    {
        private bool shouldClose = false;

        protected void CanClose()
        {
            shouldClose = true;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !shouldClose;
            
            base.OnClosing(e);
        }
    }
}
