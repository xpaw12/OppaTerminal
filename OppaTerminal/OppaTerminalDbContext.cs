using Microsoft.EntityFrameworkCore;
using OppaTerminal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OppaTerminal
{
    public class OppaTerminalDbContext : DbContext
    {
        public OppaTerminalDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; }//
    }
}
