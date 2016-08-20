using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage
{
    /// <summary>
    /// Base functionality for validation
    /// </summary>
    /// <typeparam name="T">object of validation</typeparam>
    public interface IValidator<T>
    {
        /// <summary>
        /// Check whether entity is valid
        /// </summary>
        /// <param name="entity">entity for validation</param>
        /// <returns>true if the entity is valid</returns>
        bool Validate(T entity);
    }
}
