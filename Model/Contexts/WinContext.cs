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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ConceptDependency>()
                .HasKey(d => new { d.ConceptId, d.DependencyId });
            
            builder.Entity<ConceptDependency>()
                .HasOne(d => d.Concept)
                .WithMany(c => c.Dependencies)
                .HasForeignKey(d => d.ConceptId);
            
            builder.Entity<ConceptDependency>()
                .HasOne(d => d.Dependency)
                .WithMany(c => c.DependantConcepts)
                .HasForeignKey(d => d.DependencyId);
        }
    }
}
