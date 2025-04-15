using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyToDoAppSQL.Models;

namespace MyToDoAppSQL.Data
{
    public class MyToDoAppSQLContext : DbContext
    {
        public MyToDoAppSQLContext (DbContextOptions<MyToDoAppSQLContext> options)
            : base(options)
        {
        }

        public DbSet<MyToDoAppSQL.Models.Item> Item { get; set; } = default!;
    }
}
