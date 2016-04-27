namespace UAHFitVault.LogicLayer.Resources
{
    /// <summary>
    /// Class is used to define constant values for Zephyr waveform data conversions
    /// </summary>
    public static class ZephyrConstants
    {
        /// <summary>
        /// Maximum value a waveform can represent in Zephyr accelerometer waveform.
        /// </summary>
        public const int ACCEL_MAX_VALUE = 4095;

        /// <summary>
        /// Minimum value a waveform can represent in Zephyr accelerometer waveform.
        /// </summary>
        public const int ACCEL_MIN_VALUE = 0;

        /// <summary>
        /// Zephyr waveform data ranges from 0 - 4095 where the median value
        /// 2048 represents 0Gs.  The accelerometer is rated at +- 16G.
        /// </summary>
        public const int ACCEL_0G = 2048;

        /// <summary>
        /// 1G represents 83 bits on the accelermeter scale.
        /// </summary>
        public const int ACCEL_1G_COUNTS = 83;

        /// <summary>
        /// Maxiumum possible G value out from the accelerometer
        /// </summary>
        public const int ACCEL_MAX_VALUE_G = 16;

        /// <summary>
        /// Minimum possible G value out from the accelerometer
        /// </summary>
        public const int ACCEL_MIN_VALUE_G = -16;


    }
}
