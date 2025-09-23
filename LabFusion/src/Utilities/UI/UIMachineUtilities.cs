using Il2CppSLZ.Bonelab;
using Il2CppSLZ.Marrow.Interaction;
using Il2CppSLZ.Marrow.Warehouse;
using Il2CppSLZ.Marrow.Zones;
using Il2CppTMPro;
using LabFusion.Extensions;
using LabFusion.Marrow;
using LabFusion.Scene;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LabFusion.Utilities;

public static class UIMachineUtilities
{
    public static void CreateLaserCursor(Transform canvas, Transform uiPlane, Vector3 bounds)
    {
        LaserCursorUtilities.CreateLaserCursor((cursor) =>
        {
            cursor.transform.parent = canvas.parent;
            cursor.transform.SetLocalPositionAndRotation(Vector3Extensions.zero, QuaternionExtensions.identity);

            LaserCursor.CursorRegion region = new()
            {
                bounds = new Bounds(Vector3Extensions.zero, Vector3.Scale(bounds, canvas.lossyScale)),
                center = uiPlane,
            };

            cursor.regions = new LaserCursor.CursorRegion[] { region };

            FusionSceneManager.HookOnLevelLoad(() =>
            {
                if (cursor == null)
                {
                    return;
                }

                cursor.gameObject.SetActive(true);
            });
        });
    }

    public static void CreateUITrigger(GameObject canvas, GameObject trigger)
    {
        trigger.SetActive(false);
        trigger.layer = (int)MarrowLayers.EntityTrigger;
        var zone = trigger.AddComponent<Zone>();
        var zoneEvent = trigger.AddComponent<ZoneEvents>();

        zoneEvent._zone = zone;

        zoneEvent.onZoneEnter = new ZoneEvents.ZoneEventCallback();
        zoneEvent.onZoneEnterOneShot = new ZoneEvents.ZoneEventCallback();
        zoneEvent.onZoneExit = new ZoneEvents.ZoneEventCallback();

        var tagQuery = new TagQuery
        {
            BoneTag = MarrowBoneTagReferences.PlayerReference,
        };

        zoneEvent.activatorTags.Tags.Add(tagQuery);

        trigger.SetActive(true);
    }

    public static void OverrideFonts(Transform root)
    {
        foreach (var text in root.GetComponentsInChildren<TMP_Text>(true))
        {
            text.font = PersistentAssetCreator.Font;
        }
    }

    public static void AddClickEvent(this Button button, Action action)
    {
        button.onClick.AddListener((UnityAction)action);
    }
}