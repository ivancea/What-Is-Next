using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WhatIsNext.Model.Entities
{
    public class ConceptDependency
    {
        public int ConceptId { get; set; }

        public int DependencyId { get; set; }

        public virtual Concept Concept { get; set; }

        public virtual Concept Dependency { get; set; }
    }
}
