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

        [HttpGet("graphs/{id}/concepts")]
        public ICollection<ConceptDto> ListConceptsByGraphId(int id)
        {
            return graphService.ListConceptsByGraphId(id);
        }
    }
}
