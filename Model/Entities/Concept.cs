using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WhatIsNext.Model.Enums;

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

        [Required]
        public ConceptLevel Level { get; set; }

        public virtual ICollection<Concept> Dependencies { get; } = new List<Concept>();

        public virtual ICollection<Concept> DependantConcepts { get; } = new List<Concept>();

        [Required]
        public virtual Graph Graph { get; set; }
    }
}
