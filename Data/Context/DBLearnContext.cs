using System.Collections.Generic;
using System.Reflection.Emit;
using System;
using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace Data.Context
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
        public DbSet<Exercise>? Exercise { get; set; }
        public DbSet<UserProgress> UserProgresses { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var configuration = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json")
        //        .Build();

        //    var connectionString = configuration.GetConnectionString("LearnContext");
        //    optionsBuilder.UseSqlServer(connectionString);
        //}

       

    }
}
