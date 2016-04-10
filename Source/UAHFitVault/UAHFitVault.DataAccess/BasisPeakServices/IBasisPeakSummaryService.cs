using System;
using System.Collections.Generic;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.DataAccess.BasisPeakServices
{
    /// <summary>
    /// Interface to implement the basis peak summary service
    /// </summary>
    public interface IBasisPeakSummaryService
    {
        /// <summary>
        /// Add a new BasisPeak Summary data record to the database
        /// </summary>
        /// <param name="BasisPeakSummary">BasisPeakSummaryData object to add to the database</param>
        void CreateBasisPeakSummary(BasisPeakSummaryData BasisPeakSummary);

        /// <summary>
        /// Get the BasisPeak Summary data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the BasisPeak Summary Data records</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        IEnumerable<BasisPeakSummaryData> GetBasisPeakSummaryData(PatientData patientData, int skip = 0, int take = 0);

        /// <summary>
        /// Get the BasisPeak Summary data for the given a patient data record or all records for all patients.
        /// </summary>
        /// <param name="patientData">PatientData object used to retrieve the BasisPeak Summary Data records</param>
        /// <param name="startTime">Start time of date/time filter</param>
        /// <param name="endTime">End time of date/time filter</param>
        /// <param name="skip">Skip a number of records in the data collection</param>
        /// <param name="take">Number of records to return.</param>
        /// <returns></returns>
        IEnumerable<BasisPeakSummaryData> GetBasisPeakSummaryData(PatientData patientData, DateTime startTime, DateTime endTime, int skip = 0, int take = 0);


        /// <summary>
        /// Get BasisPeak Summary data from database using the BasisPeak Summary id
        /// </summary>
        /// <param name="id">Id of the BasisPeak Summary data</param>
        /// <returns></returns>
        BasisPeakSummaryData GetBasisPeakSummaryData(int id);

        /// <summary>
        /// Bulk Insert BasisPeak Summary Data into the database
        /// </summary>
        /// <param name="basisPeakSummary">Collection of Zephyr summary data to insert into database.</param>
        void BulkInsert(List<BasisPeakSummaryData> basisPeakSummary);

        /// <summary>
        /// Save changes to database
        /// </summary>
        void SaveChanges();
    }
}