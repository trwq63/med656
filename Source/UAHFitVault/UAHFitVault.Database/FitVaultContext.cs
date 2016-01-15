using System.Data.Entity;

namespace UAHFitVault.Database
{
    public class FitVaultContext : DbContext
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public FitVaultContext() : base("name=FitVaultDatabase") {
            Database.SetInitializer<FitVaultContext>(new DropCreateDatabaseIfModelChanges<FitVaultContext>());
        }

        #endregion

        #region DataBase Tables

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<BasisPeakSummaryData> BasisPeakSummaryData { get; set; }
        public virtual DbSet<Experiment> Experiments { get; set; }
        public virtual DbSet<ExperimentAdministrator> ExperimentAdministrators { get; set; }
        public virtual DbSet<MedicalDevice> MedicalDevices { get; set; }
        public virtual DbSet<MSBandAccelerometer> MSBandAccelerometer { get; set; }
        public virtual DbSet<MSBandCalories> MSBandCalories { get; set; }
        public virtual DbSet<MSBandDistance> MSBandDistance { get; set; }
        public virtual DbSet<MSBandGryoscope> MSBandGyroscrope { get; set; }
        public virtual DbSet<MSBandHeartRate> MSBandHeartRate { get; set; }
        public virtual DbSet<MSBandPedometer> MSBandPedometer { get; set; }
        public virtual DbSet<MSBandTemperature> MSBandTemperature { get; set; }
        public virtual DbSet<MSBandUV> MSBandUV { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<PatientData> PatientData { get; set; }
        public virtual DbSet<Physician> Physicians { get; set; }
        public virtual DbSet<ZephyrAccelerometer> ZephyrAccelerometer { get; set; }
        public virtual DbSet<ZephyrBreathingWaveform> ZephyrBreathingWaveform { get; set; }
        public virtual DbSet<ZephyrECGWaveform> ZephyrECGWaveform { get; set; }
        public virtual DbSet<ZephyrEventData> ZephyrEventData { get; set; }
        public virtual DbSet<ZephyrSummaryData> ZephyrSummaryData { get; set; }

        #endregion

        #region DbContext Overrides

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
