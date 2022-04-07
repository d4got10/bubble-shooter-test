using BubbleShooter.UI;

namespace BubbleShooter
{

    public class LevelSelectionUIController
    {
        private readonly Level Level;
        private readonly GameUI UI;
        private readonly MapsContainer MapsContainer;


        private LevelSelectionWindow Window => UI.LevelSelectionWindow;


        public LevelSelectionUIController(Level level, MapsContainer mapsContainer, GameUI ui)
        {
            Level = level;
            UI = ui;
            MapsContainer = mapsContainer;
        }

        
        public void Init()
        {
            Window.BackToMenuButton.onClick.AddListener(OnBackToMenuClick);
            Window.PrefabButtons[0].onClick.AddListener(OnFirstLevelButtonClick);
            Window.PrefabButtons[1].onClick.AddListener(OnSecondLevelButtonClick);
            Window.PrefabButtons[2].onClick.AddListener(OnThirdLevelButtonClick);
            Window.RandomButton.onClick.AddListener(OnRandomLevelButtonClick);
        }

        public void Deinit()
        {
            Window.BackToMenuButton.onClick.RemoveListener(OnBackToMenuClick);
            Window.PrefabButtons[0].onClick.RemoveListener(OnFirstLevelButtonClick);
            Window.PrefabButtons[1].onClick.RemoveListener(OnSecondLevelButtonClick);
            Window.PrefabButtons[2].onClick.RemoveListener(OnThirdLevelButtonClick);
            Window.RandomButton.onClick.RemoveListener(OnRandomLevelButtonClick);
        }


        private void OnFirstLevelButtonClick()
        {
            LoadLevel(MapsContainer.GetPrefabMap(0));
        }

        private void OnSecondLevelButtonClick()
        {
            LoadLevel(MapsContainer.GetPrefabMap(1));
        }

        private void OnThirdLevelButtonClick()
        {
            LoadLevel(MapsContainer.GetPrefabMap(2));
        }

        private void OnRandomLevelButtonClick()
        {
            LoadLevel(MapsContainer.GetRandomMap());
        }

        private void OnBackToMenuClick()
        {
            Window.Disable();
            UI.MenuWindow.Enable();
        }

        private void LoadLevel(Map map)
        {
            Window.Disable();
            UI.InGameWindow.Enable();
            Level.Init(map);
        }
    } 
}