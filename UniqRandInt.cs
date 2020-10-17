using System;
using System.Collections.Generic;

namespace BushidoBurrito.Planarity
{
    /// <summary>
    /// Locally unique random integers.
    /// </summary>
    public class UniqRandInt
    {
        public const int MaxRetries = 1000;

        private Random _Random;

        private HashSet<int> _UsedValues = new HashSet<int>();

        public UniqRandInt(int seed)
        {
            _Random = new Random(seed);
        }

        public UniqRandInt(Random random)
        {
            _Random = random;
        }

        public int Next(int minValue, int maxValue)
        {
            int result = _Random.Next(minValue, maxValue);

            int loopCount = 0;

            while (_UsedValues.Contains(result))
            {
                result = _Random.Next(minValue, maxValue);
                loopCount += 1;

                if (loopCount > MaxRetries)
                {
                    return result;
                }
            }

            _UsedValues.Add(result);
            return result;
        }
    }
}
