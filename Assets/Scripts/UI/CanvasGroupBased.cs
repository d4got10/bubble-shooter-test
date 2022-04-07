using UnityEngine;


namespace BubbleShooter.UI
{
    public class CanvasGroupBased : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;


        public void Enable()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Disable()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}