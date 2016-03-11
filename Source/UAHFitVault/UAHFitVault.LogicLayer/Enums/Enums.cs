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
        Microsoft_Band
    }

    /// <summary>
    /// Enum defines the different data file types supported by the system.
    /// </summary>
    public enum File_Type
    {
        Unknown,
        Summary,
        EventData,
        Accelerometer,
        ECG,
        Breathing
    }

    /// <summary>
    /// Details the different user types available in the system.
    /// </summary>
    public enum UserRole
    {
        None,
        Patient,
        Physician,
        Experiment_Administrator,
        System_Administrator
    };

    /// <summary>
    /// Details the different types of account statuses in the system.
    /// </summary>
    public enum Account_Status
    {
        Pending = 0,
        Active = 1,
        Inactive = 2
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

    /// <summary>
    /// Different Zephyr Data types
    /// </summary>
    public enum ZephyrDataTypes
    {
        Accelerometer,
        Breathing_Waveforms,
        ECG_Waveforms,
        Event_Data,
        Summary
    }

    /// <summary>
    /// The various information found in a Zephyr summary report.
    /// </summary>
    public enum ZephyrSummaryDataTypes
    {
        Heart_Rate,
        Breathing_Rate,
        Skin_Temperature,
        Posture,
        Activity,
        Peak_Accelerometer,
        Battery_Volts,
        Battery_Level,
        Breathing_Rate_Amplitude,
        Breating_Rate_Confidence,
        Breating_Rate_Noise,
        ECG_Amplitude,
        ECG_Noise,
        Heart_Rate_Confidence,
        HRV,
        System_Confidence,
        Galvanic_Skin_Resistance,
        ROG_State,
        ROG_Time,
        Accel_Vertical_Min,
        Accel_Vertical_Max,
        Accel_Lateral_Min,
        Accel_Lateral_Max,
        Accel_Sagittal_Min,
        Accel_Sagittal_Max,
        Device_Temperature,
        Status_Information,
        Link_Quality,
        RSSI,
        TxPower,
        Core_Temperature
    }
    
    /// <summary>
    /// Details the activities that a data set will be tagged with
    /// </summary>
    public enum ActivityType
    {
        Home_Cooking = 0,
        Home_Doing_Housework = 1,
        Home_Eating = 2,
        Home_Watching_TV = 3,
        Home_Napping = 4,
        Home_Sleeping = 5,
        Home_Sitting = 6,
        Home_Gardening = 7,
        Driving = 8,
        Using_Transportation = 9,
        Shopping = 10,
        Office_Work = 11,
        Workplace_Activities = 12,
        Walking = 13,
        Running = 14,
        Climbing_Stairs = 15,
        Low_Intensity_Exercise = 16,
        Medium_Intensity_Exercise = 17,
        High_Intensity_Exercise = 18,
        Playing_Sports = 19,
        Swimming = 20,
        Hiking = 21,
        Biking = 22,
        Miscellaneous = 23
    }
    /// <summary>
    /// Different BasisPeak Data types
    /// </summary>
    public enum BasisPeakTypes
    {
        Calories,
        Galvanic_Skin_Resistance,
        Heart_Rate,
        Skin_Temperature,
        Steps
    };
    
}
