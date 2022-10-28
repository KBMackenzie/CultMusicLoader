using BepInEx;
using HarmonyLib;
using System.Linq;
using BepInEx.Logging;
using BepInEx.Bootstrap;
using CultMusicLoader.Menu;
using CultMusicLoader.UI.Helpers;
using System.Collections.Generic;
using System.IO;

namespace CultMusicLoader
{
    [BepInPlugin(PluginGuid, PluginName, PluginVer)]
    [BepInDependency("kel.cotl.weaponselector", BepInDependency.DependencyFlags.SoftDependency)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "kel.cotl.cultmusicloader";
        public const string PluginName = "Cult Music Loader";
        public const string PluginVer = "1.0.0";

        internal static ManualLogSource myLogger;

        public static List<string> SongList = new List<string>();

        public static bool hasWeaponSelector;

        private void Awake()
        {
            myLogger = Logger; // Make log source

            Logger.LogInfo($"Loaded {PluginName}!");

            string[] files = Menu.Helpers.FindMusicFiles();

            // No patches are run if no songs are found.
            if (files == null) return;

            files.ToList().ForEach(x => SongList.Add(Path.GetFileName(x)));

            Harmony harmony = new Harmony("kel.harmony.cultmusicloader");
            harmony.PatchAll();

            UIHelpers.AddToPauseMenu<MusicMenu>();

            // Make sure the menu doesn't overlap with Weapon Selector's menu
            string weaponSelectorGUID = "kel.cotl.weaponselector";
            hasWeaponSelector = Chainloader.PluginInfos.Any(x => x.Value.Metadata.GUID.Equals(weaponSelectorGUID));
        }
    }
}