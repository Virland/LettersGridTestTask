using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    static class ListExtensions
    {
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
    }
}
