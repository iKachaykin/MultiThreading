using System;
using System.Diagnostics;

namespace MultiThreading2
{
    public class Philosopher : ICloneable
    {
        private string name;
        public bool isLeftForkInUse, isRightForkInUse;

        public int NecessaryNumberOfEating { get; set; }
        public long EatingTime { get; set; }
        public long Downtime { get; private set; }
        public int NumberOfEating { get; private set; }

        public Philosopher(int NecessaryNumberOfEating = 10, long EatingTime = 0, string name = "ph")
        {
            this.EatingTime = EatingTime * 1000;
            this.NecessaryNumberOfEating = NecessaryNumberOfEating;
            this.name = name;
            NumberOfEating = 0;
            Downtime = 0;
            isLeftForkInUse = isRightForkInUse = false;
        }

        public Philosopher(Philosopher other)
        {
            name = other.name;
            NumberOfEating = other.NumberOfEating;
            Downtime = other.Downtime;
            isLeftForkInUse = other.isLeftForkInUse;
            isRightForkInUse = other.isRightForkInUse;
            NecessaryNumberOfEating = other.NecessaryNumberOfEating;
            EatingTime = other.EatingTime;
        }

        public object Clone() => new Philosopher(this);

        public bool TakeFork(Fork fork)
        {
            if (fork.IsInUse)
                return false;
            fork.IsInUse = true;
            return true;
        }

        public void ReturnFork(Fork fork)
        {
            fork.IsInUse = false;
        }

        public bool Eat()
        {
            if (isLeftForkInUse && isRightForkInUse)
                NumberOfEating++;
            else
                return false;
            Stopwatch eatingTimer = new Stopwatch();
            eatingTimer.Start();
            while (eatingTimer.ElapsedMilliseconds < EatingTime) {}
            eatingTimer.Reset();
            return true;
        }

        public void EatStrategy(object obj)
        {
            if (obj == null)
                throw new ArgumentException();
            Fork leftFork = ((Fork[])obj)[0], rightFork = ((Fork[])obj)[1];
            Stopwatch thinkingTimer = new Stopwatch();
            while (NumberOfEating < NecessaryNumberOfEating)
            {
                thinkingTimer.Start();
                //Console.WriteLine("Philosopher: {0}. Try to take left fork!", name);
                if (isLeftForkInUse || TakeFork(leftFork))
                {
                    isLeftForkInUse = true;
                    //Console.WriteLine("Philosopher: {0}. Success:)", name);
                }
                else
                {
                    //Console.WriteLine("Philosopher: {0}. Next time:(", name);
                    continue;
                }
                //Console.WriteLine("Philosopher: {0}. Try to take right fork!", name);
                if (isRightForkInUse || TakeFork(rightFork))
                {
                    isRightForkInUse = true;
                    //Console.WriteLine("Philosopher: {0}. Success:)", name);
                }
                else
                {
                    //Console.WriteLine("Philosopher: {0}. Next time:(", name);
                    continue;
                }
                thinkingTimer.Stop();
                Eat();
                ReturnFork(leftFork);
                isLeftForkInUse = false;
                ReturnFork(rightFork);
                isRightForkInUse = false;
                Downtime += thinkingTimer.ElapsedMilliseconds;
                thinkingTimer.Reset();
            }

        }

    }
}
