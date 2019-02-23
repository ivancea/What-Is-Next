using System.Collections.Generic;
using WhatIsNext.Dtos;

namespace WhatIsNext.Services
{
    public interface IGraphService
    {
        ICollection<string> ListTopics();

        ICollection<GraphDto> ListGraphsByTopic(string topic);

        GraphDto GetGraphById(int id);

        ConceptDto GetConceptById(int id);

        ICollection<ConceptDto> ListConceptsByGraphId(int graphId);
        
        void InsertTestData();
    }
}