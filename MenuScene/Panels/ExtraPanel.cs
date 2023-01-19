using UnityEngine;

internal abstract class ExtraPanel : MonoBehaviour
{
    protected System.Action _notEnoughMoney;
    protected System.Action _updateMoney;
    public abstract void Open(System.Action OpenDonate, System.Action UpdateMoney);
    public abstract void Close();
}

