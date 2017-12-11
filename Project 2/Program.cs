using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

namespace MultiThreading2
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            bool end = false;
            Stopwatch timer = new Stopwatch();
            int philosopherNumber = 5, necessaryEating = int.MaxValue, eatingTime = 4;
            Philosopher[] phArr = new Philosopher[philosopherNumber];
            Fork[] forkArr = new Fork[philosopherNumber], tmpForkArr = new Fork[2];
            Thread[] threadArr = new Thread[philosopherNumber];
            for (int i = 0; i < philosopherNumber; i++)
            {
                phArr[i] = new Philosopher(necessaryEating, eatingTime, "Ph #" + Convert.ToString(i + 1));
                forkArr[i] = new Fork();
                threadArr[i] = new Thread(phArr[i].EatStrategy);
            }
            for (int i = 0; i < philosopherNumber; i++)
            {
                tmpForkArr[0] = forkArr[i];
                tmpForkArr[1] = i == philosopherNumber - 1 ? forkArr[0] : forkArr[i + 1];
                threadArr[i].Start(tmpForkArr);
            }
            timer.Start();
            for (int i = 0; !end;)
            {
                if(timer.ElapsedMilliseconds > i * 1E+4)
                {
                    end = phArr[0].NumberOfEating == phArr[0].NecessaryNumberOfEating;
                    for (int j = 0; j < philosopherNumber; j++)
                    {
                        Console.WriteLine("Philosopher #{0}. Downtime: {1}. Number of eating time: {2}.\nL. f.: {3}. R. f.: {4}",
                                          j + 1, phArr[j].Downtime, phArr[j].NumberOfEating, 
                                          phArr[j].isLeftForkInUse, phArr[j].isRightForkInUse);
                        end = end && phArr[j].NumberOfEating == phArr[j].NecessaryNumberOfEating;
                    }
                    Console.WriteLine("\n");
                    i++;
                }
            }
            for (int i = 0; i < philosopherNumber; i++)
                threadArr[i].Abort();
            timer.Reset();
        }
    }
}
