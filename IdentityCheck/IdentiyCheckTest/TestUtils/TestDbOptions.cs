using IdentityCheck.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace IdentiyCheckTest.TestUtils
{
    class TestDbOptions
    {
        public static DbContextOptions<ApplicationDbContext> Get()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "blog-testdb")
                .Options;
        }
    }
}
