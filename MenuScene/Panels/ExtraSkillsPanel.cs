using UnityEngine;
using UnityEngine.UI;

internal class ExtraSkillsPanel : MonoBehaviour
{
    [SerializeField]
    private SkillView[] _skillView;

    private System.Action _notEnoughMoney;
    private System.Action _updateMoney;

    private void Open(System.Action OpenDonate, System.Action UpdateMoney)
    {
        _notEnoughMoney = OpenDonate;
        _updateMoney = UpdateMoney;
    }

    public void SetUpSkillView(int index)
    {
        var skill = _skillView[index];
        skill.EffectDescriptionStatus.text = $"{skill.Data.GetEffectDescriptionStatus()} {skill.Data.GetValue()}";
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

}

