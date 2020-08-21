using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FD.Entity
{
    public class TestDbContext : DbContext
    {
        public DbSet<UserInfo> userInfos;
        public TestDbContext(DbContextOptions<TestDbContext> options)
             : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserInfo>(entity => {
                entity.Property(t => t.password).HasDefaultValue("123456");
            });
        }
    }
}
