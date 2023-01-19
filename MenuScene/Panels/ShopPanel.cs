using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : ViewPanel
{
    [Header("Buttons")]
    [SerializeField]
    private Button _skins;
    [SerializeField]
    private Button _skills;
    [SerializeField]
    private Button _abilities;
    [SerializeField]
    private Button _maps;
    [SerializeField]
    private Button _donate;
    [SerializeField]
    private Button _back;
    [Space(10)]
    [Header("Panels")]
    [SerializeField]
    private GameObject _skinsPanel;
    [SerializeField]
    private ExtraPanel _skillsPanel;
    [SerializeField]
    private GameObject _abilitiesPanel;
    [SerializeField]
    private GameObject _mapsPanel;
    [SerializeField]
    private GameObject _donatePanel;
    [Space(10)]
    [Header("Texts")]
    [SerializeField]
    private Text _money;

    private bool _isExtraPanelOpened = false;

    private ExtraPanel _currentExtraPanel;
    public override MenuPanelType PanelType()
    {
        return MenuPanelType.Shop;
    }

    private void Start()
    {
        _skins.onClick.AddListener(() => OpenExtraPanel(_skillsPanel));
        _skills.onClick.AddListener(() => OpenExtraPanel(_skillsPanel));
        _abilities.onClick.AddListener(() => OpenExtraPanel(_skillsPanel));
        _maps.onClick.AddListener(() => OpenExtraPanel(_skillsPanel));
        _back.onClick.AddListener(BackButton);
        _currentExtraPanel = _skillsPanel; //_skinsPanel;
    }

    public override void Open(IPanelSwitcher panelSwitcher)
    {
        UpdateMoney();
        _isExtraPanelOpened = false;
        SetButtonsActive(true);
        base.Open(panelSwitcher);
    }
    private void OpenExtraPanel(ExtraPanel panel)
    {
        _currentExtraPanel = panel;
        _currentExtraPanel.Open(OpenDonatePanel, UpdateMoney);
        _isExtraPanelOpened = true;
        SetButtonsActive(false);
    }

    private void SetButtonsActive(bool flag)
    {
        _skins.gameObject.SetActive(flag);
        _skills.gameObject.SetActive(flag);
        _abilities.gameObject.SetActive(flag);
        _maps.gameObject.SetActive(flag);
    }

    private void OpenDonatePanel()
    {

    }

    private void UpdateMoney()
    {
        _money.text = Player.money.ToString();
    }

    private void BackButton()
    {
        if (!_isExtraPanelOpened) Close(MenuPanelType.MainMenu);
        _currentExtraPanel.Close();
        _isExtraPanelOpened = false;
        SetButtonsActive(true);
    }

    private void OnDestroy()
    {
        _skins.onClick.RemoveAllListeners();
        _skills.onClick.RemoveAllListeners();
        _abilities.onClick.RemoveAllListeners();
        _maps.onClick.RemoveAllListeners();
        _back.onClick.RemoveAllListeners(); 
    }
}
