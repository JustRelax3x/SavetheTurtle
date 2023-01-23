using UnityEngine;

[CreateAssetMenu(menuName = "Entities/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private string _description;
    [SerializeField]
    private string _skillDesc;
    [SerializeField]
    private ItemType _itemType;
    [SerializeField]
    private int _cost;
    [field: SerializeField]
    public int Id { get; private set; }

    public string Name => _name;
    public string Description => _description;
    public ItemType ItemType => _itemType;
    public int Cost => _cost;
    public string SkillDesc => _skillDesc;
}
