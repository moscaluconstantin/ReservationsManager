using Microsoft.EntityFrameworkCore;
using ReservationsManager.Domain;
using System.Reflection;
using Action = ReservationsManager.Domain.Action;

namespace EFCoreMappingApp
{
    public class RezervationsDbContext : DbContext
    {
        private readonly string _connection;

        public RezervationsDbContext(string connection) =>
            _connection = connection;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<ActionEmployee> ActionEmployees { get; set; }
        public DbSet<TimeBlock> TimeBlocks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
