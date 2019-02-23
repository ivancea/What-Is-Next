using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WhatIsNext.Dtos
{
    public class GraphDto
    {
        public int Id { get; set; }

        [Required]
        public string Topic { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}