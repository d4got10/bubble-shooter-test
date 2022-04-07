using UnityEngine;
using UnityEngine.UI;

namespace BubbleShooter.UI
{
    public class PauseWindow : CanvasGroupBased
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _backToMenuButton;
        [SerializeField] private Button _restartButton;


        public Button ResumeButton => _resumeButton;
        public Button RestartButton => _restartButton;
        public Button BackToMenuButton => _backToMenuButton;
    }
}