using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhatIsNext.Dtos
{
    public class ConceptDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public ICollection<int> DependenciesIds { get; set; }

        public int GraphId { get; set; }
    }
}