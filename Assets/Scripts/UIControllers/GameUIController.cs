using BubbleShooter.UI;
using UnityEngine;

namespace BubbleShooter
{
    public class GameUIController
    {
        private readonly Level Level;
        private readonly GameUI UI;
        private readonly MapsContainer MapsContainer;
        private readonly LevelSelectionUIController LevelSelectionController;
        private readonly PauseUIController PauseController;
        private readonly PauseSystem PauseSystem;


        public GameUIController(Level level, MapsContainer mapsContainer, GameUI ui, PauseSystem pauseSystem)
        {
            Level = level;
            UI = ui;
            MapsContainer = mapsContainer;
            PauseSystem = pauseSystem;

            LevelSelectionController = new LevelSelectionUIController(level, mapsContainer, ui);
            PauseController = new PauseUIController(level, mapsContainer, ui, pauseSystem);
        }

        public void Init()
        {
            UI.MenuWindow.PlayButton.onClick.AddListener(OnMenuWindowPlayButtonClick);
            UI.VictoryWindow.BackToMenuButton.onClick.AddListener(OnVictoryWindowBackToMenuClick);
            UI.LoseWindow.BackToMenuButton.onClick.AddListener(OnLoseWindowBackToMenuClick);
            UI.LoseWindow.ReplayButton.onClick.AddListener(OnLoseWindowReplayButtonClick);
            UI.InGameWindow.PauseButton.onClick.AddListener(OnInGameWindowPauseButtonClick);

            DisableAllWindows();
            UI.MenuWindow.Enable();

            LevelSelectionController.Init();
            PauseController.Init();
        }

        public void Deinit()
        {
            UI.MenuWindow.PlayButton.onClick.RemoveListener(OnMenuWindowPlayButtonClick);
            UI.VictoryWindow.BackToMenuButton.onClick.RemoveListener(OnVictoryWindowBackToMenuClick);
            UI.LoseWindow.BackToMenuButton.onClick.RemoveListener(OnLoseWindowBackToMenuClick);
            UI.LoseWindow.ReplayButton.onClick.RemoveListener(OnLoseWindowReplayButtonClick);
            UI.InGameWindow.PauseButton.onClick.RemoveListener(OnInGameWindowPauseButtonClick);


            PauseController.Deinit();
            LevelSelectionController.Deinit();

            Level.Completed -= OnLevelCompleted;
            Level.Failed -= OnLevelFailed;
        }


        public void OnLevelCompleted()
        {
            UI.InGameWindow.Disable();
            UI.VictoryWindow.Enable();
            Level.Deinit();
        }

        public void OnLevelFailed()
        {
            UI.InGameWindow.Disable();
            UI.LoseWindow.Enable();
            Level.Deinit();
        }

        private void OnMenuWindowPlayButtonClick()
        {
            UI.MenuWindow.Disable();
            UI.LevelSelectionWindow.Enable();
        }

        private void OnLoseWindowReplayButtonClick()
        {
            UI.LoseWindow.Disable();
            StartLevel(MapsContainer.GetLastMap());
        }

        private void OnInGameWindowPauseButtonClick()
        {
            UI.InGameWindow.Disable();
            UI.PauseWindow.Enable();
            PauseSystem.Pause();
        }

        private void OnVictoryWindowBackToMenuClick()
        {
            UI.MenuWindow.Enable();
            UI.VictoryWindow.Disable();
        }

        private void OnLoseWindowBackToMenuClick()
        {
            UI.MenuWindow.Enable();
            UI.LoseWindow.Disable();
        }

        private void StartLevel(Map map)
        {
            UI.InGameWindow.Enable();
            Level.Init(map);
        }

        private void DisableAllWindows()
        {
            UI.VictoryWindow.Disable();
            UI.MenuWindow.Disable();
            UI.PauseWindow.Disable();
            UI.LoseWindow.Disable();
            UI.LevelSelectionWindow.Disable();
        }
    }
}