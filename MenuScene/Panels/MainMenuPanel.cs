using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanel : ViewPanel
{
    [SerializeField]
    private Button _play;
    [SerializeField]
    private Button _shop;
    [SerializeField]
    private Button _settings;
    [SerializeField]
    private Button _statistics;
    [SerializeField]
    private Text _highscore;

    public override MenuPanelType PanelType()
    {
        return MenuPanelType.MainMenu;
    }

    public override void Open(IPanelSwitcher panelSwitcher)
    {
        _highscore.text = Player.highscore.ToString();
        base.Open(panelSwitcher);
    }

    private void Start()
    {
        _play.onClick.AddListener(PlayButton);
        _shop.onClick.AddListener(() => Close(MenuPanelType.Shop));
        _settings.onClick.AddListener(() => Close(MenuPanelType.Settings));
        _statistics.onClick.AddListener(() => Close(MenuPanelType.Statistics));
    }

    private void PlayButton()
    {
        SaveSystem.Load();
        if (!PlayerPrefs.HasKey(GameConstants.TutorialScene))
        {
            PlayerPrefs.SetInt(GameConstants.TutorialScene, 1);
            SceneManager.LoadScene(GameConstants.TutorialScene);
        }
        else
        {
            SceneManager.LoadScene(GameConstants.GameScene);
        }
    }

    private void OnDestroy()
    {
        _play.onClick.RemoveAllListeners();
        _shop.onClick.RemoveAllListeners();
        _settings.onClick.RemoveAllListeners();
        _statistics.onClick.RemoveAllListeners();
    }
}
