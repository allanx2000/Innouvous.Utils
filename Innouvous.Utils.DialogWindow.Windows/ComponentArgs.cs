using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Innouvous.Utils.DialogWindow.Windows
{
    public struct ComponentArgs
    {
        public ComponentFactory.Components ComponentType { get; set; }
        public string FieldName { get; set; }
        public string DisplayName { get; set; }
    }
}
