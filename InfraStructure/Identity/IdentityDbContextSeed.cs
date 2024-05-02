using Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.Identity
{
    public class IdentityDbContextSeed
    {

        private readonly ModelBuilder _builder;

        public IdentityDbContextSeed(ModelBuilder modelBuilder)
        {
            _builder = modelBuilder;
        }
        public static async Task SeddingIdentity(UserManager<ApplicationUser> manager)
        {
            if (!manager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    Email = "Shelby@2002.com",
                    UserName = "Shelby",
                    Address = new Address
                    {
                        FName = "Ahmed",
                        LName = "Shelby",
                        Street = "10st",
                        City = "Mnf",
                        Governorate = "Menofia",
                        ZipCode = "32951"
                    }

                };


                await manager.CreateAsync(user, "Shelby#@142002");
            }
        }
    }
}
