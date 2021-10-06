using System;
using Microsoft.EntityFrameworkCore;
using yourmusic.Models;

namespace yourmusic.Context
{
    
    public class ContextDatabase : DbContext
    {

        private static string _dbPath =  $"{Environment.CurrentDirectory}/Database/database.sqlite";


        public DbSet<Music> Musics { get; set; }

        public DbSet<Album> Albums { get; set; }
    
        public DbSet<Playlist> Playlists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_dbPath}");
        }

    }

}