using BepInEx;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace CultMusicLoader.Menu
{
    internal static class Helpers
    {
        public static string[] FindMusicFiles()
        {
            Regex reg = new Regex(@".*_cult\.(mp3|wav|ogg)");
            string[] files = Directory.GetFiles(Paths.PluginPath, "*_cult.???", SearchOption.AllDirectories);

            string[] music = files.Length > 0 ? files.Where(x => reg.IsMatch(x))?.ToArray() : null;

            if(music == null || music.Length == 0)
            {
                Plugin.myLogger.LogWarning("No audio tracks found. Make sure your audio track ends in \"_cult\" plus the file extension!");
                return null;
            }

            return music;
        }

        public static Sprite MakeSprite(byte[] array, FilterMode filter = FilterMode.Bilinear)
        {
            Texture2D tex = new Texture2D(1, 1);
            tex.filterMode = filter;
            ImageConversion.LoadImage(tex, array);

            Rect texRect = new Rect(0, 0, tex.width, tex.height);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            return Sprite.Create(tex, texRect, pivot);
        }
    }
}
