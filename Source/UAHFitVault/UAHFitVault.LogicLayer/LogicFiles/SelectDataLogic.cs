using UAHFitVault.LogicLayer.Enums;

namespace UAHFitVault.LogicLayer.LogicFiles
{
    /// <summary>
    /// Class contains the logic functions needed by the SelectLogicController
    /// </summary>
    public static class SelectDataLogic
    {
        #region Public Methods

        /// <summary>
        /// Determine what device the data came from by the filename.
        /// </summary>
        /// <param name="fileName">Name of the data file being processed</param>
        /// <returns></returns>
        public static Device_Type DetermineDeviceType(string fileName) {
            Device_Type deviceType = Device_Type.Unknown;

            if (!string.IsNullOrEmpty(fileName)) {
                if (fileName.Contains("bodymetrics")) {
                    deviceType = Device_Type.BasisPeak;
                }
                else if (fileName.Contains("Acceleration") || fileName.Contains("Calories") || fileName.Contains("Distance")
                        || fileName.Contains("Gyroscrope") || fileName.Contains("HeartRate") || fileName.Contains("Pedometer")
                        || fileName.Contains("Temperature") || fileName.Contains("UV")) {
                    deviceType = Device_Type.MicrosoftBand;
                }
                else {
                    deviceType = Device_Type.Zephyr;
                }
            }

            return deviceType;
        }

        /// <summary>
        /// Determine the type of data that is contained inside the file being processed.
        /// </summary>
        /// <param name="fileName">Name of the file being processed.</param>
        /// <param name="deviceType">The device type the file belongs to.</param>
        /// <returns></returns>
        public static File_Type DetermineFileType(string fileName, Device_Type deviceType) {
            File_Type fileType = File_Type.Unknown;

            if (!string.IsNullOrEmpty(fileName) && deviceType != Device_Type.Unknown) {
                switch (deviceType) {
                    case Device_Type.BasisPeak:
                        fileType = File_Type.Summary;
                        break;
                    case Device_Type.Zephyr:
                        if (fileName.Contains("Accel")) {
                            fileType = File_Type.Accel;
                        }
                        else if (fileName.Contains("Breathing")) {
                            fileType = File_Type.Breathing;
                        }
                        else if (fileName.Contains("ECG")) {
                            fileType = File_Type.ECG;
                        }
                        else if (fileName.Contains("Event")) {
                            fileType = File_Type.EventData;
                        }
                        else if (fileName.Contains("Summary")) {
                            fileType = File_Type.Summary;
                        }
                        else {
                            fileType = File_Type.Unknown;
                        }
                        break;
                    case Device_Type.MicrosoftBand:
                        break;
                    default:
                        break;
                }
            }

            return fileType;
        }

        #endregion
    }
}
