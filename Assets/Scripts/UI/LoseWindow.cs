using UnityEngine;
using UnityEngine.UI;

namespace BubbleShooter.UI
{
    public class LoseWindow : CanvasGroupBased
    {
        [SerializeField] private Button _replayButton;
        [SerializeField] private Button _backToMenuButton;


        public Button ReplayButton => _replayButton;
        public Button BackToMenuButton => _backToMenuButton;
    }
}