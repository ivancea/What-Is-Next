using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatIsNext.Model.Entities;

namespace WhatIsNext.Model.Contexts
{
    public class WinContext : DbContext
    {
        public WinContext(DbContextOptions<WinContext> options) : base(options)
        {
        }
        
        public DbSet<Graph> Graphs { get; set; }
        
        public DbSet<Concept> Concepts { get; set; }
    }
}
