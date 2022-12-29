using UnityEngine;

public abstract class ViewPanel : MonoBehaviour
{
    [SerializeField]
    protected GameObject _root;

    protected IPanelSwitcher _panelSwitcher;

    public abstract MenuPanelType PanelType();

    public virtual void Open(IPanelSwitcher panelSwitcher)
    {
        _root.SetActive(true);
        _panelSwitcher = panelSwitcher;
    }

    public virtual void Close(MenuPanelType menuPanelType)
    {
        _panelSwitcher.Switch(menuPanelType);
        _root.SetActive(false);
    }
}
[SerializeField]
public enum MenuPanelType
{
    MainMenu = 0,
    Shop = 1,
    Settings,
    Statistics,
    DailyRewards,
    Length
}
