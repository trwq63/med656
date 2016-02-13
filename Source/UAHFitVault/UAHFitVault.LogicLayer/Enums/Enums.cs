namespace UAHFitVault.LogicLayer.Enums
{
    /// <summary>
    /// Enum defines the different medical device types supported by the system.
    /// </summary>
    public enum Device_Type
    {
        Unknown,
        Zephyr,
        BasisPeak,
        MicrosoftBand
    }

    /// <summary>
    /// Enum defines the different data file types supported by the system.
    /// </summary>
    public enum File_Type
    {
        Unknown,
        Summary,
        EventData,
        Accel,
        ECG,
        Breathing
    }

    /// <summary>
    /// Details the different user types available in the system.
    /// </summary>
    public enum UserRole {
        Patient,
        Physician,
        ExperimentAdmin,
        SystemAdmin
    };

    /// <summary>
    /// Details the possible values for patient Race
    /// </summary>
    public enum PatientRace
    {
        AmericanIndian = 0,
        Asian = 1,
        Black = 2,
        Hawaiian = 3,
        White = 4,
        Other = 5
    };

    /// <summary>
    /// Details the possible values for patient gender
    /// </summary>
    public enum PatientGender
    {
        Male = 0,
        Female = 1
    }

    /// <summary>
    /// Details the possible values for patient ethnicity
    /// </summary>
    public enum PatientEthnicity
    {
        NonHispanic = 0,
        Hispanic = 1
    }

    /// <summary>
    /// Class for storing conversion functions from enums to other data types.
    /// </summary>
    public class Conversions
    {
        /// <summary>
        /// Convert the patient ethnicity from a string to an int
        /// </summary>
        /// <param name="ethnicity">Input ethnicity</param>
        /// <returns>An integer corresponding to the patient's ethnicity</returns>
        public int PatientEthnicityStringToInt (string ethnicity)
        {
            if (ethnicity == "Non-Hispanic")
            {
                return (int)PatientEthnicity.NonHispanic;
            }
            return (int)PatientEthnicity.Hispanic;
        }

        /// <summary>
        /// Convert the patient gender from a string to an int
        /// </summary>
        /// <param name="gender">Input gender</param>
        /// <returns>An integer corresponding to the patient's gender</returns>
        public int PatientGenderStringToInt(string gender)
        {
            if (gender == "Male")
            {
                return (int) PatientGender.Male;
            }
            return (int) PatientGender.Female;
        }

        /// <summary>
        /// Convert the patient race from a string to an int
        /// </summary>
        /// <param name="race">Input race</param>
        /// <returns>An integer corresponding to the patient's race</returns>
        public int GenderRaceStringToInt (string race)
        {
            if (race == "American Indian")
            {
                return (int)PatientRace.AmericanIndian;
            }
            else if (race == "Asian")
            {
                return (int)PatientRace.Asian;
            }
            else if (race == "Black")
            {
                return (int)PatientRace.Black;
            }
            else if (race == "Hawaiian")
            {
                return (int)PatientRace.Hawaiian;
            }
            else if (race == "White")
            {
                return (int)PatientRace.White;
            }

            return (int) PatientRace.Other;
        }
    }

    /// <summary>
    /// Details the possible values for patient, physician and experiment administrator location
    /// </summary>
    public enum Location
    {
        // May not be used
        Alabama = 0,
        Alaska = 1,
        AmericanSamoa = 2,
        Arizona = 3,
        Arkansas = 4,
        California = 5,
        Colorado = 6,
        Connecticut = 7,
        DistrictOfColumbia = 8,
        Delaware = 9,
        Florida = 10,
        Georgia = 11,
        Guam = 12,
        Hawaii = 13,
        Idaho = 14,
        Illinois = 15,
        Indiana = 16,
        Iowa = 17,
        Kansas = 18,
        Kentucky = 19,
        Louisiana = 20,
        Maine = 21,
        Maryland = 22,
        Massachusetts = 23,
        Michigan = 24,
        Minnesota = 25,
        Mississippi = 26,
        Missouri = 27,
        Montana = 28,
        Nebraska = 29,
        Nevada = 30,
        NewHampshire = 31,
        NewJersey = 32,
        NewMexico = 33,
        NewYork = 34,
        NorthCarolina = 35,
        NorthMarianasIslands = 36,
        NorthDakota = 37,
        Ohio = 38,
        Oklahoma = 39,
        Oregon = 40,
        Pennsylvania = 41,
        PuertoRico = 42,
        RhodeIsland = 43,
        SouthCarolina = 44,
        SouthDakota = 45,
        Tennessee = 46,
        Texas = 47,
        Utah = 48,
        Vermont = 49,
        Virginia = 50,
        VirginIslands = 51,
        Washington = 52,
        WestVirginia = 53,
        Wisconsin = 54,
        Wyoming = 55,
        EuropeanUnion = 56,
        EasternEurope = 57,
        Africa = 58,
        SouthAmerica = 59,
        CentralAmerica = 60,
        Asia = 61,
        Oceania = 62,
        NorthAmerica = 63,
        Caribbean = 64
    }
    
}
