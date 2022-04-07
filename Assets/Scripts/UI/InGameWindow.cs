using UnityEngine;
using UnityEngine.UI;

namespace BubbleShooter.UI
{
    public class InGameWindow : CanvasGroupBased
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private InputImage _inputImage;


        public Button PauseButton => _pauseButton;
        public InputImage InputImage => _inputImage;
    }
}