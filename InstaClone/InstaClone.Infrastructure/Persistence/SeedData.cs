using InstaClone.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Infrastructure.Persistence
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var passwordHasher = new PasswordHasher<User>();

            /*var user1 = new User { Id = Guid.NewGuid(), UserName = "user1", NormalizedUserName = "USER1", Email = "user1@example.com", NormalizedEmail = "USER1@EXAMPLE.COM", FirstName = "Petar", LastName = "Petrovic", EmailConfirmed = true, SecurityStamp = Guid.NewGuid().ToString()};
            user1.PasswordHash = passwordHasher.HashPassword(user1, "OwnerPass1!");
            user1.PasswordHash = passwordHasher.HashPassword(user1, "OwnerPass1!");

            modelBuilder.Entity<User>().HasData(user1);*/
        }
    }
}
