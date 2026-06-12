using InstaClone.Domain.Entities;
using InstaClone.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ReplyComment> ReplyComments { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.UseSnakeCaseNamingConvention();

            builder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(u => u.Author)
                .HasForeignKey(p => p.AuthorId);

            builder.Entity<User>()
            .HasMany(u => u.Following)
            .WithMany(u => u.Followers)
            .UsingEntity<Dictionary<string, object>>(
                "user_followers",
                j => j.HasOne<User>().WithMany().HasForeignKey("follower_id"),
                j => j.HasOne<User>().WithMany().HasForeignKey("following_id")
            );

            builder.Entity<Post>()
                .HasMany(p => p.Likes)
                .WithOne(c => c.Post)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Comment>()
            .HasMany(c => c.ReplyComments)
            .WithOne(c => c.RepliedComment)
            .HasForeignKey(c => c.RepliedCommentId)
            .OnDelete(DeleteBehavior.NoAction);

            SeedData.Seed(builder);
        }
    }
}
