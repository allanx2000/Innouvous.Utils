using System.ComponentModel;
using System.Windows.Input;

namespace Innouvous.Utils.MVVM
{
    public abstract class ViewModel : ObservableClass
    {
        
        protected new void RaisePropertyChanged(string propertyName)
        {
            base.RaisePropertyChanged(propertyName);
        }

        public virtual void RefreshViewModel()
        {
            foreach (var p in this.GetType().GetProperties())
            {
                RaisePropertyChanged(p.Name);
            }
        }
    }

    public abstract class ViewModel<T> : ViewModel
    {
        private T data;
                
        public T Data
        {
            get
            {
                return data;
            }
        }

        protected ViewModel(T source)
        {
            SetSource(source);
        }

        public virtual void SetSource(T source)
        {
            this.data = source;
            RaisePropertyChanged("Data");

            RefreshViewModel();
        }

      

       

        
    }
}
