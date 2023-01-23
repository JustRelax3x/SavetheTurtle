using UnityEngine;
using UnityEngine.UI;

internal class ExtraItemPanel : ExtraPanel
{
    [SerializeField]
    private ItemView[] _itemViews;
    [SerializeField]
    private GameObject _infoPanel;
    [SerializeField]
    private Text _description;
    [SerializeField]
    private Text _skillDesc;
    public override void Open(System.Action OpenDonate, System.Action UpdateMoney)
    {
        gameObject.SetActive(true);
        for (int i = 0; i < _itemViews.Length; i++)
        {
            SetUpItemView(i);
            SetUpInfoButton(i);
            if (_itemViews[i].NameDescription != null)
                _itemViews[i].NameDescription.text = Assets.SimpleLocalization.LocalizationManager.Localize(_itemViews[i].ItemData.Name);
        }
        _notEnoughMoney = OpenDonate;
        _updateMoney = UpdateMoney;
    } 
    public override bool TryClose()
    {
        if (_infoPanel.activeInHierarchy)
        {
            CloseInfo();
            return false;
        }
        for (int i = 0; i < _itemViews.Length; i++)
        {
            _itemViews[i].EquipButton.onClick.RemoveAllListeners();
            if (_itemViews[i].Info != null)
                _itemViews[i].Info.onClick.RemoveAllListeners();
        }
        gameObject.SetActive(false);
        SaveSystem.Load();
        return true;
    }
    private void SetUpItemView(int i)
    {
        var item = _itemViews[i];
        item.EquipButton.onClick.RemoveAllListeners();
        item.EquipButton.interactable = true;
        int selectedItem = 0;
        if (Player.GetItemOpen(item.ItemData))
        {
            switch (item.ItemData.ItemType)
            {
                case ItemType.Skin:
                    selectedItem = Player.Skin;
                    break;
                case ItemType.Ability:
                    selectedItem = Player.Ability;
                    break;
                case ItemType.Map:
                    selectedItem = Player.Map;
                    break;
            }
            if (selectedItem == i)
            {
                item.EquipButton.interactable = false;
                item.EquipText.text = Assets.SimpleLocalization.LocalizationManager.Localize("select");
                return;
            }
            item.EquipText.text = Assets.SimpleLocalization.LocalizationManager.Localize("equip");
            item.EquipText.fontSize = 95;
            item.EquipText.color = new Color(0.2078431f, 0.2078431f, 0.2078431f);
            if(i!=0)
                item.EquipText.transform.GetChild(0).gameObject.SetActive(false);
            item.EquipButton.onClick.AddListener(() => SelectItem(i));
        }
        else
        {
            item.EquipButton.onClick.AddListener(() => BuyItem(i));
            item.EquipText.text = $"  {item.ItemData.Cost}";
            item.EquipText.color = new Color(0.7735849f, 0.4561232f, 0.7671778f);
            item.EquipText.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void SetUpInfoButton(int i)
    {
        if (_itemViews[i].Info != null)
            _itemViews[i].Info.onClick.AddListener(() => GetInfo(i));
    }

    private void BuyItem(int index)
    {
        if (!Player.BuyItem(_itemViews[index].ItemData)) {
            _notEnoughMoney?.Invoke();
            return; 
        }
        SetUpItemView(index);
    }

    private void SelectItem(int index)
    {
        switch (_itemViews[index].ItemData.ItemType)
        {
            case ItemType.Skin:
                Player.Skin = index;
                break;
            case ItemType.Ability:
                Player.Ability = index;
                break;
            case ItemType.Map:
                Player.Map = index;
                break;
        }
        for (int i = 0; i < _itemViews.Length; i++)
        {
            SetUpItemView(i);
        }
    }

    private void GetInfo(int index)
    {
        _description.text = Assets.SimpleLocalization.LocalizationManager.Localize(_itemViews[index].ItemData.Description);
        _skillDesc.text = Assets.SimpleLocalization.LocalizationManager.Localize(_itemViews[index].ItemData.SkillDesc);
        _infoPanel.SetActive(true);
    }

    private void CloseInfo()
    {
        _infoPanel.SetActive(false);
    }
}

