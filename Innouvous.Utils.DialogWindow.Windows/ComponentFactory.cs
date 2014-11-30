using Innouvous.Utils.DialogWindow.Windows.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Innouvous.Utils.DialogWindow.Windows
{
    /// <summary>
    /// Creates Components for the DialogControl
    /// </summary>
    public class ComponentFactory
    {
        public enum Components
        {
            TextBox,
            FolderSelector,
            OpenFileSelector,
            SaveFileSelector,
            DatePicker,
            DateTimePicker
        }

        public static ValueComponent MakeComponent(ComponentArgs options)
        {
            ValueComponent component;

            switch (options.ComponentType)
            {
                case Components.TextBox:
                    component = new TextBoxComponent(options);
                    break;
                case Components.SaveFileSelector:
                case Components.OpenFileSelector:
                case Components.FolderSelector:
                    component = new PathSelectComponent(options);
                    break;
                default:
                    throw new Exception("Component not found");
            }

            return component;
        }
    }
}
