using IdentityCheck.Data;
using IdentityCheck.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentiyCheckTest.TestUtils
{
    public class DatabaseFixture : IDisposable
    {
        private readonly DbContextOptions<ApplicationDbContext> options;
        public DatabaseFixture()
        {
            options = TestDbOptions.Get();
            using (var context = new ApplicationDbContext(options))
            {
                SeedPosts(context);
                context.SaveChanges();
            }
        }

        public void Dispose()
        {
            using (var context = new ApplicationDbContext(options))
            {
                context.Posts.RemoveRange(context.Posts);
                context.SaveChanges();
            }
        }

        private void SeedPosts(ApplicationDbContext context)
        {
            context.Posts.AddRange(new List<Post>
            {
                new Post { Title = "Test" },             
            });
        }
    }
}
