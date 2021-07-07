using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Clicks.Yoga.Portal.Models.Editors
{
    [DataContract]
    public class EntityValueDictionary<TValue> : IDictionary<object, TValue>
    {
        [DataMember]
        private readonly Dictionary<object, TValue> _elements = new Dictionary<object, TValue>();

        public IEnumerator<KeyValuePair<object, TValue>> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _elements.GetEnumerator();
        }

        public void Add(KeyValuePair<object, TValue> item)
        {
            (_elements as IDictionary<object, TValue>).Add(ConvertPair(item));
        }

        public void Clear()
        {
            _elements.Clear();
        }

        public bool Contains(KeyValuePair<object, TValue> item)
        {
            return (_elements as IDictionary).Contains(ConvertPair(item));
        }

        public void CopyTo(KeyValuePair<object, TValue>[] array, int arrayIndex)
        {
            (_elements as IDictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<object, TValue> item)
        {
            return _elements.Remove(ConvertPair(item));
        }

        public int Count
        {
            get { return _elements.Count; }
        }

        public bool IsReadOnly
        {
            get { return (_elements as IDictionary).IsReadOnly; }
        }

        public bool ContainsKey(object key)
        {
            return _elements.ContainsKey(ConvertKey(key));
        }

        public void Add(object key, TValue value)
        {
            _elements.Add(ConvertKey(key), value);
        }

        public bool Remove(object key)
        {
            return _elements.Remove(ConvertKey(key));
        }

        public bool TryGetValue(object key, out TValue value)
        {
            return _elements.TryGetValue(ConvertKey(key), out value);
        }

        public TValue this[object key]
        {
            get { return _elements[ConvertKey(key)]; }
            set { _elements[ConvertKey(key)] = value; }
        }

        public ICollection<object> Keys
        {
            get { return _elements.Keys; }
        }

        public ICollection<TValue> Values
        {
            get { return _elements.Values; }
        }

        private string ConvertKey(object key)
        {
            return key.ToString();
        }

        private KeyValuePair<object, TValue> ConvertPair(KeyValuePair<object, TValue> pair)
        {
            return new KeyValuePair<object, TValue>(ConvertKey(pair.Key), pair.Value);
        }
    }
}