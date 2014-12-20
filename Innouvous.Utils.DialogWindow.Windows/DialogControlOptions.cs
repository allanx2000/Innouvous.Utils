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

        private DialogControlOptions()
        {
        }

        
        #region TextBoxMessage

        public string TextBoxMessageContents { get; set; }
        public bool TextBoxCanEdit { get; set; }


        public static DialogControlOptions SetTextBoxMessageOptions(string message, bool canEdit)
        {
            var opts = new DialogControlOptions()
            {
                //Title = title,
                TextBoxMessageContents = message,
                SelectedMode = Mode.TextBoxMessage,
                TextBoxCanEdit = canEdit
            };

            return opts;
        }

        #endregion

        #region Data Input
        
        public List<ComponentArgs> Fields { get; private set; }
        public string InstructionsLabel { get; set; }
        public bool BoldLabels { get; set; }

        public static DialogControlOptions SetDataInputOptions(List<ComponentArgs> fields, string instructions = null)
        {
            var opts = new DialogControlOptions()
            {   InstructionsLabel = instructions,
                SelectedMode = Mode.DataInput,
                Fields = fields
            };

            return opts;
        }

        #endregion

    }
}
