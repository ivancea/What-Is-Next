using WhatIsNext.Model.Entities;

namespace WhatIsNext.Mappers
{
    public class ConceptUpdater : IEntityUpdater<Concept>
    {
        public void Update(Concept target, Concept source)
        {
            target.Name = source.Name;
            target.Description = source.Description;
            target.Level = source.Level;
        }
    }
}