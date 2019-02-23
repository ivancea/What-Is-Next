namespace WhatIsNext.Mappers
{
    public interface IEntityUpdater<TEntity>
    {
        void Update(TEntity target, TEntity source);
    }
}