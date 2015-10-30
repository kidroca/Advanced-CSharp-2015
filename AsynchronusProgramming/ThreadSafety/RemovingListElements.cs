namespace ThreadSafety
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    class RemovingListElements
    {
        static List<int> numbers;

        static void Main()
        {
            numbers = Enumerable.Range(0, 10000).ToList();

            var threads = new Thread[Environment.ProcessorCount];

            for (int i = 0; i < threads.Length; i++)
            {
                Thread thread = new Thread(() => RemoveAllElements());

                threads[i] = thread;
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }

        static void RemoveAllElements()
        {
            while (numbers.Count > 0)
            {
                lock (numbers)
                {
                    if (numbers.Count <= 0)
                    {
                        break;
                    }
                    
                    int lastIndex = numbers.Count - 1;
                    numbers.RemoveAt(lastIndex);
                }
            }
        }
    }
}
