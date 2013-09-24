using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace matrixMultiply {
    class Program {
        static void Main(string[] args) {


            Random rn = new Random();
            Stopwatch stopW = new Stopwatch();


            Console.WriteLine("Start Matrix Multiply...");

            for (int i = 0; i < 1000; i++) {

                for (int j = 0; j < 1000; j++) {
                    MyThread.A[i, j] = (float)rn.Next(0, 1);
                    MyThread.B[i, j] = (float)rn.Next(0, 1);
                }
            }

            //start timing
            stopW.Start();

            //create and array for the threads
            MyThread[] threads = new MyThread[4];

            //Thread t1 = new Thread(() => MultiplyMatrix(0, 64));
            //t1.Start();

            for (int i = 0; i < 4; i++) {
                threads[i] = new MyThread(i * 250, 250);
                threads[i].Start();
            }

            for (int i = 0; i < 4; i++) {
                try {
                    threads[i].Join();
                } catch (Exception e) {
                }
            }

            stopW.Stop();
            long duration = stopW.ElapsedMilliseconds;

            Console.WriteLine("Finished Matrix Multiply: " + (float)duration / 1000000000.0);
        }//end main



        class MyThread {

            public static float[,] A = new float[1000, 1000];
            public static float[,] B = new float[1000, 1000];
            public static float[,] C = new float[1000, 1000];

            private static int start;
            private static int end;

            public MyThread(int _start, int _end) {
                start = _start;
                end = _end;
            }

            // This is the entry point for the second thread.
            public static void MyStart() {

                for (int i = start; i < (start + end); i++) {
                    for (int j = 0; j < 1000; j++) {
                        for (int k = 0; k < 1000; k++) {
                            C[i, j] = A[i, k] * B[k, j];
                        }
                    }
                }

                Console.WriteLine("Finished running thread from:" + start + " to:"
                        + (start + end));
            }





        }

    }

}//end namespace





