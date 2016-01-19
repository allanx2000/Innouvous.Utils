using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

using System.Runtime.CompilerServices;

namespace Innouvous.Utils.MVVM
{
    public abstract class ObservableClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        private const string SET = "set_";
        private static readonly int SET_LENGTH = SET.Length;

        /// <summary>
        /// Infers the calling setter from the call stack
        /// </summary>
        protected void RaisePropertyChanged(int maxFrames = 5)
        {
            var stack = new System.Diagnostics.StackTrace();
            var frames = stack.GetFrames();


            int stop = Math.Max(frames.Length - 1, 0);

            for (int i = frames.Length - 1; i >= stop; i--)
            {
                string name = frames[i].GetMethod().Name;

                if (name.StartsWith(SET))
                {
                    RaisePropertyChanged(name.Substring(SET_LENGTH));
                    return;
                }
                
            }

            //Reverse
            stop = Math.Min(maxFrames, frames.Length - 1);
            
            for (int i = 0; i <= maxFrames; i++)
            {
                string name = frames[i].GetMethod().Name;

                if (name.StartsWith(SET))
                {
                    RaisePropertyChanged(name.Substring(SET_LENGTH));
                    return;
                }
            }

            //Warn?
        }
    }
}
