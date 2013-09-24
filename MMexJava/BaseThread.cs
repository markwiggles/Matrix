using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MMexJava {
    public abstract class BaseThread {

        private Thread _thread;

        public BaseThread() {

            _thread = new Thread(new ThreadStart(this.RunThread));
        }


        //thread methods/properties
        public void Start() {
            _thread.Start();
        }
        public void Join() {
            _thread.Join();
        }
        public bool IsAlive {
            get {
                return _thread.IsAlive;
            }
        }

        //override in base class
        public abstract void RunThread();
    }
}
