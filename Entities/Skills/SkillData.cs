using UnityEngine;
[CreateAssetMenu(menuName = "Entities/SkillData")]
internal class SkillData : ScriptableObject
{
    [SerializeField]
    private SkillType _type;
    [SerializeField]
    private int[] _costsByLevel;
    [SerializeField]
    private int _levelUpScaleCoefficient;
    [SerializeField]
    private float _startingValue;
    [SerializeField]
    private string _name;
    [SerializeField]
    private string _effectDescriptionStatus;

    private int _level = 0; 
    
    public float GetValue() => _startingValue + _levelUpScaleCoefficient*_level;

    public string GetEffectDescriptionStatus() => Assets.SimpleLocalization.LocalizationManager.Localize(_effectDescriptionStatus);

    public string GetName() => Assets.SimpleLocalization.LocalizationManager.Localize(_name);

    public int GetCost() => _costsByLevel[_level];

    public bool IsMaxLevel() => _level == _costsByLevel.Length;
    public SkillType GetSType() => _type;
    public int GetID() => (int)_type;

    public bool TryLevelUp()
    {
        if (IsMaxLevel() || Player.money < _costsByLevel[_level]) return false;
        Player.money -= _costsByLevel[_level];
        _level++;
        return true;
    }
}

public enum SkillType
{
    Shield = 0,
    RocketBubble,
}

