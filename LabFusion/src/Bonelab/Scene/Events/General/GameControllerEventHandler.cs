using Il2CppSLZ.Bonelab;
using LabFusion.Marrow.Scene;
using UnityEngine;

namespace LabFusion.Bonelab.Scene;

public class GameControllerEventHandler : LevelEventHandler
{
    public static BaseGameController GameController { get; set; }

    protected override void OnLevelLoaded()
    {
        GameController = GameObject.FindObjectOfType<BaseGameController>();
    }

    public static bool HasGameController => GameController != null;
}
