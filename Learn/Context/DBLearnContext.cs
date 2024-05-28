using System.Collections.Generic;
using System.Reflection.Emit;
using System;
using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace Learn.Context
{
    public class DBLearnContext : DbContext
    {
        public DBLearnContext(DbContextOptions<DBLearnContext> options)
            : base(options)
        {
        }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Vocabulary> Vocabularies { get; set; }
        public DbSet<Speaking> Speakings { get; set; }
        public DbSet<Grammer> Grammers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("LearnContext");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Level>()
                .Property(b => b.CreationDateTime)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Lesson>()
                .Property(b => b.CreationDateTime)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Speaking>()
                .Property(b => b.CreationDateTime)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Grammer>()
                .Property(b => b.CreationDateTime)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Vocabulary>()
                .Property(b => b.CreationDateTime)
                .HasDefaultValueSql("getdate()");
        }

        public DbSet<Model.Models.Exercise>? Exercise { get; set; }

    }
}
