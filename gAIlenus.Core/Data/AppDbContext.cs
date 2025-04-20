using Microsoft.EntityFrameworkCore;


namespace gAIlenus.Core
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Diagnosis> Diagnoses => Set<Diagnosis>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.Username).IsRequired().HasMaxLength(16);
                entity.Property(e => e.PasswordHash).IsRequired();
            });

            // Patient configuration
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(32);
                entity.Property(e => e.Surname).IsRequired().HasMaxLength(32);
                entity.Property(e => e.Birthdate).IsRequired();
            });

            // Patient Detail configuration
            modelBuilder.Entity<Diagnosis>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Patient)
                    .WithMany(e => e.Diagnoses)
                    .HasForeignKey(e => e.PatientId);
                entity.Property(e => e.DiagnosisDate).IsRequired();
                entity.Property(e => e.ComplaintOfPatient).IsRequired().HasMaxLength(128);
                entity.Property(e => e.DiagnosisOfDoctor).IsRequired().HasMaxLength(128);
                entity.Property(e => e.DoctorRemarks).IsRequired().HasMaxLength(512);
            });
        }
    }
}
