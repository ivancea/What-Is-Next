using WhatIsNext.Model.Entities;

namespace WhatIsNext.Mappers
{
    public class GraphUpdater : IEntityUpdater<Graph>
    {
        public void Update(Graph target, Graph source)
        {
            target.Topic = source.Topic;
            target.Name = source.Name;
            target.Description = source.Description;
        }
    }
}