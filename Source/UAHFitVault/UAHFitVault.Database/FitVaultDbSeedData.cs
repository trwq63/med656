using System.Collections.Generic;
using System.Data.Entity;
using UAHFitVault.Database.Entities;

namespace UAHFitVault.Database
{
    public class FitVaultDbSeedData : DropCreateDatabaseIfModelChanges<FitVaultContext> {

        protected override void Seed(FitVaultContext context) {
            //base.Seed(context);
            GetPatients().ForEach(p => context.Patients.Add(p));
            GetPhysicians().ForEach(p => context.Physicians.Add(p));
        }

        private static List<Patient> GetPatients() {
            return new List<Patient>
            {
            new Patient
            {
                Age = 10,
                Ethnicity = 1,
                Gender = 1,
                Height = 56,
                Id = 1,
                Location = 1,
                Race = 1,
                Weight = 160.0F
            },
        };
        }

        private static List<Physician> GetPhysicians() {
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
    }
}
