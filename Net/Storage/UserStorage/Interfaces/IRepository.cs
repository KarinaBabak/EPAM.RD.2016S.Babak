using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.Interfaces
{    
    /// <summary>
    /// Base functionality for repository
    /// </summary>
    /// <typeparam name="T">class for working</typeparam>
    public interface IRepository<T> where T : class
    {        
        /// <summary>
        /// Adding a new entity to repository
        /// </summary>
        /// <param name="entity">entity of class</param>
        /// <returns>id of entity</returns>
        int Add(T entity);

        /// <summary>
        /// Removing entity
        /// </summary>
        /// <param name="entity">entity for removing</param>
        void Delete(T entity);

        /// <summary>
        /// Load entities from xml file
        /// </summary>
        void ReadFromXML();

        /// <summary>
        /// Saves entities to xml file
        /// </summary>
        void WriteToXML();
    }
}
