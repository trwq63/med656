using UAHFitVault.LogicLayer.Resources;

namespace UAHFitVault.LogicLayer.LogicFiles
{
    /// <summary>
    /// Class is used for executing logical functions against Zephyr data.
    /// </summary>
    public static class ZephyrLogic
    {
        #region Public Methods

        /// <summary>
        /// Convert a Zephyr Accelerometer data point from bits to G's.
        /// </summary>
        /// <param name="accelDataPoint">Accelerometer data point in bits.</param>
        /// <returns></returns>
        public static float? ConvertAccelWaveformToGs(int accelDataPoint) {
            //G's
            float? gs = null;
            if(accelDataPoint >= ZephyrConstants.ACCEL_MIN_VALUE && accelDataPoint <= ZephyrConstants.ACCEL_MAX_VALUE) {
                gs = (float)(accelDataPoint - ZephyrConstants.ACCEL_0G) / ZephyrConstants.ACCEL_1G_COUNTS;
            }

            return gs;
        }

        #endregion
    }
}
