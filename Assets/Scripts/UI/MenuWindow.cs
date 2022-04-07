using UnityEngine;
using UnityEngine.UI;

namespace BubbleShooter.UI
{
    public class MenuWindow : CanvasGroupBased
    {
        [SerializeField] private Button _playButton;


        public Button PlayButton => _playButton;
    }
}