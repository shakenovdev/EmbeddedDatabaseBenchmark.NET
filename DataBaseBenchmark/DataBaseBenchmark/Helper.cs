using System;
using System.Linq;
using Foundation.Core;

namespace Foundation
{
    public static class Helper
    {
        private static readonly Random Random = new Random();

        public static readonly string TABLE_NAME = "dataunits";

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public static int RandomInteger(int max)
        {
            return Random.Next(0, max);
        }

        public static void Shuffle<T>(T[] array)
        {
            var n = array.Length;
            while (n > 1)
            {
                var k = Random.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }
}