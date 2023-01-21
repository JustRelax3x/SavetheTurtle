using UnityEngine;

internal abstract class Garbage : MonoBehaviour
{
    [SerializeField]
    private string _name;
    public string GetName() => _name;

    [SerializeField]
    private int _id;

    public int GetID() => _id;

    public abstract int GetDamage();
}
