using CompanyMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMVC.ConnectDB
{
    public class AppDbContext : IdentityDbContext<ConfArchUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Proposal> Proposals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Conference>().HasData(new Conference { Id = 1, Name = "Bob", Location = "28 Nguyen Tri Phuong", Start = new DateTime(2019, 10, 12) });
            modelBuilder.Entity<Conference>().HasData(new Conference { Id = 2, Name = "Andy", Location = "28 Nguyen Tri Phuong", Start = new DateTime(2019, 10, 11) });
            modelBuilder.Entity<Conference>().HasData(new Conference { Id = 3, Name = "Jame", Location = "28 Nguyen Tri Phuong", Start = new DateTime(2019, 10, 13) });


            modelBuilder.Entity<Proposal>().HasData(new Proposal {
                Id = 1,
                ConferenceId = 1,
                Speaker = "Le Tuong Phuc",
                Title = "Understanding ASP.NET Core MVC"
            });
            modelBuilder.Entity<Proposal>().HasData(new Proposal {
                Id = 2,
                ConferenceId = 2,
                Speaker = "Anh Khoa",
                Title = "Understanding ASP.NET Core Dapper"
            });
            modelBuilder.Entity<Proposal>().HasData(new Proposal {
                Id = 3,
                ConferenceId = 2,
                Speaker = "Si Lun",
                Title = "Understanding Write CV"
            });

            modelBuilder.Entity<Attendee>().HasData(new Attendee { Id = 1, ConferenceId = 1, Name = "Mai Ba Quan" });
            modelBuilder.Entity<Attendee>().HasData(new Attendee { Id = 2, ConferenceId = 1, Name = "Vinh Hoang" });
            modelBuilder.Entity<Attendee>().HasData(new Attendee { Id = 3, ConferenceId = 2, Name = "Thanh Bon" });
            modelBuilder.Entity<Attendee>().HasData(new Attendee { Id = 4, ConferenceId = 2, Name = "Nhi Uyen" });

        }

    }
}
