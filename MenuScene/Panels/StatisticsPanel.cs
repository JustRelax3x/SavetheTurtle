using UnityEngine;
using UnityEngine.UI;

public class StatisticsPanel : ViewPanel
{
    [SerializeField]
    private Button _back;
    [SerializeField]
    public Text[] objects;

    public override void Open(IPanelSwitcher panelSwitcher)
    {
        UpdateObjectsStats();
        base.Open(panelSwitcher);
    }

    private void Start()
    {
        _back.onClick.AddListener(() => Close(MenuPanelType.MainMenu));
    }

    private void UpdateObjectsStats()
    {
        for (int i = 0; i < objects.Length - 1; i++)
        {
            objects[i].text = ": " + Player.Objects[i].ToString();
        }
        objects[GameConstants.GamesPlayedID].text = Player.Objects[GameConstants.GamesPlayedID].ToString();
    }

    public override MenuPanelType PanelType()
    {
        return MenuPanelType.Statistics;
    }

    private void OnDestroy()
    {
        _back.onClick.RemoveAllListeners();
    }
}