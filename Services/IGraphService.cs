using System.Collections.Generic;
using WhatIsNext.Dtos;

namespace WhatIsNext.Services
{
    public interface IGraphService
    {
        void InsertTestData();

        ICollection<string> ListTopics();

        ICollection<GraphDto> ListGraphsByTopic(string topic);

        GraphDto GetGraphById(int id);

        void AddGraph(GraphDto graphDto);

        void UpdateGraph(int id, GraphDto graphDto);

        void DeleteGraph(int id);

        ICollection<ConceptDto> ListConceptsByGraphId(int graphId);

        ConceptDto GetConceptById(int graphId, int id);

        void AddConcept(int graphId, ConceptDto conceptDto);

        void UpdateConcept(int graphId, int id, ConceptDto conceptDto);

        void DeleteConcept(int graphId, int id);
    }
}