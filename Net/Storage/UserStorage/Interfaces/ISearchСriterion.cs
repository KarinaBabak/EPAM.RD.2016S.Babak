using System;
using System.Collections.Generic;

namespace UserStorage.Interfaces
{
    /// <summary>
    /// Interface for search criterion
    /// </summary>
    /// <typeparam name="T">type of criterion</typeparam>
    public interface ISearchСriterion<T>
    {
        /// <summary>
        /// Match criterion with entity property
        /// </summary>
        /// <param name="entity">entity for search</param>
        /// <returns>true if match</returns>
        bool MatchByCriterion(T entity);
    }
}
