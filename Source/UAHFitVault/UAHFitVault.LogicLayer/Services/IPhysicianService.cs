using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.LogicLayer.Services
{
    /// <summary>
    /// Interface for the physician service
    /// </summary>
    public interface IPhysicianService
    {
        /// <summary>
        /// Add a new physician to the database
        /// </summary>
        /// <param name="physician">Physician object to add to the database</param>
        void CreatePhysician(Physician physician);

        /// <summary>
        /// Get Physician from database using Physician Id
        /// </summary>
        /// <param name="id">Id of the physician</param>
        /// <returns></returns>
        Physician GetPhysician(int id);

        /// <summary>
        /// Get Physician from database using Physician Id
        /// </summary>
        /// <param name="firstName">First name of the physician</param>
        /// <param name="lastName">Last name of the physician</param>
        /// <returns></returns>
        Physician GetPhysician(string firstName, string lastName);

        /// <summary>
        /// Get all physicians from the database
        /// </summary>
        /// <param name="lastName">Optional parameter to get all physicians with a specific name</param>
        /// <returns></returns>
        IEnumerable<Physician> GetPhysicians(string lastName = null);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveCategory();
    }
}