using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FirelightCore
{
    [Serializable]
    public class ObservableDictionary<K,V> : Dictionary<K, V>
    {
        public delegate void DictionaryModifiedHandler();
        /// <summary>
        /// Raised when a new frame is ready to be processed and sent to the LED strip.
        /// </summary>
        public event DictionaryModifiedHandler Modified;

        new public V this[K key]
        { 
            get
            {
                return base[key];
            }

            set
            {
                base[key] = value;
                Modified?.Invoke();
            }
        }


        public ObservableDictionary() { }
        public ObservableDictionary(Dictionary<K, V> dictionary) : base(dictionary) { }

        public ObservableDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
