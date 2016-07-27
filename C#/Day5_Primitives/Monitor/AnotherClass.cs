using System.Threading;

namespace Monitor
{
    // TODO: Use SpinLock to protect this structure.
    public class AnotherClass
    {
        private static SpinLock logLock = new SpinLock();

        private int _value;

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
            bool lockTaken = false;
            logLock.Enter(ref lockTaken);

            if (lockTaken)
            {
                try
                {
                    if (!logLock.IsHeldByCurrentThread)
                        _value++;
                }
                finally
                {
                    logLock.Exit();
                }
            }
        }

        public void Decrease()
        {
            bool lockTaken = false;
            logLock.Enter(ref lockTaken);
            if (lockTaken)
            {
                try
                {
                    if (!logLock.IsHeldByCurrentThread)
                        _value--;
                }
                finally
                {
                    logLock.Exit();
                }
            }
        }
    }
}
