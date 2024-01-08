using InterviewBallast.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InterviewBallast.Infrastructure.Context
{
    public class InterviewBallastContext : DbContext
    {
        public InterviewBallastContext(DbContextOptions<InterviewBallastContext> dbContextOptions) : base(dbContextOptions)
        {
            this.Database.EnsureCreated();
        }

        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).HasMaxLength(200).IsRequired().HasColumnType("NVARCHAR");
                entity.Property(e => e.LastName).HasMaxLength(200).IsRequired().HasColumnType("NVARCHAR");
                entity.Property(e => e.Active).IsRequired().HasColumnType("BIT");
                entity.HasOne(d => d.Address)
                   .WithOne(p => p.Player)
                   .HasForeignKey<Player>(d => d.AddressId)
                   .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Street).HasMaxLength(200).IsRequired().HasColumnType("NVARCHAR");
                entity.Property(e => e.Number).IsRequired().HasColumnType("INT");
                entity.HasOne(a => a.Player)
                    .WithOne(p => p.Address);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}