using BepInEx;
using System.IO;
using System.Linq;
using UnityEngine;

namespace CultMusicLoader.API
{
    internal class LoadTexture
    {
        public static Sprite MakeSprite(string path)
        {
            string[] files = Directory.GetFiles(Paths.PluginPath, path, SearchOption.AllDirectories);
            string img = files.FirstOrDefault();
            if (img == null) return new Sprite();
            byte[] data = File.ReadAllBytes(img);

            Texture2D tex = new Texture2D(1, 1);
            tex.filterMode = FilterMode.Point;
            tex.LoadImage(data);
            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }
    }
}
