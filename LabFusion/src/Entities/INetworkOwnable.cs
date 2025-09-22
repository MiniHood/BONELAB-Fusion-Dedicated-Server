using LabFusion.Player;

namespace LabFusion.Entities;

public interface INetworkOwnable
{
    PlayerID OwnerID { get; }

    bool IsOwner { get; }

    bool IsOwnerLocked { get; }

    void SetOwner(PlayerID ownerId);

    void LockOwner();

    void UnlockOwner();

    void RemoveOwner();
}
