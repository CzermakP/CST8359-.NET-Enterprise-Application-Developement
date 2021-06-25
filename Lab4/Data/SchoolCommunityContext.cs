using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab4.Models; //added
using Microsoft.EntityFrameworkCore; //added

namespace Lab4.Data
{
    public class SchoolCommunityContext : DbContext
    {
        public SchoolCommunityContext(DbContextOptions<SchoolCommunityContext> options) : base(options)
        {
        }

        public DbSet<Student> Students 
        { 
            get; 
            set; 
        }
        public DbSet<Community> Communities
        {
            get;
            set;
        }

        public DbSet<CommunityMembership> Memberships
        {
            get;
            set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Community>().ToTable("Communities");
            modelBuilder.Entity<CommunityMembership>().HasKey(c => new { c.StudentId, c.CommunityId });
        }
    }
}
