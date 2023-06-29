using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BTH626.Models;

namespace MvcMovie.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<BTH626.Models.Sinhvien> Sinhvien { get; set; } = default!;

        public DbSet<BTH626.Models.Lophoc> Lophoc { get; set; } = default!;
    }
}
