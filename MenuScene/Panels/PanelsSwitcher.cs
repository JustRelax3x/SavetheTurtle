using UnityEngine;

public class PanelsSwitcher : MonoBehaviour, IPanelSwitcher
{
    [SerializeField]
    private ViewPanel _mainMenu;
    [SerializeField]
    private ViewPanel _shop;
    [SerializeField]
    private ViewPanel _settings;
    [SerializeField]
    private ViewPanel _stats;
    [SerializeField]
    private ViewPanel _dailyRewards;

    private const MenuPanelType DefaultPanel = MenuPanelType.MainMenu;


    private void Start()
    {
        _shop.gameObject.SetActive(false);
        _settings.gameObject.SetActive(false);
        _stats.gameObject.SetActive(false);
        Switch(DefaultPanel);
    }

    public void Switch(MenuPanelType menuPanelType)
    {
        GetPanel(menuPanelType).Open(this);
    }

    private ViewPanel GetPanel(MenuPanelType menuPanelType)
    {
        switch (menuPanelType)
        {
            case MenuPanelType.MainMenu:
                return _mainMenu;
            case MenuPanelType.Shop:
                return _shop;
            case MenuPanelType.Settings:
                return _settings;
            case MenuPanelType.Statistics:
                return _stats;
            case MenuPanelType.DailyRewards:
                return _dailyRewards;
            case MenuPanelType.Length:
                break;
        }
        return null;
    }
}