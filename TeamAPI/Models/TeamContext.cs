using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TeamAPI.Models;

namespace TeamAPI.Models
{
    public class TeamContext : DbContext
    {
        public TeamContext(DbContextOptions<TeamContext> options)
            : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; } = null!;

        public DbSet<Player> Players { get; set; } = null!;
    }
}
