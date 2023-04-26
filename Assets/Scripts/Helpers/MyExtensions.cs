using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Helpers
{
    public static class MyExtensions
    {
        public static bool TryGetComponentInParent<T>(this GameObject origin, out T component)
        {
            component = origin.GetComponentInParent<T>();
            return component != null;
        }
    
        public static Vector3 RotateAngle(this Vector3 origin, float angle)
        {
            return Quaternion.AngleAxis(angle, Vector3.up) * origin;
        }

        public static void SetLayerToThisAndChildren(this GameObject gameObject, int layer)
        {
            var children = gameObject.GetComponentsInChildren<Transform>().Select(transform => transform.gameObject);

            foreach (var child in children)
                child.layer = layer;
        
            gameObject.layer = layer;
        }
    }
    
    [Serializable]
    public class UnityDictionary<Kt, Vt>
    {
        [SerializeField]
        private List<UnityDictionaryItem<Kt, Vt>> _dictionary = new List<UnityDictionaryItem<Kt, Vt>>();

        public bool TryGetValue(Kt key, out Vt value)
        {
            value = default;
            
            foreach (var item in _dictionary)
            {
                if (item.Key.Equals(key))
                {
                    value = item.Value;
                    return true;
                }
            }

            return false;
        }
    }
    
    [Serializable]
    public class UnityDictionaryItem<Kt, Vt>
    {
        [SerializeField] private Kt _key;
        [SerializeField] private Vt _value;

        public Kt Key => _key;
        public Vt Value => _value;
    }
}
