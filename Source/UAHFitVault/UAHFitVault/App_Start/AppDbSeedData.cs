using System.Collections.Generic;
using System.Data.Entity;
using UAHFitVault.Models;

namespace UAHFitVault.App_Start
{
    public class AppDbSeedData : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context) {
            //base.Seed(context);
            GetUsers().ForEach(u => context.Users.Add(u));
        }

        private static List<ApplicationUser> GetUsers() {
            string userId = "95eee4b8 - 615f - 4777 - 932e-7e3f50db8233";
           // string roleId = "6402f1aa-7ae0-4f98-b2a8-dea595d3b31f";

            return new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    //Roles = new List<IdentityUserRole>() {
                    //    new IdentityUserRole() {
                    //        RoleId = roleId,
                    //        UserId = userId
                    //    }
                    //},
                    Id = userId,                    
                    UserName = "fry@futurama.com",
                    Status = 1,
                    PasswordHash = ""
                },
            };
        }
    }
}