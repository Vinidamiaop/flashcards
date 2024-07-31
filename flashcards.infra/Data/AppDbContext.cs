using System.Reflection;
using flashcards.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace flashcards.infra.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Subject> Subjects {get; set;}
        public DbSet<Question> Questions {get; set;}
        public DbSet<Answer> Answers {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}