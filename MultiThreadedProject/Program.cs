using MultiThreadedProject;

class Program
{
    static void Main(string[] args)
    {
        CalculatingSumOfArrayElements calculatingSum;
        List<int> run = new List<int>() { 100000, 1000000, 10000000 };
        foreach (int i in run)
        {
            calculatingSum = new CalculatingSumOfArrayElements(i);
            Console.WriteLine(calculatingSum.Regular());
            Console.WriteLine(calculatingSum.ParalleLINQ());
            Console.WriteLine(calculatingSum.ParallelWithTask());
        }

    }
}