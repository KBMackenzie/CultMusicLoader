using CultMusicLoader.UI.Base;
using CultMusicLoader.Sounds.Load;
using CultMusicLoader.UI.Helpers;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace CultMusicLoader.Menu
{
    public class MusicMenu : UIMenuBase
    {
        GameObject musicContainer, menuBox;
        GameObject trackName, arrowLeft, arrowRight, playPause, UIVinyl;

        bool isCultBase, pauseVinyl;

        public static SoundLoader SL;
        public static int SoundIndex;
        public static List<string> SongList => Plugin.SongList;
        public static string CurrentSong => SongList[SoundIndex];

        static Dictionary<string, Sprite> MenuSprites = new Dictionary<string, Sprite>()
            {
                { "BaseUI",     Helpers.MakeSprite(Properties.Resources.BaseUI)     },
                { "ArrowL" ,    Helpers.MakeSprite(Properties.Resources.ArrowL)     },
                { "ArrowR" ,    Helpers.MakeSprite(Properties.Resources.ArrowR)     },
                { "UIPause" ,   Helpers.MakeSprite(Properties.Resources.UIPause)    },
                { "UIPlay" ,    Helpers.MakeSprite(Properties.Resources.UIPlay)     },
                { "UIVinyl" ,   Helpers.MakeSprite(Properties.Resources.UIVinyl)    },
            };

        public override void InitializeMenu(Transform parent)
        {
            isCultBase = SceneManager.GetActiveScene().name == "Base Biome 1";
            if (!isCultBase) return;

            int xPos = Plugin.hasWeaponSelector ? 1700 : 1900;
            int yPos = Plugin.hasWeaponSelector ? 413 : 1000;

            musicContainer = UIManager.CreateUIObject(name: "MusicContainer")
                .AttachToParent(parent)
                .ChangeScale(x: 1f, y: 1f)
                .ChangePosition(x: xPos, y: yPos);
            musicContainer.AddComponent<RectTransform>();

            CreateMenuObjects();
            CreateButtons();

            UpdateText();
            UpdateAfterPlay();
            StartCoroutine(VinylSpin());
        }

        void CreateMenuObjects()
        {
            // Scale menu objects down
            float scaleDown = 0.14f;

            menuBox = musicContainer.CreateChild(name: "MusicMenu")
                .AttachImage(MenuSprites["BaseUI"])
                .ChangeImageOpacity(80)
                .ChangeScale(x: scaleDown, y: scaleDown)
                .ChangePosition(x: -450, y: -147)
                .MakeDraggable(musicContainer);

            trackName = musicContainer.CreateChild(name: "TrackName")
                .AddText("None", fontSize: 40)
                .ChangeScale(x: 1, y: 1)
                .ChangePosition(x: -603, y: -137);
            var rect = trackName.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(314, 0);

            arrowLeft = musicContainer.CreateChild(name: "ArrowL")
                .AttachImage(MenuSprites["ArrowL"])
                .ChangeScale(x: scaleDown, y: scaleDown)
                .ChangePosition(x: -800, y: -135);

            arrowRight = musicContainer.CreateChild(name: "ArrowR")
                .AttachImage(MenuSprites["ArrowR"])
                .ChangeScale(x: scaleDown, y: scaleDown)
                .ChangePosition(x: -413, y: -135);

            UIVinyl = musicContainer.CreateChild(name: "UIVinyl")
                .AttachImage(MenuSprites["UIVinyl"])
                .ChangeScale(x: scaleDown, y: scaleDown)
                .ChangePosition(x: -220, y: -135);

            playPause = musicContainer.CreateChild(name: "UIPause")
                .AttachImage(SL.IsPaused(CurrentSong) ? MenuSprites["UIPlay"] : MenuSprites["UIPause"])
                .ChangeScale(x: scaleDown, y: scaleDown)
                .ChangePosition(x: -220, y: -135);
        }

        void CreateButtons()
        {
            UIButton PlayPrev = arrowLeft.MakeButton();
            PlayPrev.OnClick += () =>
            {
                StopCurrent();
                PrevTrack();
                PlayCurrent();
                UpdateAfterPlay();
            };
            PlayPrev.OnCursorEnter += () => HoverEffect(ref arrowLeft, true);
            PlayPrev.OnCursorExit += () => HoverEffect(ref arrowLeft, false);

            UIButton PlayNext = arrowRight.MakeButton();
            PlayNext.OnClick += () =>
            {
                StopCurrent();
                NextTrack();
                PlayCurrent();
                UpdateAfterPlay();
            };
            PlayNext.OnCursorEnter += () => HoverEffect(ref arrowRight, true);
            PlayNext.OnCursorExit += () => HoverEffect(ref arrowRight, false);

            UIButton Pause = playPause.MakeButton();
            Pause.OnClick += () =>
            {
                SL.Pause(CurrentSong, !SL.IsPaused(CurrentSong));
                UpdateAfterPlay();
            };
            Pause.OnCursorEnter += () => HoverEffect(ref playPause, true);
            Pause.OnCursorExit += () => HoverEffect(ref playPause, false);
        }

        void StopCurrent() => SL.Stop(CurrentSong);
        void PlayCurrent() => SL.PlayMusic(CurrentSong);

        void NextTrack()
        {
            SoundIndex++;
            if (SoundIndex > SongList.Count - 1) SoundIndex = 0;
        }

        void PrevTrack()
        {
            SoundIndex--;
            if (SoundIndex < 0) SoundIndex = SongList.Count - 1;
        }

        void UpdateText()
        {
            string fileName = CurrentSong;
            string name = fileName.Substring(0, fileName.Length - "_cult.mp3".Length);

            if (name.Length >= 16)
            {
                // StringBuilder sb = new StringBuilder();
                // name = sb.Append(name.Substring(0, 12)).Append("...").ToString();

                name = name.Substring(0, 12) + "...";
            }
            trackName.EditText(name);
        }

        void UpdateAfterPlay()
        {
            pauseVinyl = SL.IsPaused(CurrentSong);
            UpdateText();
            playPause.EditImage(pauseVinyl ? MenuSprites["UIPlay"] : MenuSprites["UIPause"]);
        }

        void HoverEffect (ref GameObject obj, bool isHovering, int opacity = 50)
        {
            int changeOpacity = isHovering ? opacity : 100;
            obj.ChangeImageOpacity(changeOpacity);
        }

        IEnumerator VinylSpin(float duration = 3f)
        {
            Vector3 startAngles = UIVinyl.transform.eulerAngles;
            float startZ = startAngles.z;
            float endRotation = startAngles.z + 360.0f;

            while (true)
            {
                if (pauseVinyl)
                {
                    yield return null;
                    continue;
                }

                if (startZ >= endRotation) startZ = 0.0f;
                float time = Time.unscaledDeltaTime / duration;
                startZ = Mathf.Lerp(startAngles.z, endRotation, time) % 360f;

                UIVinyl.ChangeRotation(x:startAngles.x, y:startAngles.y, z: startZ);
                yield return null;
            }
        }

        void Update()
        {
            if (!isCultBase) return;

            if (Input.GetKeyDown("g"))
            {
                StopCurrent();
                NextTrack();
                PlayCurrent();
                UpdateAfterPlay();
            }
        }
    }
}
