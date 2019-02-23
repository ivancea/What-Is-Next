using System;
using System.Linq;
using WhatIsNext.Dtos;
using WhatIsNext.Model.Entities;

namespace WhatIsNext.Mappers
{
    public class GraphDtoToGraphMapping : IClassMapping<GraphDto, Graph>
    {
        public Graph Map(GraphDto input)
        {
            return new Graph()
            {
                Topic = input.Topic,
                Name = input.Name,
                Description = input.Description,
            };
        }
    }
}