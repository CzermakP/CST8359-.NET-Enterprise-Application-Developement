using Lab5.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab5.Data
{
    public class AnswerImageDataContext : DbContext
    {

        public AnswerImageDataContext(DbContextOptions<AnswerImageDataContext> options) : base(options) //constructor
        {

        }

        public DbSet<AnswerImage> AnswerImages { get; set; } //DbSet to hold AnswerImage objects

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnswerImage>().ToTable("Answer Image");
        }
    }
}
