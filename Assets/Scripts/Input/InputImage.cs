using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BubbleShooter
{
    [RequireComponent(typeof(Image))]
    public class InputImage : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        public event System.Action PointerUp;
        public event System.Action<Vector2> PointerDown;

        public void OnDrag(PointerEventData eventData)
        {
            PointerDown?.Invoke(eventData.position);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            PointerDown?.Invoke(eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PointerUp?.Invoke();
        }
    }
}