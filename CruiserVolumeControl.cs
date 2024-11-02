using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LobbyCompatibility.Attributes;
using LobbyCompatibility.Enums;
using LethalConfig;
using LethalConfig.ConfigItems;
using LethalConfig.ConfigItems.Options;
using BepInEx.Configuration;


namespace CruiserVolumeControl;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("BMX.LobbyCompatibility", BepInDependency.DependencyFlags.HardDependency)]
[BepInDependency("ainavt.lc.lethalconfig")]
[LobbyCompatibility(CompatibilityLevel.ClientOnly, VersionStrictness.None)]
public class CruiserVolumeControl : BaseUnityPlugin
{
    public static CruiserVolumeControl Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger { get; private set; } = null!;
    internal static Harmony? Harmony { get; set; }
    public ConfigEntry<float> cruiserVolumeConfigEntry;

    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;

        cruiserVolumeConfigEntry = Config.Bind("Volume", "Cruiser Volume", 0.3f, "Set the volume of the Cruiser's Radio");

        var volumeSlider = new FloatSliderConfigItem(cruiserVolumeConfigEntry, new FloatSliderOptions
        {
            Min = 0,
            Max = 1,
            RequiresRestart = false
        });

        LethalConfigManager.AddConfigItem(volumeSlider);

        Patch();

        Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
    }

    internal static void Patch()
    {
        Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

        Logger.LogDebug("Patching...");

        Harmony.PatchAll();

        Logger.LogDebug("Finished patching!");
    }

    internal static void Unpatch()
    {
        Logger.LogDebug("Unpatching...");

        Harmony?.UnpatchSelf();

        Logger.LogDebug("Finished unpatching!");
    }
}
