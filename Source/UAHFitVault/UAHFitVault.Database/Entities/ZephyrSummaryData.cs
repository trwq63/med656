using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UAHFitVault.Database.Entities
{
    public class ZephyrSummaryData
    {
        #region Public Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ZephyrSummaryData() {

        }

        #endregion

        #region Scalar Properties

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int HeartRate { get; set; }
        public float? BreathingRate { get; set; }
        public float? SkinTemp { get; set; }
        public int? Posture { get; set; }
        public float? Activity { get; set; }
        public float? PeakAccel { get; set; }
        public float? BatteryVolts { get; set; }
        public float? BatteryLevel { get; set; }
        public float? BRAmplitude { get; set; }
        public float? BRNoise { get; set; }
        public int? BRConfidence { get; set; }
        public float? ECGAmplitude { get; set; }
        public float? ECGNoise { get; set; }
        public int? HRConfidence { get; set; }
        public int? HRV { get; set; }
        public int? SystemConfidence { get; set; }
        public int? GSR { get; set; }
        public int? ROGState { get; set; }
        public int? ROGTime { get; set; }
        public float? VerticalMin { get; set; }
        public float? VerticalPeak { get; set; }
        public float? LateralMin { get; set; }
        public float? LateralPeak { get; set; }
        public float? SagittalMin { get; set; }
        public float? SagittalPeak { get; set; }
        public float? DeviceTemp { get; set; }
        public int? StatusInfo { get; set; }
        public int? LinkQuality { get; set; }
        public int? RSSI { get; set; }
        public int? TxPower { get; set; }
        public float? CoreTemp { get; set; }
        public int? AuxADC1 { get; set; }
        public int? AuxADC2 { get; set; }
        public int? AuxADC3 { get; set; }

        #endregion

        #region Navigation Properties

        [Required]
        public virtual string PatientDataId { get; set; }

        [ForeignKey("PatientDataId")]
        public virtual PatientData PatientData { get; set; }

        #endregion
    }
}
