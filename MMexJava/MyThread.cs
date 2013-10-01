using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MMexJava {
    
    public class MyThread : BaseThread {

        //class constants and variables
        public static int M_SIZE = 1000;
        private int start;
        private int end;
        public static float[,] A = new float[M_SIZE, M_SIZE];
        public static float[,] B = new float[M_SIZE, M_SIZE];
        public static float[,] C = new float[M_SIZE, M_SIZE];

        //constructor
        public MyThread(int _start, int _end) {
            this.start = _start;
            this.end = _end;
        }

        //main work done here in overriden method
        public override void RunThread() {

            for (int i = start; i < (start + end); i++) {
                for (int j = 0; j < M_SIZE; j++) {
                    for (int k = 0; k < M_SIZE; k++) {
                        C[i, j] = A[i, k] * B[k, j];
                    }
                }
            }
            Console.WriteLine("Finished running thread from: " + start + " to " + (start + end)); 
 
            //test  
        }
    }
}
