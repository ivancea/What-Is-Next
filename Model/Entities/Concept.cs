using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhatIsNext.Model.Entities
{
    public class Concept
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<ConceptDependency> Dependencies { get; set; }

        public virtual ICollection<ConceptDependency> DependantConcepts { get; set; }

        [Required]
        public virtual Graph Graph { get; set; }
    }
}
