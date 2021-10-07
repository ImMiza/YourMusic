using System;
using Microsoft.EntityFrameworkCore;
using yourmusic.Models;

namespace yourmusic.Context
{
    
    public class ContextDatabase : DbContext
    {

        public DbSet<Music> Musics { get; set; }

        public DbSet<User> Users { get; set; }
    
        public DbSet<Playlist> Playlists { get; set; }

        public ContextDatabase(DbContextOptions<ContextDatabase> options) : base(options) {}

    }

}