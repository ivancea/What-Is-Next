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
            };

            Concept externalLibraryConcept = new Concept
            {
                Name = "External library",
                Description = "External library",
            };

            stdConcept.Dependencies.Add(basicsConcept);
            externalLibraryConcept.Dependencies.Add(stdConcept);
            externalLibraryConcept.Dependencies.Add(basicsConcept);

            Graph graph = new Graph
            {
                Topic = "C++",
                Name = "Test C++ graph",
                Description = "Test graph",
                Concepts = new List<Concept>
                {
                    basicsConcept,
                    stdConcept,
                    externalLibraryConcept,
                }
            };

            winContext.Graphs.Add(graph);

            winContext.SaveChanges();
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
                .SingleOrDefault(g => g.Id == id);

            if (graph == null)
            {
                return null;
            }

            return graphToGraphDtoMapping.Map(graph);
        }

        public void AddGraph(GraphDto graphDto)
        {
            Graph graph = graphDtoToGraphMapping.Map(graphDto);

            winContext.Graphs.Add(graph);
            winContext.SaveChanges();
        }

        public void UpdateGraph(int id, GraphDto graphDto)
        {
            Graph actualGraph = winContext.Graphs
                .SingleOrDefault(g => g.Id == id);

            if (actualGraph == null)
            {
                return;
            }

            Graph graph = graphDtoToGraphMapping.Map(graphDto);

            graphUpdater.Update(actualGraph, graph);

            winContext.SaveChanges();
        }

        public void DeleteGraph(int id)
        {
            Graph graph = winContext.Graphs
                .SingleOrDefault(g => g.Id == id);

            if (graph != null)
            {
                winContext.Graphs.Remove(graph);
                winContext.SaveChanges();
            }
        }

        public ICollection<ConceptDto> ListConceptsByGraphId(int graphId)
        {
            return winContext.Concepts
                .Include(c => c.Graph)
                .Include(c => c.Dependencies)
                .Where(c => c.Graph.Id == graphId)
                .Select(conceptToConceptDtoMapping.Map)
                .ToList();
        }

        public ConceptDto GetConceptById(int graphId, int id)
        {
            return winContext.Concepts
                .Include(c => c.Graph)
                .Include(c => c.Dependencies)
                .Where(c => c.Id == id && c.Graph.Id == graphId)
                .Select(conceptToConceptDtoMapping.Map)
                .FirstOrDefault();
        }

        public void AddConcept(int graphId, ConceptDto conceptDto)
        {
            Graph graph = winContext.Graphs
                .SingleOrDefault(g => g.Id == graphId);

            Concept concept = conceptDtoToConceptMapping.Map(conceptDto);

            concept.Graph = graph;

            foreach (var dependency in winContext.Concepts
                .Where(c => c.Graph.Id == graphId)
                .Where(c => conceptDto.DependenciesIds.Contains(c.Id))
                .ToList())
            {
                concept.Dependencies.Add(dependency);
            }

            winContext.Concepts.Add(concept);
            winContext.SaveChanges();
        }

        public void UpdateConcept(int graphId, int id, ConceptDto conceptDto)
        {
            Concept actualConcept = winContext.Concepts
                .Include(c => c.Dependencies)
                .SingleOrDefault(c => c.Id == id && c.Graph.Id == graphId);

            if (actualConcept == null)
            {
                return;
            }

            Concept concept = conceptDtoToConceptMapping.Map(conceptDto);

            conceptUpdater.Update(actualConcept, concept);

            winContext.RemoveRange(actualConcept.Dependencies);

            foreach (var dependency in winContext.Concepts
                .Where(c => conceptDto.DependenciesIds.Contains(c.Id))
                .ToList())
            {
                actualConcept.Dependencies.Add(dependency);
            }

            winContext.SaveChanges();
        }

        public void DeleteConcept(int graphId, int id)
        {
            Concept concept = winContext.Concepts
                .SingleOrDefault(c => c.Id == id && c.Graph.Id == graphId);

            if (concept != null)
            {
                winContext.Concepts.Remove(concept);
                winContext.SaveChanges();
            }
        }
    }
}