using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace MG
{
    public static class Utils
    {
        private static Dictionary<int, Random> _randomGenerators = new Dictionary<int, Random>();

        public static int GetRandomNumber(int minInclusive, int maxExclusive)
        {
            if (_randomGenerators.ContainsKey(Thread.CurrentThread.ManagedThreadId) == false)
            {
                _randomGenerators.Add(Thread.CurrentThread.ManagedThreadId, new Random());
            }

            return _randomGenerators[Thread.CurrentThread.ManagedThreadId].Next(minInclusive, maxExclusive);
        }

        public static double GetRandomDouble()
        {
            if (_randomGenerators.ContainsKey(Thread.CurrentThread.ManagedThreadId) == false)
            {
                _randomGenerators.Add(Thread.CurrentThread.ManagedThreadId, new Random());
            }

            return _randomGenerators[Thread.CurrentThread.ManagedThreadId].NextDouble();
        }

        public static bool DoesNotContain<T> (this IEnumerable<T> container, T item)
        {
            return !container.Contains(item);
        }

        public static IEnumerable<T> Excluding<T>(this IEnumerable<T> container, T item)
        {
            return container.Where(x => !x.Equals(item));
        }

        public static void Shuffle<T> (this Queue<T> deck)
        {
            var deckList = new List<T>();

            while (deck.Count > 0)
            {
                deckList.Add(deck.Dequeue());
            }

            while (deckList.Count > 0)
            {
                var item = deckList[Utils.GetRandomNumber(0, deckList.Count)];
                deck.Enqueue(item);
                deckList.Remove(item);
            }
        }

        public static void Shuffle<T>(this List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var r1 = Utils.GetRandomNumber(0, list.Count);
                var r2 = Utils.GetRandomNumber(0, list.Count);
                var item1 = list[r1];
                var item2 = list[r2];

                list[r1] = item2;
                list[r2] = item1;
            }
        }

        public static void SyncAdd<T>(this List<T> list, T element)
        {
            lock(list)
            {
                list.Add(element);
            }
        }

        public static T SyncRemoveRandom<T>(this List<T> list)
        {
            lock (list)
            {
                var index = GetRandomNumber(0, list.Count);
                return SyncRemoveAt(list, index);
            }
        }

        public static T SyncRemoveAt<T>(this List<T> list, int index)
        {
            lock(list)
            {
                var element = list[index];
                list.RemoveAt(index);

                return element;
            }
        }

        public static void InvokeIfRequired(this Control control, MethodInvoker action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        public static void CreateOrSet<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }

        public static void CreateOrAddToList<TKey, TValue>(this Dictionary<TKey, List<TValue>> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key].Add(value);
            }
            else
            {
                dictionary.Add(key, new List<TValue>() { value });
            }
        }
    }

    public class AssetComparer : IComparer<Asset>
    {
        public int Compare(Asset a, Asset b)
        { 
            return a.Order.CompareTo(b.Order); 
        }
    }

    public class Pair<T1, T2>
    {
        public T1 First { get; set; }
        public T2 Second { get; set; }
    }
}
