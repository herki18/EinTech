using System;
using EinTech.Api.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace EinTech.Api.DAL
{
    public static class SeedData
    {
        public static void SeedInitialData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupEntity>().HasData(FirstGroup);
            modelBuilder.Entity<GroupEntity>().HasData(SecondGroup);

            modelBuilder.Entity<PersonEntity>().HasData(new PersonEntity()
            {
                Id = 1,
                Name = "Person A",
                GroupId = FirstGroup.Id,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });

            modelBuilder.Entity<PersonEntity>().HasData(new PersonEntity()
            {
                Id = 2,
                Name = "Person B",
                GroupId = FirstGroup.Id,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });

            modelBuilder.Entity<PersonEntity>().HasData(new PersonEntity()
            {
                Id = 3,
                Name = "Person C",
                GroupId = FirstGroup.Id,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });

            modelBuilder.Entity<PersonEntity>().HasData(new PersonEntity()
            {
                Id = 4,
                Name = "Person D",
                GroupId = SecondGroup.Id,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });

            modelBuilder.Entity<PersonEntity>().HasData(new PersonEntity()
            {
                Id = 5,
                Name = "Person E",
                GroupId = SecondGroup.Id,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            });
        }

        public static GroupEntity FirstGroup =>
            new GroupEntity
            {
                Id = 1,
                Name = "GroupA",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

        public static GroupEntity SecondGroup =>
            new GroupEntity
            {
                Id = 2,
                Name = "GroupB",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
    }
}
