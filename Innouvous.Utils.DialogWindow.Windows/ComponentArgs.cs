using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Innouvous.Utils.DialogWindow.Windows
{
    /// <summary>
    /// Holds parameters for setting up and initializing components in a DataInput DialogControl
    /// </summary>
    public struct ComponentArgs
    {
        public Func<ValueComponent> Component { get; set; }

        /// <summary>
        /// Initial value the component should have. User must ensure the object is compatible with the component
        /// </summary>
        public object InitialData { get; set; }

        /// <summary>
        /// Type of the component, used by ComponentFactory to return the correct ValueComponent
        /// </summary>
        //public ComponentFactory.Components ComponentType { get; set; }

        /// <summary>
        /// Name of the field as returned in the results dictionary
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Name shown to users
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Holds component-specific parameters
        /// Individual components should have their parameters as constant strings
        /// 
        /// i.e. TextboxComponent.MAX_LENGTH
        /// </summary>
        //public Dictionary<string, object> CustomParameters { get; set; }

        /// <summary>
        /// Returns the value of the parameter, or null if not found. 
        /// Mostly used by ValueComponents to discover their parameters.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Value or null</returns>
        /*public object GetCustomParameter(string key)
        {
            if (CustomParameters != null && CustomParameters.ContainsKey(key))
                return CustomParameters[key];
            else return null;
        }*/
    }
}
