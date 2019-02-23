using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WhatIsNext.Dtos;
using WhatIsNext.Mappers;
using WhatIsNext.Model.Contexts;
using WhatIsNext.Model.Entities;

namespace WhatIsNext.Services
{
    public class GraphService : IGraphService
    {
        private readonly WinContext winContext;
        private readonly IClassMapping<Graph, GraphDto> graphToGraphDtoMapping;
        private readonly IClassMapping<GraphDto, Graph> graphDtoToGraphMapping;
        private readonly IEntityUpdater<Graph> graphUpdater;
        private readonly IClassMapping<Concept, ConceptDto> conceptToConceptDtoMapping;
        private readonly IClassMapping<ConceptDto, Concept> conceptDtoToConceptMapping;
        private readonly IEntityUpdater<Concept> conceptUpdater;

        public GraphService(
            WinContext winContext,
            IClassMapping<Graph, GraphDto> graphToGraphDtoMapping,
            IClassMapping<GraphDto, Graph> graphDtoToGraphMapping,
            IEntityUpdater<Graph> graphUpdater,
            IClassMapping<Concept, ConceptDto> conceptToConceptDtoMapping,
            IClassMapping<ConceptDto, Concept> conceptDtoToConceptMapping,
            IEntityUpdater<Concept> conceptUpdater
        )
        {
            this.winContext = winContext;
            this.graphToGraphDtoMapping = graphToGraphDtoMapping;
            this.graphDtoToGraphMapping = graphDtoToGraphMapping;
            this.graphUpdater = graphUpdater;
            this.conceptToConceptDtoMapping = conceptToConceptDtoMapping;
            this.conceptDtoToConceptMapping = conceptDtoToConceptMapping;
            this.conceptUpdater = conceptUpdater;
        }

        public ICollection<string> ListTopics()
        {
            return winContext.Graphs
                .Select(g => g.Topic)
                .Distinct()
                .ToList();
        }

        public ICollection<GraphDto> ListGraphsByTopic(string topic)
        {
            return winContext.Graphs
                .Where(g => g.Topic == topic)
                .Select(graphToGraphDtoMapping.Map)
                .ToList();
        }

        public GraphDto GetGraphById(int id)
        {
            Graph graph = winContext.Graphs
                .Find(id);

            if (graph == null) {
                return null;
            }

            return graphToGraphDtoMapping.Map(graph);
        }

        public ConceptDto GetConceptById(int id)
        {
            Concept concept = winContext.Concepts
                .Find(id);

            if (concept == null) {
                return null;
            }

            return conceptToConceptDtoMapping.Map(concept);
        }

        public ICollection<ConceptDto> ListConceptsByGraphId(int graphId)
        {
            return winContext.Concepts
                .Include(c => c.Graph)
                .Where(c => c.Graph.Id == graphId)
                .Select(conceptToConceptDtoMapping.Map)
                .ToList();
        }

        public void InsertTestData()
        {
            Concept basicsConcept = new Concept
            {
                Name = "Syntax",
                Description = "Language syntax",
            };

            Concept stdConcept = new Concept
            {
                Name = "Standard library",
                Description = "Standard library",
                Dependencies = new List<Concept>
                {
                    basicsConcept
                }
            };

            Graph graph = new Graph {
                Topic = "C++",
                Name = "Test C++ graph",
                Description = "Test graph",
                Concepts = new List<Concept>
                {
                    basicsConcept,
                    stdConcept
                }
            };

            winContext.Graphs.Add(graph);
            
            winContext.SaveChanges();
        }
    }
}