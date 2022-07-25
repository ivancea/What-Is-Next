using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatIsNext.Dtos;
using WhatIsNext.Services;

namespace WhatIsNext.Controllers
{
    [ApiController]
    [Route("api")]
    public class GraphController : Controller
    {
        private readonly IGraphService graphService;

        public GraphController(IGraphService graphService)
        {
            this.graphService = graphService;
        }

        [HttpGet("test")]
        public void InsertTestData()
        {
            graphService.InsertTestData();
        }

        [HttpGet("topics")]
        public ICollection<string> ListTopics()
        {
            return graphService.ListTopics();
        }

        [HttpGet("topics/{topic}/graphs")]
        public ICollection<GraphDto> ListGraphsByTopic(string topic)
        {
            return graphService.ListGraphsByTopic(topic);
        }

        [HttpGet("graphs/{id}")]
        public GraphDto GetGraphById(int id)
        {
            return graphService.GetGraphById(id);
        }

        [HttpPost("graphs")]
        public void AddGraph([FromBody] GraphDto graphDto)
        {
            graphService.AddGraph(graphDto);
        }

        [HttpPut("graphs/{id}")]
        public void UpdateGraph(int id, [FromBody] GraphDto graphDto)
        {
            graphService.UpdateGraph(id, graphDto);
        }

        [HttpDelete("graphs/{id}")]
        public void DeleteGraph(int id)
        {
            graphService.DeleteGraph(id);
        }

        [HttpGet("graphs/{graphId}/concepts")]
        public ICollection<ConceptDto> ListConceptsByGraphId(int graphId)
        {
            return graphService.ListConceptsByGraphId(graphId);
        }

        [HttpGet("graphs/{graphId}/concepts/{id}")]
        public ConceptDto GetConceptById(int graphId, int id)
        {
            return graphService.GetConceptById(graphId, id);
        }

        [HttpPost("graphs/{graphId}/concepts")]
        public void AddConcept(int graphId, [FromBody] ConceptDto conceptDto)
        {
            graphService.AddConcept(graphId, conceptDto);
        }

        [HttpPut("graphs/{graphId}/concepts/{id}")]
        public void UpdateConcept(int graphId, int id, [FromBody] ConceptDto conceptDto)
        {
            graphService.UpdateConcept(graphId, id, conceptDto);
        }

        [HttpDelete("graphs/{graphId}/concepts/{id}")]
        public void DeleteConcept(int graphId, int id)
        {
            graphService.DeleteConcept(graphId, id);
        }
    }
}
