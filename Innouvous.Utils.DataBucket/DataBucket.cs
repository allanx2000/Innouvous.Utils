using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils.DataBucket
{
    public class DataBucket //Move to MVVM?
    {
        private Dictionary<string, object> bucket = new Dictionary<string, object>();

        /*
        public void Set<T>(string key, T value)
        {
            Set(key, value);
        }
        */

        public void Set(string key, object value)
        {
            bucket[key] = value;
        }

        public T Get<T>(string key) //where T : ICollection <--Example of where
        {
            if (bucket.ContainsKey(key))
                return (T)bucket[key];
            else
                return default(T);
        }
    }
}
