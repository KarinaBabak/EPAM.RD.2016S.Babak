using System;


namespace Monitor
{
    using System.Threading;
    // TODO: Use Monitor (not lock) to protect this structure.
    public class MyClass
    {
        private int _value;
        private object locker = new object();

        public int Counter
        {           
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        
        public void Increase()
        {
            Monitor.Enter(locker);
            try
            {
                _value++;
            }
            finally
            {
                Monitor.Exit(locker);
            }
        }

        public void Decrease()
        {
            Monitor.Enter(locker);
            try
            {
                _value--;
            }
            finally
            {
                Monitor.Exit(locker);
            }
        }
    }
}
