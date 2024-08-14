using Domain.Core.Entities;
using Infrastructure.Data.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;


namespace Infrastructure.Data.Core.Context
{
    public class ContextoPrincipal : DbContext, IContextoUnidadDeTrabajo
    {
    //    public ContextoPrincipal() : base()
    //    {
    ////        var options = new DbContextOptionsBuilder<ContextoPrincipal>()
    ////.UseSqlServer(@"Server=DESKTOP-92T8MQB\\DATA;Database=AgenciaViajes;Integrated Security=True;Trusted_Connection=True;")
    ////.Options;
    //    }
       
        public ContextoPrincipal(DbContextOptions<ContextoPrincipal> options)
    : base(options)
        {
            try { var databasecreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databasecreator != null )
                {
                    if(!databasecreator.CanConnect()) databasecreator.Create();
                    if(!databasecreator.HasTables()) databasecreator.CreateTables();
                }
            }
            catch(Exception e) {
            Console.WriteLine(e.ToString());
            }
        }
        //atributo
        DbSet<Hotel> _hotel;
        DbSet<ContactoEmergencia> _contactoEmergencia;
        DbSet<Habitacion> _habitacion;
        DbSet<Huesped> _huesped;
        DbSet<Reserva> _reserva;
        //propiedad
        public DbSet<Hotel> Hotel
        {
            get { return _hotel ?? (_hotel = base.Set<Hotel>()); }
        }
        public DbSet<ContactoEmergencia> ContactoEmergencia
        {
            get { return _contactoEmergencia ?? (_contactoEmergencia = base.Set<ContactoEmergencia>()); }
        }
        public DbSet<Habitacion> Habitacion
        {
            get { return _habitacion ?? (_habitacion = base.Set<Habitacion>()); }
        }
        public DbSet<Huesped> Huesped
        {
            get { return _huesped ?? (_huesped = base.Set<Huesped>()); }
        }
        public DbSet<Reserva> Reserva
        {
            get { return _reserva ?? (_reserva = base.Set<Reserva>()); }
        }
        public new DbSet<Entidad> Set<Entidad>() where Entidad : class
        {
            return base.Set<Entidad>();
        }
        public void Attach<Entidad>(Entidad item) where Entidad : class
        {
            if (Entry(item).State == EntityState.Detached)
            {
                base.Set<Entidad>().Attach(item);
            }
        }
        public void SetModified<Entidad>(Entidad item) where Entidad : class
        {
            Entry(item).State = EntityState.Modified;
        }
        public void GuardarCambios()
        {
             base.SaveChanges();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //IConfigurationRoot configuration = new ConfigurationBuilder()
                //    .SetBasePath(Directory.GetCurrentDirectory())
                //    .AddJsonFile("appsettings.json")
                //    .Build();
                //var connectionString = configuration.GetConnectionString("DbCoreConnectionString");
                //optionsBuilder.UseSqlServer(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de modelos
            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(h => h.HotelID);

                entity.Property(h => h.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(h => h.Descripcion)
                    .HasMaxLength(500);

                entity.Property(h => h.Direccion)
                    .HasMaxLength(200);

                entity.Property(h => h.Ciudad)
                    .HasMaxLength(100);

                entity.Property(h => h.Estado)
                    .HasMaxLength(100);

                entity.Property(h => h.Pais)
                    .HasMaxLength(100);

                entity.HasMany(h => h.Habitaciones)
                    .WithOne(hb => hb.Hotel)
                    .HasForeignKey(hb => hb.HotelID);
            });

            modelBuilder.Entity<Habitacion>(entity =>
            {
                entity.HasKey(h => h.HabitacionID);

                entity.Property(h => h.Numero)
                    .IsRequired()
                    .HasMaxLength(20);

                //entity.Property(h => h.CostoBase)
                //    .HasColumnType("decimal(18,2)");

                //entity.Property(h => h.Impuestos)
                //    .HasColumnType("decimal(18,2)");

                entity.Property(h => h.Tipo)
                    .HasMaxLength(50);

                entity.Property(h => h.Ubicacion)
                    .HasMaxLength(100);

                entity.HasMany(h => h.Reservas)
                    .WithOne(r => r.Habitacion)
                    .HasForeignKey(r => r.HabitacionID);
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(r => r.ReservaID);

                entity.Property(r => r.EstadoReserva)
                    .HasMaxLength(50);

                entity.Property(r => r.FechaEntrada)
                    .IsRequired();

                entity.Property(r => r.FechaSalida)
                    .IsRequired();

                entity.Property(r => r.CantidadPersonas)
                    .IsRequired();

                entity.HasMany(r => r.Huespedes)
                    .WithOne(h => h.Reserva)
                    .HasForeignKey(h => h.ReservaID);

                entity.HasOne(r => r.ContactoEmergencia)
                    .WithOne(ce => ce.Reserva)
                    .HasForeignKey<ContactoEmergencia>(ce => ce.ReservaID);
            });

            modelBuilder.Entity<Huesped>(entity =>
            {
                entity.HasKey(h => h.HuespedID);

                entity.Property(h => h.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(h => h.Apellido)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(h => h.FechaNacimiento)
                    .IsRequired();

                entity.Property(h => h.Genero)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(h => h.TipoDocumento)
                    .HasMaxLength(50);

                entity.Property(h => h.NumeroDocumento)
                    .HasMaxLength(50);

                entity.Property(h => h.Email)
                    .HasMaxLength(100);

                entity.Property(h => h.TelefonoContacto)
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<ContactoEmergencia>(entity =>
            {
                entity.HasKey(ce => ce.ContactoEmergenciaID);

                entity.Property(ce => ce.NombreCompleto)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(ce => ce.TelefonoContacto)
                    .HasMaxLength(15);
            });

        }
    }
}

