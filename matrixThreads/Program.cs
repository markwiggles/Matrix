using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace matrixThreads {
    class Program {


        //some class constants and variables
        static int M_SIZE = 256;
        static int NUM_THREADS = 4;
        static int increment = M_SIZE / NUM_THREADS;
        static int start = 0;

        static float[,] A = new float[M_SIZE, M_SIZE];
        static float[,] B = new float[M_SIZE, M_SIZE];
        static float[,] C = new float[M_SIZE, M_SIZE];


        static void Main(string[] args) {

            //Q
            QueryPerfCounter qp = new QueryPerfCounter();
            qp.Start();

            //create the stopwatch timer
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //do the work



            Thread t1 = new Thread(() => MultiplyMatrix(0, 64));
            t1.Start();
            Thread t2 = new Thread(() => MultiplyMatrix(65, 128));
            t2.Start();
            Thread t3 = new Thread(() => MultiplyMatrix(129, 192));
            t3.Start();
            Thread t4 = new Thread(() => MultiplyMatrix(193, 256));
            t4.Start();

            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();


            sw.Stop();

            qp.Stop();

            //assign time and result
            long result = sw.ElapsedMilliseconds;

            //long result = Convert.ToInt64(qpf.ElapsedTime());

            WriteResults(result);


        }//end main

        /*
         * Method to output results
         */
        private static void WriteResults(long result) {

            string resultString = Convert.ToString(M_SIZE) + "," + Convert.ToString(result);

            String filePath = @"results.csv";

            if (File.Exists(filePath)) {
                using (StreamWriter sWriter = File.AppendText(filePath)) {
                    sWriter.WriteLine(resultString);
                }
            }

            Console.WriteLine("Time result (ms) is: {0}", result);
            Console.ReadKey();
        }

        /*
         * Method to do the arithmetic
         */
        static void MultiplyMatrix(int start, int end) {          

            int i, j, k;

            for (i = start; i < end; i++) {

                for (j = start; j < end; j++) {

                    C[i, j] = 0.0f;

                    for (k = start; k < end; k++) {

                        C[i, j] += A[i, k] * B[j, k];

                    }
                }
            }
        }


    }//end class
}//end namespace


/* QUESTIONS
 * 
 * why use a delegate?
 * 
 * naming?
 * 
 * joining?
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */


