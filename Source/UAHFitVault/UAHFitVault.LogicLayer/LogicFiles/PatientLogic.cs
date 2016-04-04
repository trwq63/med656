using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UAHFitVault.Database.Entities;
using UAHFitVault.LogicLayer.Enums;
using System.Text.RegularExpressions;

namespace UAHFitVault.LogicLayer.LogicFiles
{
    /// <summary>
    /// Class contains the logic functions needed to work with Patient functionality
    /// </summary>
    public static class PatientLogic
    {
        #region Public Methods

        /// <summary>
        /// Determine which type of medical device was entered by the user.
        /// </summary>
        /// <param name="filename">patient data record file name</param>
        /// <returns></returns>
        public static Device_Type DetermineDeviceType(string filename) {
            Device_Type deviceType = Device_Type.Unknown;

            if (!string.IsNullOrEmpty(filename)) {
                if (filename.Contains("Accelerometer")) {
                    deviceType = Device_Type.Microsoft_Band;
                }
                else if (filename.Contains("Accel")) {
                    deviceType = Device_Type.Zephyr;
                }
                else if (filename.Contains("bodymetrics")) {
                    deviceType = Device_Type.BasisPeak;
                }
                else if (filename.Contains("Summary")) {
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
                            fileType = File_Type.Accelerometer;
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
                        if (fileName.Contains("Accel")) {
                            fileType = File_Type.Accelerometer;
                        }
                        else if (fileName.Contains("Calories")) {
                            fileType = File_Type.Calorie;
                        }
                        else if (fileName.Contains("Distance")) {
                            fileType = File_Type.Distance;
                        }
                        else if (fileName.Contains("Gyroscope")) {
                            fileType = File_Type.Gyroscope;
                        }
                        else if (fileName.Contains("HeartRate")) {
                            fileType = File_Type.HeartRate;
                        }
                        else if (fileName.Contains("Pedometer")) {
                            fileType = File_Type.Pedometer;
                        }
                        else if (fileName.Contains("UV")) {
                            fileType = File_Type.UV;
                        }
                        else if (fileName.Contains("Temperature")) {
                            fileType = File_Type.Temperature;
                        }
                        else {
                            fileType = File_Type.Unknown;
                        }
                        break;
                    default:
                        break;
                }
            }

            return fileType;
        }


        /// <summary>
        /// Create a list of ZephyrAccelerometer objects from the data read from the csv file selected by the user.
        /// </summary>
        /// <param name="csvReader">csv reader object</param>
        /// <param name="patientData">Patient data record that will be referenced by each zephyr accel data record.</param>
        /// <returns></returns>
        public static List<ZephyrAccelerometer> BuildZephyrAccelDataList(CsvReader csvReader, PatientData patientData) {
            List<ZephyrAccelerometer> zephyrAccelData = null;

            if (csvReader != null && patientData != null && patientData.Id != null) {
                zephyrAccelData = new List<ZephyrAccelerometer>();
                while (csvReader.ReadNextRecord()) {
                    if (csvReader != null) {
                        //File should read in the following order.
                        //Time | Vertical | Lateral | Sagittal
                        string dateFormat = "dd/MM/yyyy HH:mm:ss.fff";
                        string date = csvReader[0];
                        DateTime dateTime;
                        if (DateTime.TryParseExact(date, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)) {
                            ZephyrAccelerometer zephyrAccel = new ZephyrAccelerometer() {
                                Time = dateTime,
                                Vertical = Convert.ToInt32(csvReader[1]),
                                Lateral = Convert.ToInt32(csvReader[2]),
                                Sagittal = Convert.ToInt32(csvReader[3]),
                                PatientDataId = patientData.Id
                            };
                            zephyrAccelData.Add(zephyrAccel);
                        }
                    }
                }
            }

            return zephyrAccelData;
        }


        /// <summary>
        /// Create a list of ZephyrECGWaveform objects from the data read from the csv file selected by the user.
        /// </summary>
        /// <param name="csvReader">csv reader object</param>
        /// <param name="patientData">Patient data record that will be referenced by each zephyr ecg data record.</param>
        /// <returns></returns>
        public static List<ZephyrECGWaveform> BuildZephyrEcgDataList(CsvReader csvReader, PatientData patientData) {
            List<ZephyrECGWaveform> zephyrEcgData = null;

            if (csvReader != null && patientData != null && patientData.Id != null) {
                zephyrEcgData = new List<ZephyrECGWaveform>();
                while (csvReader.ReadNextRecord()) {
                    if (csvReader != null) {
                        //File should read in the following order.
                        //Time | EcgWaveform
                        string dateFormat = "dd/MM/yyyy HH:mm:ss.fff";
                        string date = csvReader[0];
                        DateTime dateTime;
                        if (DateTime.TryParseExact(date, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)) {
                            ZephyrECGWaveform zephyrEcg = new ZephyrECGWaveform() {
                                Time = dateTime,
                                Data = Convert.ToInt32(csvReader[1]),
                                PatientDataId = patientData.Id
                            };
                            zephyrEcgData.Add(zephyrEcg);
                        }
                    }
                }
            }

            return zephyrEcgData;
        }


        /// <summary>
        /// Create a list of ZephyrBreathingWaveform objects from the data read from the csv file selected by the user.
        /// </summary>
        /// <param name="csvReader">csv reader object</param>
        /// <param name="patientData">Patient data record that will be referenced by each zephyr breathing data record.</param>
        /// <returns></returns>
        public static List<ZephyrBreathingWaveform> BuildZephyrBreathingDataList(CsvReader csvReader, PatientData patientData) {
            List<ZephyrBreathingWaveform> zephyrBreathingData = null;

            if (csvReader != null && patientData != null && patientData.Id != null) {
                zephyrBreathingData = new List<ZephyrBreathingWaveform>();
                while (csvReader.ReadNextRecord()) {
                    if (csvReader != null) {
                        //File should read in the following order.
                        //Time | BreathingWaveform
                        string dateFormat = "dd/MM/yyyy HH:mm:ss.fff";
                        string date = csvReader[0];
                        DateTime dateTime;
                        if (DateTime.TryParseExact(date, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)) {
                            ZephyrBreathingWaveform zephyrBreathing = new ZephyrBreathingWaveform() {
                                Time = dateTime,
                                Data = Convert.ToInt32(csvReader[1]),
                                PatientDataId = patientData.Id
                            };
                            zephyrBreathingData.Add(zephyrBreathing);
                        }
                    }
                }
            }

            return zephyrBreathingData;
        }


        /// <summary>
        /// Create a list of ZephyrEventData objects from the data read from the csv file selected by the user.
        /// </summary>
        /// <param name="csvReader">csv reader object</param>
        /// <param name="patientData">Patient data record that will be referenced by each zephyr event data record.</param>
        /// <returns></returns>
        public static List<ZephyrEventData> BuildZephyrEventDataList(CsvReader csvReader, PatientData patientData) {
            List<ZephyrEventData> zephyrEventData = null;

            if (csvReader != null && patientData != null && patientData.Id != null) {
                zephyrEventData = new List<ZephyrEventData>();
                while (csvReader.ReadNextRecord()) {
                    if (csvReader != null) {
                        //File should read in the following order.
                        //Seq | Year | Month | Day | ms | Event Code | Type | Source | EventId | Event Specific Data
                        int ms = Convert.ToInt32(csvReader[4]);
                        TimeSpan time = TimeSpan.FromMilliseconds(ms);

                        DateTime dateTime = new DateTime(Convert.ToInt32(csvReader[1]),
                                                         Convert.ToInt32(csvReader[2]),
                                                         Convert.ToInt32(csvReader[3]),
                                                         time.Hours,
                                                         time.Minutes,
                                                         time.Seconds,
                                                         time.Milliseconds);

                        ZephyrEventData zephyrEvent = new ZephyrEventData() {
                            Date = dateTime,
                            EventCode = Convert.ToInt32(csvReader[5]),
                            Type = csvReader[6],
                            Source = csvReader[7],
                            EventId = Convert.ToInt32(csvReader[8]),
                            EventSpecificData = csvReader[9],
                            PatientDataId = patientData.Id
                        };
                        zephyrEventData.Add(zephyrEvent);
                        
                    }
                }
            }

            return zephyrEventData;
        }


        /// <summary>
        /// Create a list of ZephyrSummaryData objects from the data read from the csv file selected by the user.
        /// </summary>
        /// <param name="csvReader">csv reader object</param>
        /// <param name="patientData">Patient data record that will be referenced by each zephyr summary data record.</param>
        /// <returns></returns>
        public static List<ZephyrSummaryData> BuildZephyrSummaryDataList(CsvReader csvReader, PatientData patientData) {
            List<ZephyrSummaryData> zephyrSummaryData = null;

            if (csvReader != null && patientData != null && patientData.Id != null) {
                zephyrSummaryData = new List<ZephyrSummaryData>();
                while (csvReader.ReadNextRecord()) {
                    if (csvReader != null) {
                        //File should read in the following order.
                        //Time | HR | BR | SkinTemp | Posture | Activity | PeakAccel | BatteryVolts | BatteryLevel | BRAmplitude
                        //cont. BRNoise | BRConfidence | ECGAmplitude | ECGNoise | HRConfidence | HRV | SystemConfidence | GSR
                        //cont. ROGState | ROGTime | VerticalMin | VericalPeak | LateralMin | LateralPeak | SagittalMin | SagittalPeak
                        //cont. DeviceTemp | StatusInfo | LinkQuality | RSSI | TxPower | CoreTemp | AuxADC1 | AuxADC2 | AuxADC3
                        string dateFormat = "dd/MM/yyyy HH:mm:ss.fff";
                        string date = csvReader[0];
                        DateTime dateTime;
                        if (DateTime.TryParseExact(date, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)) {

                            ZephyrSummaryData zephyrSummary = new ZephyrSummaryData() {
                                Date = dateTime,
                                HeartRate = Convert.ToInt32(csvReader[1]),
                                BreathingRate = (float)Convert.ToDouble(csvReader[2]),
                                SkinTemp = (float)Convert.ToDouble(csvReader[3]),
                                Posture = Convert.ToInt32(csvReader[4]),
                                Activity = (float)Convert.ToDouble(csvReader[5]),
                                PeakAccel = (float)Convert.ToDouble(csvReader[6]),
                                BatteryVolts = (float)Convert.ToDouble(csvReader[7]),
                                BatteryLevel = (float)Convert.ToDouble(csvReader[8]),
                                BRAmplitude = (float)Convert.ToDouble(csvReader[9]),
                                BRNoise = (float)Convert.ToDouble(csvReader[10]),
                                BRConfidence = Convert.ToInt32(csvReader[11]),
                                ECGAmplitude = (float)Convert.ToDouble(csvReader[12]),
                                ECGNoise = (float)Convert.ToDouble(csvReader[13]),
                                HRConfidence = Convert.ToInt32(csvReader[14]),
                                HRV = Convert.ToInt32(csvReader[15]),
                                SystemConfidence = Convert.ToInt32(csvReader[16]),
                                GSR = Convert.ToInt32(csvReader[17]),
                                ROGState = Convert.ToInt32(csvReader[18]),
                                ROGTime = Convert.ToInt32(csvReader[19]),
                                VerticalMin = (float)Convert.ToDouble(csvReader[20]),
                                VerticalPeak = (float)Convert.ToDouble(csvReader[21]),
                                LateralMin = (float)Convert.ToDouble(csvReader[22]),
                                LateralPeak = (float)Convert.ToDouble(csvReader[23]),
                                SagittalMin = (float)Convert.ToDouble(csvReader[24]),
                                SagittalPeak = (float)Convert.ToDouble(csvReader[25]),
                                DeviceTemp = (float)Convert.ToDouble(csvReader[26]),
                                StatusInfo = Convert.ToInt32(csvReader[27]),
                                LinkQuality = Convert.ToInt32(csvReader[28]),
                                RSSI = Convert.ToInt32(csvReader[29]),
                                TxPower = Convert.ToInt32(csvReader[30]),
                                CoreTemp = (float)Convert.ToDouble(csvReader[31]),
                                AuxADC1 = Convert.ToInt32(csvReader[32]),
                                AuxADC2 = Convert.ToInt32(csvReader[33]),
                                AuxADC3 = Convert.ToInt32(csvReader[34]),
                                PatientDataId = patientData.Id
                            };
                            zephyrSummaryData.Add(zephyrSummary);
                        }
                    }
                }
            }

            return zephyrSummaryData;
        }


        /// <summary>
        /// Create a list of BasisPeakSummaryData objects from the data read from the csv file selected by the user.
        /// </summary>
        /// <param name="csvReader">Csv reader object</param>
        /// <param name="patientData">Patient data record that will be referenced by each basispeak summary data record.</param>
        /// <returns></returns>
        public static List<BasisPeakSummaryData> BuildBasisPeakSummaryDataList(CsvReader csvReader, PatientData patientData) {
            List<BasisPeakSummaryData> basisPeakSummaryData = null;

            if (csvReader != null && patientData != null && patientData.Id != null) {
                basisPeakSummaryData = new List<BasisPeakSummaryData>();

                while (csvReader.ReadNextRecord()) {
                    if (csvReader != null) {
                        //TODO: Dr. Milenkovic has asked to automatically convert the date from GMT time.
                        //File should read in the following order.
                        //Date | Calories | GSR | heart-rate | skin-temp | steps
                        BasisPeakSummaryData summary = new BasisPeakSummaryData();
                        summary.Date = DateTime.Parse(csvReader[0]);

                        if (!string.IsNullOrEmpty(csvReader[1])) {
                            summary.Calories = (float)Convert.ToDouble(csvReader[1]);
                        }
                        if (!string.IsNullOrEmpty(csvReader[2])) {
                            summary.GSR = (float)Convert.ToDouble(csvReader[2]);
                        }
                        if (!string.IsNullOrEmpty(csvReader[3])) {
                            summary.HeartRate = Convert.ToInt32(csvReader[3]);
                        }
                        if (!string.IsNullOrEmpty(csvReader[4])) {
                            summary.SkinTemp = (float)Convert.ToDouble(csvReader[4]);
                        }
                        summary.Steps = (!string.IsNullOrEmpty(csvReader[5])) ? Convert.ToInt32(csvReader[5]) : 0;
                        summary.PatientDataId = patientData.Id;

                        basisPeakSummaryData.Add(summary);
                    }
                }
            }

            return basisPeakSummaryData;
        }

        /// <summary>
        /// Get the date for the data from the first line in a Microsoft band csv file.
        /// </summary>
        /// <param name="csvReader">csv reader object</param>
        /// <returns></returns>
        public static DateTime FindMSBandDate(CsvReader csvReader) {
            //First line contains the date of the file that needs to be pulled out.
            string dateData = string.Empty;

            DateTime date = DateTime.MinValue;

            //Read one time to get the column information for the first line.
            //This will contain the date information for the file.
            while (csvReader.ReadNextRecord()) {
                if (csvReader != null) {
                    //If the date has not been extracted from the file extract it.
                    if (string.IsNullOrEmpty(dateData)) {
                        dateData = csvReader.Columns.FirstOrDefault().Name;
                        date = ExactMSBandFileDate(dateData);
                    }
                }
                break;
            }

            return date;
        }


