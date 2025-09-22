namespace LabFusion.Entities;

public class EntityUpdateList<TUpdatable>
{
    private readonly HashSet<TUpdatable> _entities = new();
    public HashSet<TUpdatable> Entities => _entities;

    public void Register(TUpdatable entity)
    {
        if (_entities.Contains(entity))
        {
            return;
        }

        _entities.Add(entity);
    }

    public void Unregister(TUpdatable entity)
    {
        _entities.Remove(entity);
    }
}
