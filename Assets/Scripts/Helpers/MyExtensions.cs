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
}
