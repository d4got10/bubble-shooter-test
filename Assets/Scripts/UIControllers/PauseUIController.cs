using BubbleShooter.UI;

namespace BubbleShooter
{
    public class PauseUIController
    {
        private readonly Level Level;
        private readonly GameUI UI;
        private readonly MapsContainer MapsContainer;
        private readonly PauseSystem PauseSystem;

        private PauseWindow Window => UI.PauseWindow;

        public PauseUIController(Level level, MapsContainer mapsContainer, GameUI ui, PauseSystem pauseSystem)
        {
            Level = level;
            UI = ui;
            MapsContainer = mapsContainer;
            PauseSystem = pauseSystem;
        }

        public void Init()
        {
            Window.RestartButton.onClick.AddListener(OnRestartButtonClick);
            Window.ResumeButton.onClick.AddListener(OnResumeButtonClick);
            Window.BackToMenuButton.onClick.AddListener(OnBackToMenuButtonClick);
        }

        public void Deinit()
        {
            Window.RestartButton.onClick.RemoveListener(OnRestartButtonClick);
            Window.ResumeButton.onClick.RemoveListener(OnResumeButtonClick);
            Window.BackToMenuButton.onClick.RemoveListener(OnBackToMenuButtonClick);
        }


        private void OnRestartButtonClick()
        {
            Level.Deinit();
            LoadLevel(MapsContainer.GetLastMap());
            PauseSystem.Unpause();
        }

        private void OnResumeButtonClick()
        {
            Window.Disable();
            UI.InGameWindow.Enable();
            PauseSystem.Unpause();
        }

        private void OnBackToMenuButtonClick()
        {
            Level.Deinit();
            Window.Disable();
            UI.MenuWindow.Enable();
            PauseSystem.Unpause();
        }

        private void LoadLevel(Map map)
        {
            Window.Disable();
            UI.InGameWindow.Enable();
            Level.Init(map);
        }
    }
}