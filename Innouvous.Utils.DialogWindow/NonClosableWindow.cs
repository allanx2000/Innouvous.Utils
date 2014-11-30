using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Innouvous.Utils.DialogWindow
{
    /// <summary>
    /// Adds functionality to disable the X button on the Window
    /// </summary>
    public class NonClosableWindow : Window
    {
        private bool shouldClose = false;

        public void CanClose()
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
