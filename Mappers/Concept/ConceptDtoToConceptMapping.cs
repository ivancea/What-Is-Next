using System;
using System.Linq;
using WhatIsNext.Dtos;
using WhatIsNext.Model.Entities;

namespace WhatIsNext.Mappers
{
    public class ConceptDtoToConceptMapping : IClassMapping<ConceptDto, Concept>
    {
        public Concept Map(ConceptDto input)
        {
            return new Concept()
            {
                Name = input.Name,
                Description = input.Description,
                Level = input.Level,
            };
        }
    }
}