using UnityEngine;
using TMPro;
using Image = UnityEngine.UI.Image;
using CultMusicLoader.UI.Base;
using CultMusicLoader.Menu;
using CultMusicLoader.API;

namespace CultMusicLoader.UI.Helpers;
public static class GameObjectExtensions
{
    // Extension methods for GameObjects.
    public static GameObject AttachToParent(this GameObject obj, Transform parent)
    {
        obj.transform.SetParent(parent);
        return obj;
    }

    public static GameObject ChangePosition(this GameObject obj,
        float? x = null, float? y = null, float? z = null)
    {
        x ??= obj.transform.localRotation.x;
        y ??= obj.transform.localRotation.y;
        z ??= obj.transform.localRotation.z;

        obj.transform.localPosition = new Vector3((float)x, (float)y, (float)z);
        return obj;
    }

    public static GameObject ChangeScale(this GameObject obj,
        float? x = null, float? y = null, float? z = null)
    {
        x ??= obj.transform.localScale.x;
        y ??= obj.transform.localScale.y;
        z ??= obj.transform.localScale.z;

        obj.transform.localScale = new Vector3((float)x, (float)y, (float)z);
        return obj;
    }
    public static GameObject ChangeRotation(this GameObject obj,
        float? x = null, float? y = null, float? z = null)
    {
        x ??= obj.transform.eulerAngles.x;
        y ??= obj.transform.eulerAngles.y;
        z ??= obj.transform.eulerAngles.z;

        obj.transform.Rotate(new Vector3((float)x, (float)y, (float)z));
        return obj;
    }

    public static GameObject CreateChild(this GameObject obj, string name)
    {
        Transform parent = obj.transform;
        return UIManager.CreateUIObject(name, parent);
    }

    public static GameObject MakeDraggable(this GameObject obj)
    {
        var script = obj.AddComponent<UIBehaviourHelpers.DraggableUIObject>();
        return obj;
    }

    public static GameObject MakeDraggable(this GameObject obj, in RectTransform dragRect)
    {
        var script = obj.AddComponent<UIBehaviourHelpers.DraggableUIObject>();
        script.dragRectTransform = dragRect;
        return obj;
    }

    public static GameObject MakeDraggable(this GameObject obj, in GameObject dragObj)
    {
        var script = obj.AddComponent<UIBehaviourHelpers.DraggableUIObject>();
        var dragRect = dragObj.GetComponent<RectTransform>();
        script.dragRectTransform = dragRect;
        return obj;
    }

    public static UIButton MakeButton(this GameObject obj)
    {
        var button = obj.AddComponent<UIButton>();
        return button;
    }

    public static GameObject AddText(this GameObject obj, string message, float fontSize = 10f, TextAlignmentOptions alignment = TextAlignmentOptions.Center)
    {
        TextMeshProUGUI textMesh = obj.AddComponent<TextMeshProUGUI>();
        textMesh.font = FontHelpers.GetAnyFont;
        textMesh.fontSize = fontSize;
        textMesh.text = message;
        textMesh.alignment = alignment;
        return obj;
    }

    public static GameObject EditText(this GameObject obj, string message)
    {
        TextMeshProUGUI textMesh = obj.GetComponent<TextMeshProUGUI>();
        if(textMesh == null)
        {
            Plugin.myLogger.LogWarning("EditText: TextMeshProUGUI component not found.");
            return obj;
        }
        textMesh.text = message;
        return obj;
    }


    // -- IMAGE-RELATED --
    // I wanted opacity to be an int from 0-100 because I think most people are more used to terms like "100% opacity" and "50% opacity" than "1f opacity" and "0.5f opacity".
    // I can change it if necessary (or make a variation of this method that takes a float).

    public static GameObject AttachImage(this GameObject obj, Sprite sprite, int opacity = 100)
    {
        Image img = obj.AddComponent<Image>();
        img.sprite = sprite;
        img.SetNativeSize();
        img.preserveAspect = true;

        if (opacity < 100 && opacity >= 0)
        {
            Color color = img.color;
            color.a = (float)opacity / 100f;
            img.color = color;
        }
        return obj;
    }

    public static GameObject AttachImage(this GameObject obj, string imagePath, int opacity = 100)
    {
        Sprite sprite = LoadTexture.MakeSprite(imagePath);

        Image img = obj.AddComponent<Image>();
        img.sprite = sprite;
        img.SetNativeSize();
        img.preserveAspect = true;

        Mathf.Clamp(opacity, 0, 100);
        Color color = img.color;
        color.a = (float)opacity / 100f;
        img.color = color;

        return obj;
    }

    public static GameObject EditImage(this GameObject obj, Sprite sprite)
    {
        Image img = obj.GetComponent<Image>();

        if(img == null)
        {
            Plugin.myLogger.LogError("EditImage: Image component not found.");
            return obj;
        }

        img.sprite = sprite;
        return obj;
    }

    public static GameObject EditImage(this GameObject obj, string imagePath)
    {
        Sprite sprite = LoadTexture.MakeSprite(imagePath);

        Image img = obj.GetComponent<Image>();
        if (img == null)
        {
            Plugin.myLogger.LogError("EditImage: Image component not found.");
            return obj;
        }
        img.sprite = sprite;

        return obj;
    }

    public static GameObject ChangeImageOpacity(this GameObject obj, int opacity = 100)
    {
        Image img = obj.GetComponent<Image>();
        if (img == null)
        {
            Plugin.myLogger.LogError("ChangeOpacity: Image component not found.");
            return obj;
        }

        Mathf.Clamp(opacity, 0, 100);
        Color color = img.color;
        color.a = (float)opacity / 100f;
        img.color = color;

        return obj;
    }

}
