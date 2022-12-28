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
    private GameObject _skillsPanel;
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

    private GameObject _currentExtraPanel;
    public override MenuPanelType PanelType()
    {
        return MenuPanelType.Shop;
    }

    private void Start()
    {
        _skins.onClick.AddListener(() => OpenExtraPanel(_skinsPanel));
        _skills.onClick.AddListener(() => OpenExtraPanel(_skillsPanel));
        _abilities.onClick.AddListener(() => OpenExtraPanel(_abilitiesPanel));
        _maps.onClick.AddListener(() => OpenExtraPanel(_mapsPanel));
        _back.onClick.AddListener(BackButton);
        _currentExtraPanel = _skinsPanel;
    }

    public override void Open(IPanelSwitcher panelSwitcher)
    {
        _money.text = Player.money.ToString();
        _isExtraPanelOpened = false;
        base.Open(panelSwitcher);
    }

    private void OpenExtraPanel(GameObject panel)
    {
        _currentExtraPanel = panel;
        _currentExtraPanel.SetActive(true);
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

    private void BackButton()
    {
        if (!_isExtraPanelOpened) Close(MenuPanelType.MainMenu);
        _currentExtraPanel.SetActive(false);
        _isExtraPanelOpened = false;
        SetButtonsActive(true);
    }

    private void OnDestroy()
    {
        _skins.onClick.RemoveAllListeners();
        _skills.onClick.RemoveAllListeners();
        _abilities.onClick.RemoveAllListeners();
        _maps.onClick.RemoveAllListeners();
    }
}
