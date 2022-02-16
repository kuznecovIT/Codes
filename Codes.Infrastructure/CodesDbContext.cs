using Codes.Domain.Entities;
using Codes.Infrastructure.ModelBuilders;
using Microsoft.EntityFrameworkCore;

namespace Codes.Infrastructure
{
    public class CodesDbContext : DbContext
    {
        public CodesDbContext(DbContextOptions<CodesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Code> Codes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.BuildCodeModel();
        }
    }
}