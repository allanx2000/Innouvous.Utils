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
            try
            {
                if (bucket.ContainsKey(key) && bucket[key] != null)
                    return (T)bucket[key];
                else
                    return default(T);
            }
            catch (Exception e)
            {
                //Seems to happen when object is garbage collected? Collection.Remove
                return default(T);
            }
        }
    }
}
