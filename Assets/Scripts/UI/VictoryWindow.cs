using UnityEngine;
using UnityEngine.UI;

namespace BubbleShooter.UI
{
    public class VictoryWindow : CanvasGroupBased
    {
        [SerializeField] private Button _backToMenuButton;


        public Button BackToMenuButton => _backToMenuButton;
    }
}