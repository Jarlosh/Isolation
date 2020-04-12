using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnRandom = UnityEngine.Random;
using Random = System.Random;
// ReSharper disable InconsistentNaming

namespace _Game.Scripts.Tools
{
    public static class BehExt
    {
        public static void print(this MonoBehaviour beh, params object[] objects)
        {
            print(beh, "\n", objects);
        }
        public static void print(this MonoBehaviour beh, string separator, params object[] objects)
        {
            if (objects.Length == 0) return;
            var text = string.Join(separator, objects.Select(o => o.ToString()));
            beh.print(text);
        }
        
    }
    public static class JLinqExt
    {
        public static T Pick<T>(this IEnumerable<T> collection)
        {
            var enumerable = collection.ToArray();
            if (enumerable.Length == 0) return default(T);
            var i = UnRandom.Range(0, enumerable.Length - 1);
            return enumerable[i];
        }
        
        public static IEnumerable<T> Choices<T>(this IEnumerable<T> collection, int count)
        {
            var r = new Random();
            return collection.OrderBy(n => r.Next()).Take(count);
        }

        public static void Foreach<T>(this IEnumerable<T> collection, params Action<T>[] actions)
        {
            foreach (var item in collection)
            foreach (var action in actions)
                action(item);
        } 
        
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            var result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}