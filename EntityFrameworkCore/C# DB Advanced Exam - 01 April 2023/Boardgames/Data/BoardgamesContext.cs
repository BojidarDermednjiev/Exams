﻿namespace Boardgames.Data
{
    using Microsoft.EntityFrameworkCore;

    using Models;

    public class BoardgamesContext : DbContext
    {
        public BoardgamesContext()
        {
        }

        public BoardgamesContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Boardgame> Boardgames { get; set; } = null!;
        public DbSet<Seller> Sellers { get; set; } = null!;
        public DbSet<Creator> Creators { get; set; } = null!;
        public BoardgameSeller BoardgamesSellers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString)
                    .UseLazyLoadingProxies();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardgameSeller>(entity => entity.HasKey(bs => new { bs.BoardgameId, bs.SellerId }));
        }
    }
}
