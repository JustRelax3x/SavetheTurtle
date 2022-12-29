using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class IAPShop : MonoBehaviour
{
    private readonly string _8k = "com.tornadoelitepro.savetheturtle.8kpearls";
    private readonly string _20k = "com.tornadoelitepro.savetheturtle.20kpearls";
    private readonly string _40k = "com.tornadoelitepro.savetheturtle.40kpearls";

    public Text _ShopMoney;
    public GameObject PurshaceStatusPanel;
    public Text Status;
    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == _8k)
        {
            Player.money += 8000;
        }
        else if (product.definition.id == _20k)
        {
            Player.money += 20000;
        }
        else if (product.definition.id == _40k)
        {
            Player.money += 40000;
        }
        _ShopMoney.text = Player.money.ToString();
        PurshaceStatusPanel.SetActive(true);
        Status.text = Assets.SimpleLocalization.LocalizationManager.Localize("suc");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason )
    {
        PurshaceStatusPanel.SetActive(true);
        Status.text = Assets.SimpleLocalization.LocalizationManager.Localize("fail");
    }
}
