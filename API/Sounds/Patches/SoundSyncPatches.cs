using HarmonyLib;
using Lamb.UI.SettingsMenu;
using CultMusicLoader.Sounds.Load;

namespace CultMusicLoader.Sounds.Patches;

[HarmonyPatch]
internal class SoundSyncPatches
{
    [HarmonyPatch(typeof(AudioSettings), nameof(AudioSettings.OnMasterVolumeChanged))]
    [HarmonyPostfix]
    private static void SyncMaster()
    {
        SoundLoader.AllInstances.ForEach(x => x.SyncAllVolume());
    }

    [HarmonyPatch(typeof(AudioSettings), nameof(AudioSettings.OnMusicVolumeChanged))]
    [HarmonyPostfix]
    private static void SyncMusic()
    {
        SoundLoader.AllInstances.ForEach(x => x.SyncAllVolume());
    }
}
