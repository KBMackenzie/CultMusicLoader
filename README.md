A Cult of the Lamb mod that lets you load custom music for the cult base, and adds a little music selection menu to the game's pause menu to choose what track should play.

The little music selection menu is **draggable** and looks like this:

![Music Selection Menu](https://i.imgur.com/E2nc5Nq.gif)

You can easily add your own track by putting an audio file anywhere inside of the `BepInEx/plugins` folder. The audio file must meet certain criteria though:
1. The file's name **must end in** `_cult.mp3` (or `_cult.wav` / `_cult.ogg`).
2. The only supported audio formats, as you might have guessed, are MP3, WAV and OGG. 

An example of a valid name for an audio track is `Example Music_cult.mp3`.

You can navigate through the music tracks by clicking the arrows. 
You can also navigate through them with your **keyboard** by pressing **G**.

## Installation
This mod’s only dependency is BepInEx.

#### Installation (Mod Manager)
1. Download and install [r2modman](https://thunderstore.io/package/ebkr/r2modman/) or the [Thunderstore Mod Manager](https://www.overwolf.com/app/Thunderstore-Thunderstore_Mod_Manager).
2. Install this mod and all of its dependencies with the help of the mod manager! 

#### Installation (Manual)
1. Download and install BepInEx.
    1. If you're downloading it from [its Github page](https://github.com/BepInEx/BepInEx/releases), follow [this installation guide](https://docs.bepinex.dev/articles/user_guide/installation/index.html#where-to-download-bepinex).
    2. If you're downloading ["BepInExPack CultOfTheLamb" from Thunderstore](https://cult-of-the-lamb.thunderstore.io/package/BepInEx/BepInExPack_CultOfTheLamb/), follow the manual installation guide on the Thunderstore page itself. This one comes with a preconfigured `BepInEx.cfg` file, so it's advised you download this one.
2. Find the `BepInEx/plugins` folder.
3. Place the contents of **"CultMusicLoader.zip"** in a new folder within the plugins folder.

## How To Use
As mentioned, all you have to do is put your audio files inside of the `BepInEx/plugins` folder and this mod will find and load them for you, so long as they meet the following criteria:
1. The file's name **must end in** `_cult.mp3` (or `_cult.wav` / `_cult.ogg`).
2. The only supported audio formats, as you might have guessed, are MP3, WAV and OGG. 

The track name displayed in the music menu is the name of the file minus both the extension and the `_cult` part. If your track name is too long, it'll be cut off.

If you find any bugs or issues, you can contact me on Discord! `kelly betty#7936`
You're encouraged you to do so! This mod is my way of testing my contributions to the API, so telling me about any bugs you find helps!

## Changelog
- 1.0.0 -- Initial upload.