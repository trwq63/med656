using System.Collections.Generic;
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
        /// <param name="accelDataPoint">Accelerometer data points in bits.</param>
        /// <returns></returns>
        public static List<double> ConvertAccelWaveformToGs(List<int> accelDataPoints) {
            //G's
            List<double> gs = new List<double>();
            if (accelDataPoints != null && accelDataPoints.Count > 0) {
                foreach (int accelDataPoint in accelDataPoints) {
                    if (accelDataPoint >= ZephyrConstants.ACCEL_MIN_VALUE && accelDataPoint <= ZephyrConstants.ACCEL_MAX_VALUE) {
                        gs.Add((double)(accelDataPoint - ZephyrConstants.ACCEL_0G) / ZephyrConstants.ACCEL_1G_COUNTS);                        
                    }
                }
            }
            return gs;
        }

        #endregion
    }
}
