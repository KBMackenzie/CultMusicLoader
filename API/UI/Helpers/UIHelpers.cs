﻿using UnityEngine;
using CultMusicLoader.UI.Base;
using CultMusicLoader.UI.Patches;

namespace CultMusicLoader.UI.Helpers;
public static class UIHelpers
{
    // UI Layer
    static LayerMask _UILayer = LayerMask.NameToLayer("UI");
    public static LayerMask UILayer => _UILayer;

    // UI Helper methods:
    public static void AddToPauseMenu<T>() where T : UIMenuBase
    {
        UIPatches.PauseMenuQueue.Add(typeof(T));
    }
    public static void AddToStartMenu<T>() where T : UIMenuBase
    {
        UIPatches.StartMenuQueue.Add(typeof(T));
    }

    public static GameObject CreateUIObject(string name)
    {
        GameObject obj = new GameObject(name);
        obj.layer = UILayer;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        return obj;
    }

    public static GameObject CreateUIObject(string name, Transform parent)
    {
        GameObject obj = new GameObject(name);
        obj.layer = UILayer;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.transform.SetParent(parent);
        return obj;
    }
}