using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Innouvous.Utils
{
    public class NestedDictionary
    {
        private Type keyType;
        //private Type valueType;
        private List<Type> childTypes;
        private bool isLastNode;

        private Dictionary<object, object> _dict = new Dictionary<object,object>();

        public static NestedDictionary CreateNestedDictionary(params Type[] types)
        {
            return CreateNestedDictionary(types.ToList());
        }
        
        private static NestedDictionary CreateNestedDictionary(List<Type> types)
        {
            //int total = types.Count;

            return new NestedDictionary(types);
        }

        private NestedDictionary(Type type1, Type type2)
        {
        }

        private NestedDictionary(List<Type> types)
        {
            this.keyType = types[0];
            types.RemoveAt(0);
            isLastNode = types.Count == 1;

            if (!isLastNode)
                childTypes = new List<Type>(types);
        }

        public object GetItem(params object[] keys)
        {
            return GetItem(keys.ToList());
        }

        public object GetItem(List<object> keys)
        {

            object key = keys[0];
            keys.RemoveAt(0);

            if (key.GetType() != keyType)
                throw new Exception("Key type does not match");

            if (_dict.ContainsKey(key))
            {
                object value = _dict[key];

                if (isLastNode)
                    return value;
                else return ((NestedDictionary)value).GetItem(keys);
            }
            else return null;
        }

        public void AddItem(params object[] keysAndValue)
        {
            AddItem(keysAndValue.ToList());
        }

        private void AddItem(List<object>keysAndValue)
        {
            var key = keysAndValue[0];
            keysAndValue.RemoveAt(0);

            if (isLastNode)
            {
                var value = keysAndValue[0];

                _dict.Add(key, value);
            }
            else
                _dict.Add(key, CreateNestedDictionary(childTypes));
        }
    }
}
