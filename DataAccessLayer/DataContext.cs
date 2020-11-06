using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
   public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Course>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = Guid.NewGuid(),
                    Avatar = "avatar.jpg",
                    FullName = "Mohamed Hemeda",
                    Job = "Programmer"
                }
                );
        }
        public DbSet<Student> Student { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
