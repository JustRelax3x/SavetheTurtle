using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    public GameObject[] MainObjectsMenu;
    public Text[] MainTextMenu;
    public Text[] ShopMenu;
    public GameObject[] SettingsMenu;
    public AudioMixer Mixer;
    public Button[] Skins, AbilityButtons;
    public Button[] Skills, Maps;
    public GameObject[] ShopButtons;
    public GameObject[] Leaderboard;
    float dimskins=20, dimskills=6, dimabil = 4;   // сколько скинов
    readonly int[] costPuck = {200, 500, 1000}, costShield = {200, 500, 800, 1200, 1300, 1400,1500}, costRocket = {300, 600 , 900 , 1300}, costAbilities = {200, 500, 1000, 1300};
    public Button VolumeButton, MusicButton, LanguageButton, RandomMapButton;
    public Text SkinName, SkinDesc;
    int skin, ability, map;
    AchInc Ach;
    public GameObject PurchaseStatus, DonatePanel, DailyTrigger, Tracks;
    public GameObject[] TracksImage;
    [SerializeField]
    private SettingsPanel _settings;

    public GameObject[] X2PearlsObjects;
    private System.DateTime? dateTime;

    private void LanguageUpdate()
    {
        switch (Player.Language)
        {
            case 0:
                Assets.SimpleLocalization.LocalizationManager.Language = "English";
                break;
            case 1:
                Assets.SimpleLocalization.LocalizationManager.Language = "Russian";
                break;
        }
    }

    public void OpenDonate()
    {
        DonatePanel.SetActive(true);
        ShopButtons[0].SetActive(false);
        ShopButtons[1].SetActive(false);
        ShopButtons[2].SetActive(false);
        ShopButtons[3].SetActive(false);
        ShopButtons[4].SetActive(false);
        ShopButtons[5].SetActive(false);
        ShopButtons[6].SetActive(false);
        ShopButtons[7].SetActive(false);
        ShopButtons[8].SetActive(false);
        ShopButtons[10].SetActive(false); 

    }
  
    public void AbilityInfo(int i)
    {
        switch (i)
        {
            case 0:
                SkinName.text = Assets.SimpleLocalization.LocalizationManager.Localize("shel");
                SkinDesc.text = Assets.SimpleLocalization.LocalizationManager.Localize("sheltext");
                break;
            case 1:
                SkinName.text = Assets.SimpleLocalization.LocalizationManager.Localize("refl");
                SkinDesc.text = Assets.SimpleLocalization.LocalizationManager.Localize("refltext");
                break;
            case 2:
                SkinName.text = Assets.SimpleLocalization.LocalizationManager.Localize("time");
                SkinDesc.text = Assets.SimpleLocalization.LocalizationManager.Localize("timetext");
                break;
            case 3:
                SkinName.text = Assets.SimpleLocalization.LocalizationManager.Localize("astr");
                SkinDesc.text = Assets.SimpleLocalization.LocalizationManager.Localize("astrtext");
                break;
            default:
                break;
        }
        ShopButtons[6].SetActive(false);
        ShopButtons[5].SetActive(true);
    }
    public void DisPurchaseStatus()
    {
        PurchaseStatus.SetActive(false);
        DailyTrigger.gameObject.SetActive(false);
    } 
    private void Start()
    {
        Assets.SimpleLocalization.LocalizationManager.Read();
        LanguageUpdate();
        _settings.Initialization(LanguageUpdate);
        skin = Player.Skin;
        ability = Player.Ability;
        map = Player.Map;
        Ach = new AchInc();
        int counter = 0;
        for (int i =0; i < dimskins; i++)
        {
            //if (Player._skinOpen[i])
            //{
            //    counter++;
            //}
        }
        if (counter == dimskins)
        {
            Ach.UnlockRegular(3);
        }
        dateTime = Player.dateTime;
        StartCoroutine(DailyTriggerUpdater());
        if (Player.X2Pearls)
        {
            X2PearlsObjects[0].SetActive(false);
            X2PearlsObjects[1].SetActive(true);
        }
        else
        {
            X2PearlsObjects[0].SetActive(true);
            X2PearlsObjects[1].SetActive(false);
        }
    }

    public void UpdateDailyTrigger()
    {
        if (!dateTime.HasValue)
        {
            DailyTrigger.SetActive(true);
            return;
        }
        var timeSpan = System.DateTime.UtcNow - dateTime.Value;
        if (timeSpan.TotalHours >= 24f)
        {
            DailyTrigger.SetActive(true);
        }
        else if (timeSpan.TotalHours <= 23f)
        {
            DailyTrigger.SetActive(false);
            StopAllCoroutines();
        }
        else
        {
            DailyTrigger.SetActive(false);
        }
    }

    private IEnumerator DailyTriggerUpdater()
    {
        while (true)
        {
            UpdateDailyTrigger();
            yield return new WaitForSeconds(1);
        }
    }
}