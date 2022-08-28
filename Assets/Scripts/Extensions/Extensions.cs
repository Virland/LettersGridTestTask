using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    static class Extensions
    {
        public static T AddEmptyChild<T>(this T parent, string name = "NewChild") where T : Transform
        {
            var go = new GameObject(name, typeof(T));
            var tForm = go.transform;
            tForm.SetParent(parent, true);
            tForm.localScale = Vector3.one;
            return go.GetComponent<T>();
        }

        public static void Shuffle<T>(this IList<T> list, int count)
        {
            for (int i = 0; i < count; i++)
            {
                int n = Random.Range(0, count);
                T temp = list[i];
                list[i] = list[n];
                list[n] = temp;
            }
        }

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