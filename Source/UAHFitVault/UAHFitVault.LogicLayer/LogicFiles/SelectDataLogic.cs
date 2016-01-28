using UAHFitVault.LogicLayer.Enums;
using UAHFitVault.Database.Entities;
using System.Collections.Generic;
using System.Linq;

namespace UAHFitVault.LogicLayer.LogicFiles
{
    /// <summary>
    /// Class contains the logic functions needed by the SelectLogicController
    /// </summary>
    public static class SelectDataLogic
    {
        #region Public Methods

        /// <summary>
        /// Determine which type of medical device was entered by the user.
        /// </summary>
        /// <param name="medicalDeviceTypes">List of medical devices in the system.</param>
        /// <param name="deviceType">User selected medical device.</param>
        /// <returns></returns>
        public static MedicalDevice DetermineDeviceType(List<MedicalDevice> medicalDeviceTypes, string deviceType) {
            MedicalDevice medicalDevice = null;

            if(medicalDeviceTypes != null && medicalDeviceTypes.Count > 0 && !string.IsNullOrEmpty(deviceType)) {
                medicalDevice = medicalDeviceTypes.FirstOrDefault(m => m.Name == deviceType);
            }

            return medicalDevice;
        }

        /// <summary>
        /// Determine the type of data that is contained inside the file being processed.
        /// </summary>
        /// <param name="fileName">Name of the file being processed.</param>
        /// <param name="deviceType">The device type the file belongs to.</param>
        /// <returns></returns>
        //TODO: This function needs to be made more dynamic.  Risk of file name change from standard format for the device.
        public static File_Type DetermineFileType(string fileName, MedicalDevice medicalDevice) {
            File_Type fileType = File_Type.Unknown;

            if (!string.IsNullOrEmpty(fileName) && medicalDevice != null && !string.IsNullOrEmpty(medicalDevice.Name)) {
                switch (medicalDevice.Name) {
                    case "BasisPeak":
                        fileType = File_Type.Summary;
                        break;
                    case "Zephyr":
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
                    case "Microsoft Band":
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
