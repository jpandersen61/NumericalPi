using System;
using System.Threading;
using System.Threading.Tasks;

namespace NumericalPi
{
    public class PiCalc
    {
        /// <summary>
        /// Executes the calculation of an 
        /// approximate value of pi.
        /// </summary>
        /// <param name="iterations">Number of iterations to perform</param>
        /// <returns>Approximate value of pi</returns>

        const int numberOfTasks = 4 ;
        public double Calculate(int iterations)
        {
            Task<int> task1 = IterateAsync(iterations/numberOfTasks);
            Task<int> task2 = IterateAsync(iterations/numberOfTasks);
            Task<int> task3 = IterateAsync(iterations/numberOfTasks);
            Task<int> task4 = IterateAsync(iterations/numberOfTasks);

            Task.WaitAll(task1, task2, task3, task4);

            int insideUnitCircle = task1.Result + task2.Result + task3.Result + task4.Result;
            return insideUnitCircle * 4.0 / iterations;
        }

        /// <summary>
        /// Perform a number of "dart-throwing" simulations.
        /// </summary>
        /// <param name="iterations">Number of dart-throws to perform</param>
        /// <returns>Number of throws within the unit circle</returns>
        /// 

        private Task<int> IterateAsync(int iterations)
        {
            Task<int> t = Task<int>.Run(() => Iterate(iterations));
            return t;
        }

        public int Iterate(int iterations)
        {
            Random _generator = new Random(Guid.NewGuid().GetHashCode());
            int insideUnitCircle = 0;

            for (int i = 0; i < iterations; i++)
            {
                double x = _generator.NextDouble();
                double y = _generator.NextDouble();

                if (x * x + y * y < 1.0)
                {
                    insideUnitCircle++;
                }
            }

            return insideUnitCircle;
        }
    }
}