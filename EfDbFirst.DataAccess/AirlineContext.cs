using EfDbFirst.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace EfDbFirst.DataAccess;

public partial class AirlineContext : DbContext
{
    public virtual DbSet<Airport> Airports { get; set; }
    public virtual DbSet<Country> Countries { get; set; }
    public virtual DbSet<Flight> Flights { get; set; }
    public virtual DbSet<FlightAttendant> FlightAttendants { get; set; }
    public virtual DbSet<FlightInstance> FlightInstances { get; set; }
    public virtual DbSet<Pilot> Pilots { get; set; }
    public virtual DbSet<PlaneDetail> PlaneDetails { get; set; }
    public virtual DbSet<PlaneModel> PlaneModels { get; set; }

    public AirlineContext()
    { }

    public AirlineContext(DbContextOptions<AirlineContext> options)
        : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(Constants.ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Airport>(entity =>
        {
            entity.HasKey(e => e.AirportCode).HasName("PK__Airport__4B677352CD46450C");

            entity.ToTable("Airport");

            entity.Property(e => e.AirportCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.AirportName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContactNo).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.CountryCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.CountryCodeNavigation).WithMany(p => p.Airports)
                .HasForeignKey(d => d.CountryCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CountryCode_fk");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryCode).HasName("country_pk");

            entity.ToTable("Country");

            entity.HasIndex(e => e.CountryName, "Country_name_uk").IsUnique();

            entity.Property(e => e.CountryCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CountryName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightNo).HasName("FlightNo_pk");

            entity.ToTable("Flight");

            entity.Property(e => e.FlightNo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FlightArriveFrom)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FlightDepartTo)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.FlightArriveFromNavigation).WithMany(p => p.FlightFlightArriveFromNavigations)
                .HasForeignKey(d => d.FlightArriveFrom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FLightArriceFrom_fk");

            entity.HasOne(d => d.FlightDepartToNavigation).WithMany(p => p.FlightFlightDepartToNavigations)
                .HasForeignKey(d => d.FlightDepartTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FLightDepartTo_fk");
        });

        modelBuilder.Entity<FlightAttendant>(entity =>
        {
            entity.HasKey(e => e.AttendantId).HasName("attendantID_pk");

            entity.ToTable("FlightAttendant");

            entity.Property(e => e.AttendantId).HasColumnName("AttendantID");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HireDate).HasColumnType("date");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MentorId).HasColumnName("MentorID");

            entity.HasOne(d => d.Mentor).WithMany(p => p.InverseMentor)
                .HasForeignKey(d => d.MentorId)
                .HasConstraintName("mentorID_fk");
        });

        modelBuilder.Entity<FlightInstance>(entity =>
        {
            entity.HasKey(e => e.InstanceId).HasName("InstanceId_pk");

            entity.ToTable("FlightInstance");

            entity.Property(e => e.InstanceId).HasColumnName("InstanceID");
            entity.Property(e => e.CoPilotAboardId).HasColumnName("CoPilotAboardID");
            entity.Property(e => e.DateTimeArrive).HasColumnType("datetime");
            entity.Property(e => e.DateTimeLeave).HasColumnType("datetime");
            entity.Property(e => e.FlightNo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FsmAttendantId).HasColumnName("FSM_AttendantID");
            entity.Property(e => e.PilotAboardId).HasColumnName("PilotAboardID");
            entity.Property(e => e.PlaneId).HasColumnName("PlaneID");

            entity.HasOne(d => d.CoPilotAboard).WithMany(p => p.FlightInstanceCoPilotAboards)
                .HasForeignKey(d => d.CoPilotAboardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CoPilotAboardId_fk");

            entity.HasOne(d => d.FlightNoNavigation).WithMany(p => p.FlightInstances)
                .HasForeignKey(d => d.FlightNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FlightNo_fk");

            entity.HasOne(d => d.FsmAttendant).WithMany(p => p.FlightInstances)
                .HasForeignKey(d => d.FsmAttendantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FSM_AttendantID");

            entity.HasOne(d => d.PilotAboard).WithMany(p => p.FlightInstancePilotAboards)
                .HasForeignKey(d => d.PilotAboardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PilotAboardId_fk");

            entity.HasOne(d => d.Plane).WithMany(p => p.FlightInstances)
                .HasForeignKey(d => d.PlaneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PlaneID_fk");

            entity.HasMany(d => d.Attendants).WithMany(p => p.Instances)
                .UsingEntity<Dictionary<string, object>>(
                    "InstanceAttendant",
                    r => r.HasOne<FlightAttendant>().WithMany()
                        .HasForeignKey("AttendantId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("AttendantId_fk"),
                    l => l.HasOne<FlightInstance>().WithMany()
                        .HasForeignKey("InstanceId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("InstanceId_fk"),
                    j =>
                    {
                        j.HasKey("InstanceId", "AttendantId").HasName("InstanceAttendantID_pk");
                        j.ToTable("InstanceAttendant");
                        j.IndexerProperty<int>("InstanceId").HasColumnName("InstanceID");
                        j.IndexerProperty<int>("AttendantId").HasColumnName("AttendantID");
                    });
        });

        modelBuilder.Entity<Pilot>(entity =>
        {
            entity.HasKey(e => e.PilotId).HasName("PilotId_pk");

            entity.ToTable("Pilot");

            entity.Property(e => e.PilotId).HasColumnName("PilotID");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.PlaneModels).WithMany(p => p.Pilots)
                .UsingEntity<Dictionary<string, object>>(
                    "PlanePilot",
                    r => r.HasOne<PlaneModel>().WithMany()
                        .HasForeignKey("PlaneModel")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("PlaneModel_fk"),
                    l => l.HasOne<Pilot>().WithMany()
                        .HasForeignKey("PilotId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("PilotID_fk"),
                    j =>
                    {
                        j.HasKey("PilotId", "PlaneModel").HasName("planePilot_pk");
                        j.ToTable("PlanePilot");
                        j.IndexerProperty<int>("PilotId").HasColumnName("PilotID");
                        j.IndexerProperty<string>("PlaneModel")
                            .HasMaxLength(10)
                            .IsUnicode(false);
                    });
        });

        modelBuilder.Entity<PlaneDetail>(entity =>
        {
            entity.HasKey(e => e.PlaneId).HasName("PlaneId_pk");

            entity.ToTable("PlaneDetail");

            entity.HasIndex(e => e.RegistrationNo, "RegNO_uk").IsUnique();

            entity.Property(e => e.PlaneId).HasColumnName("PlaneID");
            entity.Property(e => e.ModelNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationNo)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.ModelNumberNavigation).WithMany(p => p.PlaneDetails)
                .HasForeignKey(d => d.ModelNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ModelN_fk");
        });

        modelBuilder.Entity<PlaneModel>(entity =>
        {
            entity.HasKey(e => e.ModelNumber).HasName("ModelN_pk");

            entity.ToTable("PlaneModel");

            entity.Property(e => e.ModelNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ManufacturerName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
