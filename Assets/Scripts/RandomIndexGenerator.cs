using System.Collections.Generic;
using UnityEngine;


namespace CustomRandomGenExtension
{
    public static class RandomIndexGenerator
    {
        private static int lastIndex = 0;

        public static int GetRandomIndex<T>(this List<T> list)
        {

            int randomIndex = lastIndex;

            while (randomIndex == lastIndex)
            {
                randomIndex = Random.Range(0, list.Count);
            }

            lastIndex = randomIndex;

            return randomIndex;
        }

    }
}