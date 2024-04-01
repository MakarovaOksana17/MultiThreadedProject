using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MultiThreadedProject
{
    public class CalculatingSumOfArrayElements
    {
        int cycleСounter;
        Random random;
        Stopwatch stopwatch;
        int[] mainArray;
        long sum;
        int thread;
        Task<int>[]? taskArray;

        public CalculatingSumOfArrayElements(int count)
        {
            cycleСounter = count;
            random = new Random();
            stopwatch = new Stopwatch();
            mainArray = new int[count];
            thread = Environment.ProcessorCount;
            for (int i = 0; i < cycleСounter; i++)
            {
                mainArray[i] = random.Next(1, 100);
            }
        }

        public string Regular()
        {
            stopwatch.Restart();
            sum = mainArray.Sum();
            stopwatch.Stop();
            return "Обычное |" + ToString();
        }

        public string ParalleLINQ()
        {
            stopwatch.Restart();
            sum = mainArray.AsParallel().WithDegreeOfParallelism(thread).Sum();
            stopwatch.Stop();
            
            return "LINQ    |" + ToString();
        }

        public string ParallelWithTask()
        {
            stopwatch.Restart();
            IEnumerable<int[]> chanks = mainArray.Chunk(mainArray.Length / thread);
            taskArray = new Task<int>[chanks.Count()];
            int i = 0;
            foreach (int[] chank in chanks)
            {
                taskArray[i] = Task.Run(() => chank.Sum());
                ++i;
            }
            Task.WaitAll(taskArray);
            sum = taskArray.Sum(t => t.Result);
            stopwatch.Stop();
            return "Thread  |" + ToString();
        }        

        public override string ToString()
        {
           return $"Цикл: {cycleСounter} | Сумма: {sum} | Время {stopwatch.ElapsedMilliseconds} мс.";
        }
    }
}