        /// <summary>
        /// Create a list of Microsoft Band Accelerometer objects from the data read from the csv file selected by the user.
        /// </summary>
        /// <param name="csvReader">csv reader object</param>
        /// <param name="patientData">Patient data record that will be referenced by each microsoft band accel data record.</param>
        /// <param name="date">Date the data in the file was collected.</param>
        /// <returns></returns>
        public static List<MSBandAccelerometer> BuildMSBandAccelerometerDataList(CsvReader csvReader, PatientData patientData, DateTime date) {
            List<MSBandAccelerometer> msBandAccelData = null;

            if (csvReader != null && patientData != null && patientData.Id != null) {
                msBandAccelData = new List<MSBandAccelerometer>();

                while (csvReader.ReadNextRecord()) {
                    if (csvReader != null) {
                        //File should read in the following order.
                        //Time | Lateral | Vertical | Sagittal
                        string dateFormat = "HH:mm:ss.fff";
                        string dateInfo = csvReader[0];
                        DateTime dateTime;
                        if (DateTime.TryParseExact(dateInfo, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)) {
                            date = new DateTime(date.Year, date.Month, date.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
                            MSBandAccelerometer msBandAccel = new MSBandAccelerometer() {
                                Date = date,
                                Lateral = (float)Convert.ToDouble(csvReader[1]),
                                Vertical = (float)Convert.ToDouble(csvReader[2]),
                                Sagittal = (float)Convert.ToDouble(csvReader[3]),
                                PatientDataId = patientData.Id
                            };
                            msBandAccelData.Add(msBandAccel);
                        }
                    }
                }
            }

            return msBandAccelData;
        }


        /// <summary>
        /// Create a list of Microsoft Band Calorie objects from the data read from the csv file selected by the user.
        /// </summary>
        /// <param name="csvReader">csv reader object</param>
        /// <param name="patientData">Patient data record that will be referenced by each microsoft band calories data record.</param>
        /// <param name="date">Date the data in the file was collected.</param>
        /// <returns></returns>
        public static List<MSBandCalories> BuildMSBandCalorieDataList(CsvReader csvReader, PatientData patientData, DateTime date) {
            List<MSBandCalories> msBandCalorieData = null;

            if (csvReader != null && patientData != null && patientData.Id != null) {
                msBandCalorieData = new List<MSBandCalories>();

                while (csvReader.ReadNextRecord()) {
                    if (csvReader != null) {
                        //File should read in the following order.
                        //Time | Total
                        string dateFormat = "HH:mm:ss";
                        string dateInfo = csvReader[0];
                        DateTime dateTime;
                        if (DateTime.TryParseExact(dateInfo, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)) {
                            date = new DateTime(date.Year, date.Month, date.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
                            MSBandCalories msBandCalories = new MSBandCalories() {
                                Date = date,
                                Total = Convert.ToInt32(csvReader[1]),
                                PatientDataId = patientData.Id
                            };
                            msBandCalorieData.Add(msBandCalories);
                        }
                    }
                }
            }

            return msBandCalorieData;
        }


        /// <summary>
        /// Create a list of Microsoft Band Distance objects from the data read from the csv file selected by the user.
        /// </summary>
        /// <param name="csvReader">csv reader object</param>
        /// <param name="patientData">Patient data record that will be referenced by each microsoft band distance data record.</param>
        /// <param name="date">Date the data in the file was collected.</param>
        /// <returns></returns>
        public static List<MSBandDistance> BuildMSBandDistanceDataList(CsvReader csvReader, PatientData patientData, DateTime date) {
            List<MSBandDistance> msBandDistanceData = null;

            if (csvReader != null && patientData != null && patientData.Id != null) {
                msBandDistanceData = new List<MSBandDistance>();

                while (csvReader.ReadNextRecord()) {
                    if (csvReader != null) {
                        //File should read in the following order.
                        //Time | Motion Type | Pace | Speed | Total
                        string dateFormat = "HH:mm:ss";
                        string dateInfo = csvReader[0];
                        DateTime dateTime;
                        if (DateTime.TryParseExact(dateInfo, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)) {
                            date = new DateTime(date.Year, date.Month, date.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
                            MSBandDistance msBandDistance = new MSBandDistance() {
                                Date = date,
                                MotionType = csvReader[1],
                                Pace = (float)Convert.ToDouble(csvReader[2]),
                                Speed = (float)Convert.ToDouble(csvReader[3]),
                                Total = (float)Convert.ToDouble(csvReader[4]),
                                PatientDataId = patientData.Id
                            };
                            msBandDistanceData.Add(msBandDistance);
                        }
                    }
                }
            }

            return msBandDistanceData;
        }

        /// <summary>
        /// Create a list of Microsoft Band Gyroscope objects from the data read from the csv file selected by the user.
        /// </summary>
        /// <param name="csvReader">csv reader object</param>
        /// <param name="patientData">Patient data record that will be referenced by each microsoft band gyroscope data record.</param>
        /// <param name="date">Date the data in the file was collected.</param>
        /// <returns></returns>
        public static List<MSBandGyroscope> BuildMSBandGyroscopeDataList(CsvReader csvReader, PatientData patientData, DateTime date) {
            List<MSBandGyroscope> msBandGyroscopeData = null;

            if (csvReader != null && patientData != null && patientData.Id != null) {
                msBandGyroscopeData = new List<MSBandGyroscope>();

                while (csvReader.ReadNextRecord()) {
                    if (csvReader != null) {
                        //File should read in the following order.
                        //Timestamp | X | Y | Z
                        string dateFormat = "HH:mm:ss";
                        string dateInfo = csvReader[0];
                        DateTime dateTime;
                        if (DateTime.TryParseExact(dateInfo, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)) {
                            date = new DateTime(date.Year, date.Month, date.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
                            MSBandGyroscope msBandGyroscope = new MSBandGyroscope() {
                                Timestamp = date,
                                X = (float)Convert.ToDouble(csvReader[1]),
                                Y = (float)Convert.ToDouble(csvReader[2]),
                                Z = (float)Convert.ToDouble(csvReader[3]),
                                PatientDataId = patientData.Id
                            };
                            msBandGyroscopeData.Add(msBandGyroscope);
                        }
                    }
                }
            }

            return msBandGyroscopeData;
        }


        /// <summary>
        /// Create a list of Microsoft Band Heart Rate objects from the data read from the csv file selected by the user.
        /// </summary>
        /// <param name="csvReader">csv reader object</param>
        /// <param name="patientData">Patient data record that will be referenced by each microsoft band heart rate data record.</param>
        /// <param name="date">Date the data in the file was collected.</param>
        /// <returns></returns>
        public static List<MSBandHeartRate> BuildMSBandHeartRateDataList(CsvReader csvReader, PatientData patientData, DateTime date) {
            List<MSBandHeartRate> msBandHeartRateData = null;

            if (csvReader != null && patientData != null && patientData.Id != null) {
                msBandHeartRateData = new List<MSBandHeartRate>();

                while (csvReader.ReadNextRecord()) {
                    if (csvReader != null) {
                        //File should read in the following order.
                        //Date | Read status | HeartRate
                        string dateFormat = "HH:mm:ss";
                        string dateInfo = csvReader[0];
                        DateTime dateTime;
                        if (DateTime.TryParseExact(dateInfo, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)) {
                            date = new DateTime(date.Year, date.Month, date.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
                            MSBandHeartRate msBandHeartRate = new MSBandHeartRate() {
                                Date = date,
                                ReadStatus = csvReader[1],
                                HeartRate = Convert.ToInt32(csvReader[2]),
                                PatientDataId = patientData.Id
                            };
                            msBandHeartRateData.Add(msBandHeartRate);
                        }
                    }
                }
            }

            return msBandHeartRateData;
        }


        /// <summary>
        /// Create a list of Microsoft Band Pedometer objects from the data read from the csv file selected by the user.
        /// </summary>
        /// <param name="csvReader">csv reader object</param>
        /// <param name="patientData">Patient data record that will be referenced by each microsoft band pedometer data record.</param>
        /// <param name="date">Date the data in the file was collected.</param>
        /// <returns></returns>
        public static List<MSBandPedometer> BuildMSBandPedometerDataList(CsvReader csvReader, PatientData patientData, DateTime date) {
            List<MSBandPedometer> msBandPedometerData = null;

            if (csvReader != null && patientData != null && patientData.Id != null) {
                msBandPedometerData = new List<MSBandPedometer>();

                while (csvReader.ReadNextRecord()) {
                    if (csvReader != null) {
                        //File should read in the following order.
                        //Date | Steps
                        string dateFormat = "HH:mm:ss";
                        string dateInfo = csvReader[0];
                        DateTime dateTime;
                        if (DateTime.TryParseExact(dateInfo, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)) {
                            date = new DateTime(date.Year, date.Month, date.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
                            MSBandPedometer msBandPedometer = new MSBandPedometer() {
                                Date = date,
                                Steps = Convert.ToInt32(csvReader[1]),
                                PatientDataId = patientData.Id
                            };
                            msBandPedometerData.Add(msBandPedometer);
                        }
                    }
                }
            }

            return msBandPedometerData;
        }


        /// <summary>
        /// Create a list of Microsoft Band Temperature objects from the data read from the csv file selected by the user.
        /// </summary>
        /// <param name="csvReader">csv reader object</param>
        /// <param name="patientData">Patient data record that will be referenced by each microsoft band temperature data record.</param>
        /// <param name="date">Date the data in the file was collected.</param>
        /// <returns></returns>
        public static List<MSBandTemperature> BuildMSBandTemperatureDataList(CsvReader csvReader, PatientData patientData, DateTime date) {
            List<MSBandTemperature> msBandTemperatureData = null;

            if (csvReader != null && patientData != null && patientData.Id != null) {
                msBandTemperatureData = new List<MSBandTemperature>();

                while (csvReader.ReadNextRecord()) {
                    if (csvReader != null) {
                        //File should read in the following order.
                        //Date | Temperature
                        string dateFormat = "HH:mm:ss";
                        string dateInfo = csvReader[0];
                        DateTime dateTime;
                        if (DateTime.TryParseExact(dateInfo, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)) {
                            date = new DateTime(date.Year, date.Month, date.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
                            MSBandTemperature msBandTemperature = new MSBandTemperature() {
                                Date = date,
                                Temperature = (float)Convert.ToDouble(csvReader[1]),
                                PatientDataId = patientData.Id
                            };
                            msBandTemperatureData.Add(msBandTemperature);
                        }
                    }
                }
            }

            return msBandTemperatureData;
        }


        /// <summary>
        /// Create a list of Microsoft Band UV objects from the data read from the csv file selected by the user.
        /// </summary>
        /// <param name="csvReader">csv reader object</param>
        /// <param name="patientData">Patient data record that will be referenced by each microsoft band uv data record.</param>
        /// <param name="date">Date the data in the file was collected.</param>
        /// <returns></returns>
        public static List<MSBandUV> BuildMSBandUVDataList(CsvReader csvReader, PatientData patientData, DateTime date) {
            List<MSBandUV> msBandUVData = null;

            if (csvReader != null && patientData != null && patientData.Id != null) {
                msBandUVData = new List<MSBandUV>();

                while (csvReader.ReadNextRecord()) {
                    if (csvReader != null) {
                        //File should read in the following order.
                        //Date | UVIndex
                        string dateFormat = "HH:mm:ss";
                        string dateInfo = csvReader[0];
                        DateTime dateTime;
                        if (DateTime.TryParseExact(dateInfo, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)) {
                            date = new DateTime(date.Year, date.Month, date.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
                            MSBandUV msBandUV = new MSBandUV() {
                                Date = date,
                                UVIndex = Convert.ToInt32(csvReader[1]),
                                PatientDataId = patientData.Id
                            };
                            msBandUVData.Add(msBandUV);
                        }
                    }
                }
            }

            return msBandUVData;
        }

        /// <summary>
        /// Verify the activity created by the user.
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public static bool IsActivityValid(Activity activity) {
            bool valid = false;
            if(activity.StartTime != DateTime.MinValue && activity.EndTime != DateTime.MinValue
                && activity.StartTime <= activity.EndTime && activity.StartTime.Year != 1
                && activity.EndTime.Year != 1) {

                valid = true;
            }
            return valid;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Read the date out of text from a cell in csv file.
        /// </summary>
        /// <param name="data">Text from csv file that contains date of date collected.</param>
        /// <returns></returns>
        private static DateTime ExactMSBandFileDate(string data) {
            Regex regex = new Regex(@"\d+-\d+-\d+");
            DateTime date;
            try {
                date = DateTime.Parse(regex.Match(data).Value);
            }
            catch (Exception ex) {
                //Currently no error handling is developed into the system.
                Console.WriteLine(ex.Message);
                return DateTime.MinValue;
            }

            return date;
        }

        #endregion
    }
}
