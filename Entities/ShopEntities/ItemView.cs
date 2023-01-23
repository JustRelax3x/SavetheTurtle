using UnityEngine;
using UnityEngine.UI;

internal class ItemView : MonoBehaviour
{
    [field: SerializeField]
    public ItemData ItemData { get; private set; }
    public Button EquipButton;
    public Text EquipText;
    public Text NameDescription;
    public Button Info;
}

public enum ItemType
{
    Skin,
    Ability, 
    Map
}
