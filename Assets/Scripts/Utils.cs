using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace DefaultNamespace
{
    public static class Utils
    {
        private static Random _random = new Random();
        
        
        public static bool HasComponent<T>(this MonoBehaviour b)
        {
            return b.GetComponent<T>() != null;
        }
        
        public static bool HasComponent<T>(this GameObject gameObject)
        {
            return gameObject.GetComponent<T>() != null;
        }

        public static T GetRandomElement<T>(this T[] a)
        {
            return a[_random.Next(0, a.Count() - 1)];
        }
        
        public static T GetRandomElement<T>(this List<T> a)
        {
            return a[_random.Next(0, a.Count() - 1)];
        }
       
    }
}