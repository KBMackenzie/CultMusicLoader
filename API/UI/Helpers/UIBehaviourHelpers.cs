using UnityEngine;
using UnityEngine.EventSystems;

namespace CultMusicLoader.UI.Helpers;
internal class UIBehaviourHelpers
{
    public class DraggableUIObject : MonoBehaviour, IDragHandler
    {
        public RectTransform dragRectTransform;
        public Canvas canvas;

        void Start()
        {
            dragRectTransform ??= GetComponent<RectTransform>();
            canvas ??= GetComponentInParent<Canvas>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
}
