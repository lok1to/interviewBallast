using InterviewBallast.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewBallast.Infrastructure.Context
{
    public class InterviewBallastAuthContext : DbContext
    {
        public InterviewBallastAuthContext(DbContextOptions<InterviewBallastAuthContext> dbContextOptions) : base(dbContextOptions)
        {
            this.Database.EnsureCreated();
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).HasMaxLength(200).IsRequired().HasColumnType("NVARCHAR");
                entity.Property(e => e.LastName).HasMaxLength(200).IsRequired().HasColumnType("NVARCHAR");
                entity.Property(e => e.Username).HasMaxLength(200).IsRequired().HasColumnType("NVARCHAR");
                entity.Property(e => e.Password).HasMaxLength(200).IsRequired().HasColumnType("NVARCHAR");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
