using BubbleShooter.UI;
using UnityEngine;

namespace BubbleShooter
{
    public class Game : MonoBehaviour
    {
        public const int SizeX = 11;
        public const int SizeY = 2 * SizeX + 1;


        [SerializeField] private PauseSystem _pauseSystem;
        [SerializeField] private Level _level;
        [SerializeField] private GameUI _ui;


        private GameUIController _uiController;
        private MapsContainer _mapsContainer;
        private InputSystem _inputSystem;
        

        private void Start()
        {
            _inputSystem = new InputSystem(_ui.InGameWindow.InputImage);
            _inputSystem.Init();
            InputSystemProvider.Init(_inputSystem);

            _mapsContainer = new MapsContainer();
            _uiController = new GameUIController(_level, _mapsContainer, _ui, _pauseSystem);
            _uiController.Init();

            _level.Completed += _uiController.OnLevelCompleted;
            _level.Failed += _uiController.OnLevelFailed;
        }


        private void OnDestroy()
        {
            _level.Completed -= _uiController.OnLevelCompleted;
            _level.Failed -= _uiController.OnLevelFailed;

            if (_level.IsInitialized)
                _level.Deinit();

            _uiController.Deinit();

            _inputSystem.Deinit();
        }
    }
}