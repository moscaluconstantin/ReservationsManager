using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReservationsManager.Domain.Auth;
using ReservationsManager.Domain.Models;
using System.Reflection;
using Action = ReservationsManager.Domain.Models.Action;

namespace EFCoreMappingApp
{
    public class RezervationsDbContext : IdentityDbContext<IdentityUser>
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Action> Actions { get; set; }
        public DbSet<ActionEmployee> ActionEmployees { get; set; }
        public DbSet<TimeBlock> TimeBlocks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<UserCredential> UserCredentials { get; set; }
    }
}
