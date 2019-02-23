using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WhatIsNext.Services;

namespace WhatIsNext.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly IGraphService graphService;

        public TestController(IGraphService graphService)
        {
            this.graphService = graphService;
        }

        [HttpGet("[action]")]
        public void Test()
        {
            graphService.test();
        }
    }
}
