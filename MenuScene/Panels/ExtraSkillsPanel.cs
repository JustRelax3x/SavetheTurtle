using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

internal class ExtraSkillsPanel : ExtraPanel
{
    [SerializeField]
    private SkillView[] _skillView;
    [SerializeField]
    private SpriteAtlas _spriteAtlas;

    public override void Open(System.Action OpenDonate, System.Action UpdateMoney)
    {
        _notEnoughMoney = OpenDonate;
        _updateMoney = UpdateMoney;
        for (int i=0; i < _skillView.Length; i++)
        {
            SetUpSkillView(i);
            _skillView[i].gameObject.SetActive(true);
        }
        gameObject.SetActive(true);
    }

    private void SetUpSkillView(int index)
    {
        var skill = _skillView[index];
        skill.Name.text = skill.Data.Name;
        skill.Description.text = skill.Data.Description;
        skill.EffectDescriptionStatus.text = $"{skill.Data.GetEffectDescriptionStatus()} {skill.Data.GetValue()}";
        skill.LevelUpScaleCoefficient.text = skill.Data.GetLevelUpCoeff() > 0 ? $"+{skill.Data.GetLevelUpCoeff()}" : skill.Data.GetLevelUpCoeff().ToString();
        skill.Icon.sprite = _spriteAtlas.GetSprite(skill.Data.GetSpriteName());
        skill.UpgradeButton.onClick.AddListener(() => UpgradeSkillButton(index));
        if (!skill.Data.IsMaxLevel())
        {
            skill.Cost.text = $"{Assets.SimpleLocalization.LocalizationManager.Localize("cost")} {skill.Data.GetCost()}";
            skill.UpgradeButton.transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("upgrade");
            return;
        }
        skill.UpgradeButton.interactable = false;
        skill.UpgradeButton.transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("max");
        skill.Cost.enabled = false;
        skill.LevelUpScaleCoefficient.enabled = false;
    }

    public void UpgradeSkillButton(int index)
    {
        var skill = _skillView[index];
        if (skill.Data.IsMaxLevel()) return;
        if (skill.Data.TryLevelUp())
        {
            SetUpSkillView(index);
            _updateMoney?.Invoke();
        }
        else _notEnoughMoney?.Invoke();
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        for (int i = 0; i < _skillView.Length; i++)
        {
            _skillView[i].UpgradeButton.onClick.RemoveAllListeners();
        }
    }
}

