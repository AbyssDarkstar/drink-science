using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Current { get; private set; }

        private Transform _pauseMenu;

        private Button _pauseToSettingsButton;
        private Transform _settingsMenu;
        private Slider _sfxSlider;
        private Slider _musicSlider;
        private Button _closeSettingsButton;

        private Button _mainMenuButton;
        
        public float SfxVolume { get; private set; }
        public float MusicVolume { get; private set; }

        private void Awake()
        {
            Current = this;

            _pauseMenu = transform.Find("PauseMenu");

            _pauseToSettingsButton = _pauseMenu.Find("SettingsButton").GetComponent<Button>();
            _settingsMenu = transform.Find("SettingsMenu");
            _sfxSlider = _settingsMenu.Find("SFXVolumeSlider").GetComponent<Slider>();
            _musicSlider = _settingsMenu.Find("MusicVolumeSlider").GetComponent<Slider>();
            _closeSettingsButton = _settingsMenu.Find("BackButton").GetComponent<Button>();

            _mainMenuButton = _pauseMenu.Find("MainMenuButton").GetComponent<Button>();
            
            _pauseToSettingsButton.onClick.AddListener(() =>
            {
                HidePauseMenu();
                ShowSettingsMenu();
            });

            _sfxSlider.onValueChanged.AddListener(newValue =>
            {
                SfxVolume = newValue;
                PlayerPrefs.SetFloat("sfxVolume", newValue);
                SfxManager.Current.SetVolume(newValue);
            });

            _musicSlider.onValueChanged.AddListener(newValue =>
            {
                MusicVolume = newValue;
                PlayerPrefs.SetFloat("musicVolume", newValue);
                MusicManager.Current.SetVolume(newValue);
            });

            _closeSettingsButton.onClick.AddListener(() =>
            {
                HideSettingsMenu();
                ShowPauseMenu();
            });

            _mainMenuButton.onClick.AddListener(() =>
            {
                GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
            });
        }

        private void Start()
        {
            SfxVolume = PlayerPrefs.GetFloat("sfxVolume", 5f);
            _sfxSlider.SetValueWithoutNotify(SfxVolume);
            SfxManager.Current.SetVolume(SfxVolume);
            MusicVolume = PlayerPrefs.GetFloat("musicVolume", 5f);
            _musicSlider.SetValueWithoutNotify(MusicVolume);
            MusicManager.Current.SetVolume(MusicVolume);

            HideAllMenus();
        }

        public void ShowPauseMenu()
        {
            Time.timeScale = 0f;
            gameObject.SetActive(true);
            _pauseMenu.gameObject.SetActive(true);
        }

        public void HidePauseMenu()
        {
            _pauseMenu.gameObject.SetActive(false);
        }

        public void ShowSettingsMenu()
        {
            gameObject.SetActive(true);
            _settingsMenu.gameObject.SetActive(true);
        }

        public void HideSettingsMenu()
        {
            _settingsMenu.gameObject.SetActive(false);
        }

        public void HideAllMenus()
        {
            HidePauseMenu();
            HideSettingsMenu();

            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }

        public bool MenusOpen()
        {
            return gameObject.activeSelf;
        }
    }
}