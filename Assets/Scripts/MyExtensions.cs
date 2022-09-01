using UnityEngine;

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
}
