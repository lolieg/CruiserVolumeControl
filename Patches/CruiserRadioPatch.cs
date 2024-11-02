using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;


namespace CruiserVolumeControl.Patches;

[HarmonyPatch(typeof(VehicleController))]
public class CruiserRadioPatch
{
    [HarmonyPatch("Update")]
    [HarmonyPostfix]
    private static void UpdatePostfix(VehicleController __instance)
    {
        __instance.radioAudio.volume = CruiserVolumeControl.Instance.cruiserVolumeConfigEntry.Value;
    }
}
