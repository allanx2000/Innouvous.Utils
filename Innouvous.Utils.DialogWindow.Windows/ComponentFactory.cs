using Innouvous.Utils.DialogWindow.Windows.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Innouvous.Utils.DialogWindow.Windows
{
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

        public static IValueComponent MakeComponent(ComponentArgs options)
        {
            switch (options.ComponentType)
            {
                case Components.TextBox:
                    return new TextBoxComponent(options);
                case Components.SaveFileSelector:
                case Components.OpenFileSelector:
                case Components.FolderSelector:
                    return new PathSelectComponent(options);
                default:
                    throw new Exception("Component not found");
            }
        }
    }
}
