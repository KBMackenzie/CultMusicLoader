using HarmonyLib;
using System;
using System.Linq;
using CultMusicLoader.Menu;
using UnityEngine;
using CultMusicLoader.Sounds.Load;

namespace CultMusicLoader.Patches
{
    [HarmonyPatch]
    internal class BaseMusic
    {
        [HarmonyPatch(typeof(BiomeBaseManager), nameof(BiomeBaseManager.Start))]
        [HarmonyPrefix]
        static void AddMusic(BiomeBaseManager __instance)
        {
            GameObject Parent = __instance.gameObject;

            MusicMenu.SL = Parent.AddSoundLoader();
            
            // Add all music loaded
            foreach(string Song in Plugin.SongList)
            {
                try
                {
                    MusicMenu.SL.CreateSound(Song);
                    Plugin.myLogger.LogInfo($"Loaded audio track from file \"{Song}\"!");
                }
                catch (Exception)
                {
                    Plugin.myLogger.LogError($"Couldn't load audio track from file \"{Song}\"!");
                }
            }

            MusicMenu.SoundIndex = 0;
            MusicMenu.SL.PlayMusic(Plugin.SongList.First());
        }

        [HarmonyPatch(typeof(AudioManager), nameof(AudioManager.PlayMusic))]
        [HarmonyPrefix]
        static bool PatchAudioManager(ref string soundPath)
        {
            return soundPath != "event:/music/base/base_main";
        }
    }
}
