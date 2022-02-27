using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedData.Models;

namespace WebApiApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<GameResult> GameResults { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
