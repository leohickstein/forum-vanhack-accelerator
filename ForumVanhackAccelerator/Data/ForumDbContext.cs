using ForumVanhackAccelerator.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumVanhackAccelerator.Data
{
    public class ForumDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Constructor

        public ForumDbContext(DbContextOptions options) : base(options)
        {

        }

        #endregion Constructor

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Posts).WithOne(i => i.User);
            modelBuilder.Entity<ApplicationUser>().HasMany(u => u.Topics).WithOne(i => i.User);

            modelBuilder.Entity<Topic>().ToTable("Topics");
            modelBuilder.Entity<Topic>().Property(t => t.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Topic>().HasOne(t => t.User).WithMany(u => u.Topics);
            modelBuilder.Entity<Topic>().HasMany(t => t.Posts).WithOne(c => c.Topic);

            modelBuilder.Entity<Post>().ToTable("Posts");
            modelBuilder.Entity<Post>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Post>().HasOne(p => p.User).WithMany(u => u.Posts);
            modelBuilder.Entity<Post>().HasOne(p => p.Topic).WithMany(t => t.Posts);
        }

        #endregion Methods

        #region Properties

        //public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }

        #endregion Properties
    }
}
