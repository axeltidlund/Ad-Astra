using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYellowpaper.SerializedCollections
{
    [System.Serializable]
    public struct SerializedKeyValuePair<TKey, TValue>
    {
        public TKey Key;
        [ColorUsage(true, true)]
        public TValue Value;

        public SerializedKeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
