using System;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;

namespace MultiThreading3
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int barberShopCapacity = 5, hairCutTime = 20, clientsRefreshRate = 3,
            experimentDurationTime = 100;
            double criticalProbability = 0.2;
            Stopwatch time = new Stopwatch();
            BarberShop barbSh = new BarberShop(barberShopCapacity, hairCutTime, clientsRefreshRate, criticalProbability);
            Thread thread1 = new Thread(barbSh.BarberBehavior), thread2 = new Thread(barbSh.RefreshClients);
            thread1.Start();
            thread2.Start();
            time.Start();
            while (time.ElapsedMilliseconds < experimentDurationTime * 1E+3){}
            thread1.Abort();
            thread2.Abort();
        }
    }

    class BarberShop
    {
        int barberShopCapacity, hairCutTime, clientsRefreshRate;
        double criticalProbability;
        Queue<bool> barberShopQueue;
        Stopwatch time, refreshClientsTimer;
        Mutex mtx;
        Random rand;

        public BarberShop(int barberShopCapacity, int hairCutTime, int clientsRefreshRate, double criticalProbability)
        {
            this.barberShopCapacity = barberShopCapacity;
            this.hairCutTime = hairCutTime;
            this.clientsRefreshRate = clientsRefreshRate;
            this.criticalProbability = criticalProbability;
            barberShopQueue = new Queue<bool>();
            time = new Stopwatch();
            refreshClientsTimer = new Stopwatch();
            mtx = new Mutex();
            rand = new Random();
        }

        public void RefreshClients()
        {
            int counter = 0;
            double tmp = 0;
            refreshClientsTimer.Start();
            while (true)
            {
                if (refreshClientsTimer.ElapsedMilliseconds > counter * clientsRefreshRate * 1E+3)
                {
                    counter++;
                    tmp = rand.NextDouble();
                    Console.WriteLine("Client is thinking about a visit to the barbershop");
                    mtx.WaitOne();
                    if (barberShopQueue.Count < barberShopCapacity && tmp > criticalProbability)
                    {
                        Console.WriteLine("New client entered the barbershop");
                        barberShopQueue.Enqueue(true);
                    }
                    else if (tmp <= criticalProbability)
                        Console.WriteLine("Client has made a decision, that he doesn't need a new haircut");
                    else
                        Console.WriteLine("Barbershop is owerflowed");
                    mtx.ReleaseMutex();
                }
            }
        }

        public void BarberBehavior()
        {
            while (true)
            {
                Console.WriteLine("Barber is waiting for clients");
                while (barberShopQueue.Count == 0){}
                Console.WriteLine("Barber is waiting for closing the door");
                mtx.WaitOne();
                if (barberShopQueue.Count != 0)
                {
                    Console.WriteLine("Barber has taken new client");
                    barberShopQueue.Dequeue();
                }
                mtx.ReleaseMutex();
                time.Start();
                while (time.ElapsedMilliseconds < hairCutTime * 1E+3) { }
                time.Reset();
                Console.WriteLine("Barber has finished");
            }
        }
    }
}
