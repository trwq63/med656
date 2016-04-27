using System.Data.Entity;
using UAHFitVault.Database.Entities;
using System.Data.Entity.Validation;
using System;

namespace UAHFitVault.Database
{
    public class FitVaultContext : DbContext
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public FitVaultContext() 
           //: base("name=DefaultConnection") {
            : base("name=FitVaultDatabase") {
            // Database.SetInitializer<FitVaultContext>(new DropCreateDatabaseIfModelChanges<FitVaultContext>());
        }

        #endregion

        #region Public Methods

        public virtual void Commit() {
            try
            {
                base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", 
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
            }
        }

        #endregion

        #region DataBase Tables

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<BasisPeakSummaryData> BasisPeakSummaryData { get; set; }
        public virtual DbSet<Experiment> Experiments { get; set; }
        public virtual DbSet<ExperimentAdministrator> ExperimentAdministrators { get; set; }
        public virtual DbSet<MedicalDevice> MedicalDevices { get; set; }
        public virtual DbSet<MSBandAccelerometer> MSBandAccelerometer { get; set; }
        public virtual DbSet<MSBandCalories> MSBandCalories { get; set; }
        public virtual DbSet<MSBandDistance> MSBandDistance { get; set; }
        public virtual DbSet<MSBandGyroscope> MSBandGyroscrope { get; set; }
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
        public virtual DbSet<ZephyrBRRR> ZephyrBRRRData { get; set; }
        public virtual DbSet<AccountRequest> AccountRequests { get; set; }

        #endregion

        #region DbContext Overrides

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }    
}
