using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BubbleShooter.UI
{
    public class LevelSelectionWindow : CanvasGroupBased
    {
        [SerializeField] private Button[] _prefabButtons;
        [SerializeField] private Button _randomButton;
        [SerializeField] private Button _backToMenuButton;


        public IReadOnlyList<Button> PrefabButtons => _prefabButtons;
        public Button RandomButton => _randomButton;
        public Button BackToMenuButton => _backToMenuButton;
    }
}