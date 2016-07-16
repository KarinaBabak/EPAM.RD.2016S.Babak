using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Observer
{
    public interface IObservable
    {
        /// <summary>
        /// Determination of adding  observer
        /// </summary>
        /// <param name="o">observer</param>
        void RegisterObserver(IObserver o);
        /// <summary>
        /// Determination of removing  observer
        /// </summary>
        /// <param name="o">observer</param>
        void RemoveObserver(IObserver o);
        /// <summary>
        /// Determination of notifying  observer
        /// </summary>        
        void NotifyObservers();
    }
}
