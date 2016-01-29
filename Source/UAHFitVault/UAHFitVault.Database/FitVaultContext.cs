using System.Collections.Generic;
using System.Data.Entity;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.Database
{
    public class FitVaultContext : DbContext
    {
        #region Public Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public FitVaultContext() : base("name=FitVaultDatabase") {
           // Database.SetInitializer<FitVaultContext>(new DropCreateDatabaseIfModelChanges<FitVaultContext>());
        }

        #endregion

        #region Public Methods

        public virtual void Commit() {
            base.SaveChanges();
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

    public class StoreSeedData : DropCreateDatabaseIfModelChanges<FitVaultContext>
    {
        protected override void Seed(FitVaultContext context)
        {
            //base.Seed(context);
            GetPatients().ForEach(p => context.Patients.Add(p));
            GetPhysicians().ForEach(p => context.Physicians.Add(p));
            GetUsers().ForEach(u => context.AspNetUsers.Add(u));
        }

        private static List<Patient> GetPatients()
        {
            return new List<Patient>
            {
                new Patient
                {
                    Age = 10,
                    Ethnicity = 1,
                    Gender = 1,
                    Height = 56,
                    Id = new System.Guid(),
                    Location = 1,
                    Race = 1,
                    Weight = 160.0F
                },
            };
        }

        private static List<Physician> GetPhysicians()
        {
            return new List<Physician>
            {
                new Physician
                {
                    Address = "here",
                    Email = "me@here.com",
                    FirstName = "Phillip",
                    Id = 1,
                    LastName = "Fry",
                    PhoneNumber = "123-456-7890"
                },
            };
        }

        private static List<AspNetUser> GetUsers()
        {
            return new List<AspNetUser>
            {
                new AspNetUser
                {
                    AspNetRole = new AspNetRole(),
                    Id = new System.Guid(),
                    UserName = "fry@futurama.com",
                    Status = 1,
                    PasswordHash = ""
                },
            };
        }
    }
}
