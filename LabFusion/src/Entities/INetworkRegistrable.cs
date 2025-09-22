namespace LabFusion.Entities;

public interface INetworkRegistrable
{
    ushort ID { get; }

    ushort QueueID { get; }

    bool IsRegistered { get; }

    bool IsQueued { get; }

    bool IsDestroyed { get; }

    void Register(ushort id);

    void Queue(ushort queuedId);

    void Unregister();
}