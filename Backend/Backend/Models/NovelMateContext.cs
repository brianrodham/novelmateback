using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace NovelMate.Models
{
    public class NovelMateContext : DbContext
    {
        public string ConnectionString { get; set; }

        public DbSet<StoryNode> StoryNodes { get; set; }

        public NovelMateContext(DbContextOptions<NovelMateContext> options)
           : base(options)
        {
        }



        // Tables should be created automatically, so this shouldn't be needed. 
        // Uncomment if you want to override the default table names

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoryNode>().ToTable("StoryNodes");
        }*/

    }
}
