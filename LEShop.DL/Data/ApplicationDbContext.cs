using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEShop.DL.Models;
using Microsoft.EntityFrameworkCore;


namespace LEShop.DL.Data
{
    public class ApplicationDbContext :DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {


        }

    }
}
