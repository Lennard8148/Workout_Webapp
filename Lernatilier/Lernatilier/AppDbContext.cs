using Microsoft.EntityFrameworkCore;
using FitnessTrackerAPI.Models;
using System.Collections.Generic;

namespace FitnessTrackerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Benutzer> Benutzer { get; set; }
        public DbSet<Workout> Workouts { get; set; }
    }
}
