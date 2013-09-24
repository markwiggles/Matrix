using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;


namespace MMexJava {
    class Program {
        static void Main(string[] args) {

            int numThreads = 4;
            int increment = MyThread.M_SIZE/numThreads;

            Random rand = new Random();
            Stopwatch timer = new Stopwatch();

            Console.WriteLine("Start Matrix Multiply....");

            //create random values 
            for (int i = 0; i < MyThread.M_SIZE; i++) {
                for (int j = 0; j < MyThread.M_SIZE; j++) {
                    MyThread.A[i, j] = (float)rand.Next(0, 1);
                    MyThread.B[i, j] = (float)rand.Next(0, 1);
                }
            }

            //start timing
            timer.Start();

            //create array for threads
            MyThread[] threads = new MyThread[numThreads];

            //create threads in array, pass increment for start/end, and commence work
            for (int i = 0; i < numThreads; i++) {
                threads[i] = new MyThread(i * increment, increment);
                threads[i].Start();
            }

            //join threads to main thread
            for (int i = 0; i < numThreads; i++) {
                try {
                    threads[i].Join();
                } catch (Exception e) {
                   Console.WriteLine(e);
                }
            }

            //calculate and display time
            long duration = timer.ElapsedMilliseconds;
            Console.WriteLine("Finished Matrix Multiply: " + duration + " milliseconds");
            Console.ReadKey();
        }
    }
}
