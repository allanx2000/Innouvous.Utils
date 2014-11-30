using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Innouvous.Utils.DialogWindow.Windows
{
    /// <summary>
    /// Holds the initialization parameters for the DialogControl
    /// </summary>

    public class DialogControlOptions
    {
        public enum Mode
        {
            TextBoxMessage,
            DataInput
        }

        public Mode SelectedMode { get; private set; }

        public string InstructionsLabel { get; private set; }

        public string Title { get; private set; }

        private DialogControlOptions()
        {
        }


        #region TextBoxMessage
        
        public string TextBoxMessageContents { get; private set; }
        public bool TextBoxCanEdit { get; private set; }

        public static DialogControlOptions SetTextBoxMessageOptions(string title, string message, bool canEdit, RoutedEventHandler closeAction)
        {
            var opts = new DialogControlOptions()
            {
                Title = title,
                TextBoxMessageContents = message,
                SelectedMode = Mode.TextBoxMessage,
                TextBoxCanEdit = canEdit
            };

            return opts;
        }

        #endregion

        #region Data Input
        public List<ComponentArgs> Fields { get; private set; }

        public static DialogControlOptions SetDataInputOptions(string title, string instructions, List<ComponentArgs> fields)
        {
            var opts = new DialogControlOptions()
            {
                Title = title,
                InstructionsLabel = instructions,
                SelectedMode = Mode.DataInput,
                Fields = fields
            };

            return opts;
        }

        #endregion

    }
}
