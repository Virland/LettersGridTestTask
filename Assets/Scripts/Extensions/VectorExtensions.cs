using UnityEngine;

namespace Extensions
{
    static class VectorExtensions
    {
        public static Vector3 DivideComponentWise(this Vector3 v, Vector3 by)
        {
            return new Vector3(
                v.x / by.x,
                v.y / by.y,
                v.z / by.z
            );
        }

        public static Vector2 DivideComponentWise(this Vector2 v, Vector2 by)
        {
            return new Vector2(
                v.x / by.x,
                v.y / by.y
            );
        }
    }
}