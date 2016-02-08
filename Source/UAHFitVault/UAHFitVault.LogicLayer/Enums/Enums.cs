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

}
