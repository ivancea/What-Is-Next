using System;
using System.Linq;
using WhatIsNext.Dtos;
using WhatIsNext.Model.Entities;

namespace WhatIsNext.Mappers
{
    public class GraphToGraphDtoMapping : IClassMapping<Graph, GraphDto>
    {
        public GraphDto Map(Graph input)
        {
            return new GraphDto()
            {
                Id = input.Id,
                Topic = input.Topic,
                Name = input.Name,
                Description = input.Description,
            };
        }
    }
}