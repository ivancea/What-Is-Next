using System;
using System.Linq;
using WhatIsNext.Dtos;
using WhatIsNext.Model.Entities;

namespace WhatIsNext.Mappers
{
    public class ConceptToConceptDtoMapping : IClassMapping<Concept, ConceptDto>
    {
        public ConceptDto Map(Concept input)
        {
            return new ConceptDto()
            {
                Id = input.Id,
                Name = input.Name,
                Description = input.Description,
                Level = input.Level,
                DependenciesIds = input.Dependencies.Select(d => d.Id).ToList(),
                GraphId = input.Graph.Id
            };
        }
    }
}