using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublicUtilitiesData.Models;

namespace PublicUtilitiesData
{
    public class PUDbContext : DbContext
    {
        private static string _connectionString;

        internal PUDbContext()
        {

        }

        internal DbSet<Unit> Units { get; private set; }
        internal DbSet<Service> Services { get; private set; }
        internal DbSet<Tariff> Tariffs { get; private set; }
        internal DbSet<Consumer> Consumers { get; private set; }
        internal DbSet<MetersData> MetersData { get; private set; }
        internal DbSet<ConsumerTariff> ConsumerTariffs { get; private set; }
        internal DbSet<Calculator> Calculators { get; private set; }
        internal DbSet<TariffCalculators> TariffCalculators { get; private set; }
        internal DbSet<ServiceBase> ServiceBases { get; private set; }
        internal DbSet<MetersType> MetersTypes { get; private set; }
        internal DbSet<ServiceMetersType> ServiceMetersTypes { get; private set; }
        internal DbSet<CalculationsHistory> CalculationsHistory { get; private set; }
        internal DbSet<PeopleCountHistory> PeopleCountHistory { get; private set; }

        public override void Dispose()
        {
            base.Dispose();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MetersData>()
                .HasKey(u => new { u.Period, u.ConsumerId, u.MetersTypeId });

            modelBuilder.Entity<ConsumerTariff>()
                .HasKey(ct => new { ct.ConsumerId, ct.ServiceId });

            modelBuilder.Entity<ServiceMetersType>()
                .HasKey(smt => new { smt.ServiceId, smt.MetersTypeId });

            modelBuilder.Entity<Service>()
                .Ignore(s => s.MetersType);

            modelBuilder.Entity<CalculationsHistory>()
                .HasKey(ch => new { ch.ConsumerId, ch.Period, ch.ServiceId });

            modelBuilder.Entity<PeopleCountHistory>()
                .HasKey(pch => new { pch.ConsumerId, pch.ChangeDate });
        }

        public static void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
