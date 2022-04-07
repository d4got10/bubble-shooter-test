using UnityEngine;


namespace BubbleShooter.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private InGameWindow _inGameWindow;
        [SerializeField] private VictoryWindow _victoryWindow;
        [SerializeField] private LoseWindow _loseWindow;
        [SerializeField] private PauseWindow _pauseWindow;
        [SerializeField] private MenuWindow _menuWindow;
        [SerializeField] private LevelSelectionWindow _levelSelectionWindow;


        public InGameWindow InGameWindow => _inGameWindow;
        public VictoryWindow VictoryWindow => _victoryWindow;
        public LoseWindow LoseWindow => _loseWindow;
        public PauseWindow PauseWindow => _pauseWindow;
        public MenuWindow MenuWindow => _menuWindow;
        public LevelSelectionWindow LevelSelectionWindow => _levelSelectionWindow;
    }
}