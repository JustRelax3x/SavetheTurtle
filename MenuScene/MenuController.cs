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

    public void OnClickVolume()
    {
        if (!Player.muted)
        {
            Mixer.SetFloat("Sound", -80f);
            Player.muted = true;
            VolumeButton.transform.GetChild(0).gameObject.SetActive(false);
            VolumeButton.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            Mixer.SetFloat("Sound", 0f);
            Player.muted = false;
            VolumeButton.transform.GetChild(1).gameObject.SetActive(false);
            VolumeButton.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void OnSkinInfo(int i)
    {   
        switch (i) {
            case 0:
                SkinName.text = Assets.SimpleLocalization.LocalizationManager.Localize("defname");
                SkinDesc.text =Assets.SimpleLocalization.LocalizationManager.Localize("deftext");
                break;
            case 1:
                SkinName.text =Assets.SimpleLocalization.LocalizationManager.Localize("pirname");
                SkinDesc.text = "+" + Assets.SimpleLocalization.LocalizationManager.Localize("pirtext");
                break;
            case 2:
                SkinName.text = Assets.SimpleLocalization.LocalizationManager.Localize("golname");
                SkinDesc.text = Assets.SimpleLocalization.LocalizationManager.Localize("goltext");
                break;
            case 3:
            case 4: 
            case 5:  
            case 6:
                SkinName.text = Assets.SimpleLocalization.LocalizationManager.Localize("ninname");
                SkinDesc.text = Assets.SimpleLocalization.LocalizationManager.Localize("nintext");
                break;
            case 7:
                SkinName.text = Assets.SimpleLocalization.LocalizationManager.Localize("traname");
                SkinDesc.text = "+" + Assets.SimpleLocalization.LocalizationManager.Localize("tratext");
                break;
            case 8:
                SkinName.text = Assets.SimpleLocalization.LocalizationManager.Localize("magname");
                SkinDesc.text = Assets.SimpleLocalization.LocalizationManager.Localize("magtext");
                break;
            case 9:
                SkinName.text = Assets.SimpleLocalization.LocalizationManager.Localize("pekname");
                SkinDesc.text = Assets.SimpleLocalization.LocalizationManager.Localize("pektext");
                break;
            case 10:
                SkinName.text = Assets.SimpleLocalization.LocalizationManager.Localize("kniname");
                SkinDesc.text = Assets.SimpleLocalization.LocalizationManager.Localize("knitext");
                break;
            case 11:
                SkinName.text = Assets.SimpleLocalization.LocalizationManager.Localize("zomname");
                SkinDesc.text = Assets.SimpleLocalization.LocalizationManager.Localize("zomtext");
                break;
            case 12:
                SkinName.text = Assets.SimpleLocalization.LocalizationManager.Localize("zername");
                SkinDesc.text = Assets.SimpleLocalization.LocalizationManager.Localize("zertext");
                break;
            case 13:
                SkinName.text = Assets.SimpleLocalization.LocalizationManager.Localize("timname");
                SkinDesc.text = Assets.SimpleLocalization.LocalizationManager.Localize("timtext");
                break;
            case 14:
                SkinName.text = Assets.SimpleLocalization.LocalizationManager.Localize("ghoname");
                SkinDesc.text = Assets.SimpleLocalization.LocalizationManager.Localize("ghotext");
                break;
            case 15:
                SkinName.text = Assets.SimpleLocalization.LocalizationManager.Localize("vamname");
                SkinDesc.text = Assets.SimpleLocalization.LocalizationManager.Localize("vamtext");
                break;
            case 16:
                SkinName.text = Assets.SimpleLocalization.LocalizationManager.Localize("spaname");
                SkinDesc.text = Assets.SimpleLocalization.LocalizationManager.Localize("spatext");
                break;
            case 17:
                SkinName.text = Assets.SimpleLocalization.LocalizationManager.Localize("bloname");
                SkinDesc.text = Assets.SimpleLocalization.LocalizationManager.Localize("blotext");
                break;
            case 18:
                SkinName.text = Assets.SimpleLocalization.LocalizationManager.Localize("shuname");
                SkinDesc.text = Assets.SimpleLocalization.LocalizationManager.Localize("shutext");
                break;
            case 19:
                SkinName.text = Assets.SimpleLocalization.LocalizationManager.Localize("fokname");
                SkinDesc.text = Assets.SimpleLocalization.LocalizationManager.Localize("foktext");
                break;
            default:
                break;
        }
        ShopButtons[4].SetActive(false);
        ShopButtons[5].SetActive(true);
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
    private void changerButton(int index) //0+ skins, <0 ability, 100+ Maps
    {
        if (index >= 0 && index < 100)
        {
            Skins[index].interactable = true;
            Skins[index].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("equip");
            Skins[index].transform.GetChild(0).GetComponent<Text>().fontSize = 95;
            Skins[index].transform.GetChild(0).GetComponent<Text>().color = new Color(0.2078431f, 0.2078431f, 0.2078431f);
            if (index != 0)
                Skins[index].transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (index >= 100 && index < 150) {
            Maps[index - 100].interactable = true;
            Maps[index - 100].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("equip");
            Maps[index - 100].transform.GetChild(0).GetComponent<Text>().fontSize = 95;
            Maps[index - 100].transform.GetChild(0).GetComponent<Text>().color = new Color(0.2078431f, 0.2078431f, 0.2078431f);
            if (index - 100 != 0)
                Maps[index - 100].transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (index == 150)
        {
            RandomMapButton.transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("equip");
        }
        else
        {
            AbilityButtons[-index].interactable = true;
            AbilityButtons[-index].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("equip");
            AbilityButtons[-index].transform.GetChild(0).GetComponent<Text>().fontSize = 95;
            AbilityButtons[-index].transform.GetChild(0).GetComponent<Text>().color = new Color(0.2078431f, 0.2078431f, 0.2078431f);
            AbilityButtons[-index].transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    public void Skinchanger(int index)
    {
        if (!Player.SkinOpen[index])
        {
            switch (index)
            {
                case 0:
                    Player.SkinOpen[0] = true;
                    break;
                case 1:
                    if (Player.money >= 500)
                    {
                        Player.money -= 500;  //цена 1 скина
                        Player.SkinOpen[1] = true;
                        changerButton(1);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
                case 2:
                    if (Player.money >= 1500)
                    {
                        Player.money -= 1500;  //цена 2 скина
                        Player.SkinOpen[2] = true;
                        changerButton(2);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
                case 3:
                    if (Player.money >= 3000)
                    {
                        Player.money -= 3000;  //цена 3 скина
                        Player.SkinOpen[3] = true;
                        changerButton(3);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
                case 4:
                    if (Player.money >= 3000)
                    {
                        Player.money -= 3000;  //цена 4 скина
                        Player.SkinOpen[4] = true;
                        changerButton(4);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
                case 5:
                    if (Player.money >= 3000)
                    {
                        Player.money -= 3000;  //цена 5 скина
                        Player.SkinOpen[5] = true;
                        changerButton(5);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
                case 6:
                    if (Player.money >= 3000)
                    {
                        Player.money -= 3000;  //цена 6 скина
                        Player.SkinOpen[6] = true;
                        changerButton(6);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
                case 7:
                    if (Player.money >= 2500)
                    {
                        Player.money -= 2500;  //цена 7 скина
                        Player.SkinOpen[7] = true;
                        changerButton(7);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
                case 8:
                    if (Player.money >= 2000)
                    {
                        Player.money -= 2000;  //цена 8 скина
                        Player.SkinOpen[8] = true;
                        changerButton(8);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
                case 9:
                    if (Player.money >= 4000)
                    {
                        Player.money -= 4000;  //цена 9 скина
                        Player.SkinOpen[9] = true;
                        changerButton(9);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
                case 10:
                    if (Player.money >= 1500)
                    {
                        Player.money -= 1500;  //цена 10 скина
                        Player.SkinOpen[10] = true;
                        changerButton(10);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
                case 11:
                    if (Player.money >= 4000)
                    {
                        Player.money -= 4000;  //цена 11 скина
                        Player.SkinOpen[11] = true;
                        changerButton(11);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
                case 12:
                    if (Player.money >= 500)
                    {
                        Player.money -= 500;  //цена 12 скина
                        Player.SkinOpen[12] = true;
                        changerButton(12);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
                case 13:
                    if (Player.money >= 2000)
                    {
                        Player.money -= 2000;  //цена 13 скина
                        Player.SkinOpen[13] = true;
                        changerButton(13);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
                case 14:
                    if (Player.money >= 1000)
                    {
                        Player.money -= 1000;  //цена 14 скина
                        Player.SkinOpen[14] = true;
                        changerButton(14);
                    }
                    else
                    {
                        OpenDonate();
                    } 
                    break;
                case 15:
                    if (Player.money >= 1000)
                    {
                        Player.money -= 1000;  //цена 15 скина
                        Player.SkinOpen[15] = true;
                        changerButton(15);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
                case 16:
                    if (Player.money >= 1500)
                    {
                        Player.money -= 1500;  //цена 16 скина
                        Player.SkinOpen[16] = true;
                        changerButton(16);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
                case 17:
                    if (Player.money >= 1500)
                    {
                        Player.money -= 1500;  //цена 17 скина
                        Player.SkinOpen[17] = true;
                        changerButton(17);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
                case 18:
                    if (Player.money >= 2000)
                    {
                        Player.money -= 2000;  //цена 18 скина
                        Player.SkinOpen[18] = true;
                        changerButton(18);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
                case 19:
                    if (Player.money >= 3000)
                    {
                        Player.money -= 3000;  //цена 19 скина
                        Player.SkinOpen[19] = true;
                        changerButton(19);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
                default:
                    break;
            }
            ShopMenu[0].text = Player.money.ToString();
            SaveSystem.Load();
        }
        else
        {
            for (int i = 0; i < dimskins; i++)
            {
                if (Player.SkinOpen[i])
                {
                    if (i == index)
                    {
                        Skins[i].interactable = false;
                        Skins[i].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("select");
                        Player.Skin = i;
                        skin = i;
                    }
                    else
                    {
                        Skins[i].interactable = true;
                        Skins[i].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("equip");
                    }
                }
            }
            SaveSystem.Load();
        }
    }

    public void ChangerSkillButton(int index)
    {
        switch (index)
        {
            case 0:
                {
                    ShopMenu[2].text = (0.5f + 0.1f * Player.PuckSkillLevel).ToString();
                    if (Player.PuckSkillLevel < 3)
                    {
                        ShopMenu[4].text = costPuck[Player.PuckSkillLevel].ToString();
                        Skills[0].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("upgrade");
                    }
                    else
                    {
                        Skills[0].interactable = false;
                        Skills[0].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("max");
                        ShopMenu[3].gameObject.SetActive(false);
                        ShopMenu[4].gameObject.SetActive(false);
                        ShopMenu[5].gameObject.SetActive(false);
                    }
                }
                break;
            case 1:
                ShopMenu[6].text = (5 + 5 * Player.ShieldLevel).ToString();
                if (Player.ShieldLevel < 7)
                {
                    ShopMenu[8].text = costShield[Player.ShieldLevel].ToString();
                    Skills[1].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("upgrade");
                }
                else
                {
                    Skills[1].interactable = false;
                    Skills[1].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("max");
                    ShopMenu[7].gameObject.SetActive(false);
                    ShopMenu[8].gameObject.SetActive(false);
                    ShopMenu[9].gameObject.SetActive(false);
                }
                break;
            case 2:
                ShopMenu[10].text = (5 + 1 * Player.RocketLevel).ToString();
                if (Player.RocketLevel < 4)
                {
                    ShopMenu[12].text = costRocket[Player.RocketLevel].ToString();
                    Skills[2].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("upgrade");
                }
                else
                {
                    Skills[2].interactable = false;
                    Skills[2].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("max");
                    ShopMenu[11].gameObject.SetActive(false);
                    ShopMenu[12].gameObject.SetActive(false);
                    ShopMenu[13].gameObject.SetActive(false);
                }
                break;
            case 3:
                ShopMenu[14].text = (6 - 0.25 * Player.AbilityLevel[0]).ToString();
                if (Player.AbilityLevel[0] < 4)
                {
                    ShopMenu[15].text = costAbilities[Player.AbilityLevel[0]].ToString();
                    Skills[3].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("upgrade");
                }
                else
                {
                    Skills[3].interactable = false;
                    Skills[3].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("max");
                    ShopMenu[20].gameObject.SetActive(false);
                    ShopMenu[15].gameObject.SetActive(false);
                }
                break;
            case 4:
                ShopMenu[16].text = (4 + 0.25 * Player.AbilityLevel[1]).ToString();
                if (Player.AbilityLevel[1] < 4)
                {
                    ShopMenu[17].text = costAbilities[Player.AbilityLevel[1]].ToString();
                    Skills[4].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("upgrade");
                }
                else
                {
                    Skills[4].interactable = false;
                    Skills[4].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("max");
                    ShopMenu[21].gameObject.SetActive(false);
                    ShopMenu[17].gameObject.SetActive(false);
                }
                break;
            case 5:
                ShopMenu[18].text = (200 - 25 * Player.AbilityLevel[2]).ToString();
                if (Player.AbilityLevel[2] < 4)
                {
                    ShopMenu[19].text = costAbilities[Player.AbilityLevel[2]].ToString();
                    Skills[5].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("upgrade");
                }
                else
                {
                    Skills[5].interactable = false;
                    Skills[5].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("max");
                    ShopMenu[22].gameObject.SetActive(false);
                    ShopMenu[19].gameObject.SetActive(false);
                }
                break;
            default:
                break;
        }
        SaveSystem.Load();
    }

    public void UpgradeSkillButton(int index) //ShopMenu[2] - skill time [3]->+0.1 [4]->cost value [5] cost text...
    {
        switch (index)
        {
            case 0:
                {   if (Player.PuckSkillLevel < 3)
                    {
                        if (Player.money >= costPuck[Player.PuckSkillLevel])
                        {
                            Player.money -= costPuck[Player.PuckSkillLevel];
                            Player.PuckSkillLevel++;
                            ChangerSkillButton(0);
                        }
                        else
                        {
                            OpenDonate();
                        }
                    }
                }
                break;
            case 1:
                {
                    if (Player.ShieldLevel < 7)
                    {
                        if (Player.money >= costShield[Player.ShieldLevel])
                        {
                            Player.money -= costShield[Player.ShieldLevel];
                            Player.ShieldLevel++;
                            ChangerSkillButton(1);
                        }
                        else
                        {
                            OpenDonate();
                        }
                    }
                }
                break;
            case 2:
                {
                    if (Player.RocketLevel < 4)
                    {
                        if (Player.money >= costRocket[Player.RocketLevel])
                        {
                            Player.money -= costRocket[Player.RocketLevel];
                            Player.RocketLevel++;
                            ChangerSkillButton(2);
                        }
                        else
                        {
                            OpenDonate();
                        }
                    }
                }
                break;
            case 3:
                {
                    if (Player.AbilityLevel[0] < 4)
                    {
                        if (Player.money >= costAbilities[Player.AbilityLevel[0]])
                        {
                            Player.money -= costAbilities[Player.AbilityLevel[0]];
                            Player.AbilityLevel[0]++;
                            ChangerSkillButton(3);
                        }
                        else
                        {
                            OpenDonate();
                        }
                    }
                }
                break;
            case 4:
                {
                    if (Player.AbilityLevel[1] < 4)
                    {
                        if (Player.money >= costAbilities[Player.AbilityLevel[1]])
                        {
                            Player.money -= costAbilities[Player.AbilityLevel[1]];
                            Player.AbilityLevel[1]++;
                            ChangerSkillButton(4);
                        }
                        else
                        {
                            OpenDonate();
                        }
                    }
                }
                break;
            case 5:
                {
                    if (Player.AbilityLevel[2] < 4)
                    {
                        if (Player.money >= costAbilities[Player.AbilityLevel[2]])
                        {
                            Player.money -= costAbilities[Player.AbilityLevel[2]];
                            Player.AbilityLevel[2]++;
                            ChangerSkillButton(5);
                        }
                        else
                        {
                            OpenDonate();
                        }
                    }
                }
                break;
            default:
                break;
        }
        ShopMenu[0].text = Player.money.ToString();
    }
    public void PickMaps(int index)
    {
        if (index != 50)
        {
            if (!Player.MapOpen[index])
            {
                switch (index)
                {
                    case 0:
                        Player.SkinOpen[0] = true;
                        changerButton(100);
                        break;
                    case 1:
                        if (Player.money >= 1000)
                        {
                            Player.money -= 1000;  //цена 1 скина
                            Player.MapOpen[1] = true;
                            changerButton(101);
                        }
                        else
                        {
                            OpenDonate();
                        }
                        break;
                    case 2:
                        if (Player.money >= 1000)
                        {
                            Player.money -= 1000;  //цена 2 скина
                            Player.MapOpen[2] = true;
                            changerButton(102);
                        }
                        else
                        {
                            OpenDonate();
                        }
                        break;
                    case 3:
                        if (Player.money >= 1000)
                        {
                            Player.money -= 1000;  //цена 3 скина
                            Player.MapOpen[3] = true;
                            changerButton(103);
                        }
                        else
                        {
                            OpenDonate();
                        }
                        break;
                    case 4:
                        if (Player.money >= 1000)
                        {
                            Player.money -= 1000;  //цена 4 скина
                            Player.MapOpen[4] = true;
                            changerButton(104);
                        }
                        else
                        {
                            OpenDonate();
                        }
                        break;
                    case 5:
                        if (Player.money >= 1000)
                        {
                            Player.money -= 1000;  //цена 5 скина
                            Player.MapOpen[5] = true;
                            changerButton(105);
                        }
                        else
                        {
                            OpenDonate();
                        }
                        break;
                    default:
                        break;
                }
                ShopMenu[0].text = Player.money.ToString();
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    if (Player.MapOpen[i])
                    {  
                        Maps[i].interactable = true;
                        Maps[i].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("equip");
                        if (i == index)
                        {
                            Maps[i].interactable = false;
                            Maps[i].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("select");
                            Player.Map = i;
                            map = i;
                        }  
                    }
                }
                RandomMapButton.interactable = true;
                RandomMapButton.transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("equip");
            }
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {
                if (Player.MapOpen[i])
                {
                    Maps[i].interactable = true;
                    Maps[i].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("equip");
                }
            }
            RandomMapButton.interactable = false;
            RandomMapButton.transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("select");
            Player.Map = 50;
            map = 50;
        }
            SaveSystem.Load();
    }
    private void Translate()
    {
            for (int i = 0; i < dimskins; i++)
            {
                if (Player.SkinOpen[i])
                {
                    changerButton(i);
                }
                if (i == skin)
                {
                    Skins[i].interactable = false;
                    Skins[i].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("select");
                }
            }
            for (int i = 0; i < dimskills; i++)
            {
                ChangerSkillButton(i);
            }
            for (int i = 0; i < dimabil; i++)
            {
                if (Player.AbilityOpen[i])
                {
                    if (i != 0)
                        changerButton(-i);
                    else
                    {
                        AbilityButtons[i].interactable = true;
                        AbilityButtons[i].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("equip");
                        AbilityButtons[i].transform.GetChild(0).GetComponent<Text>().fontSize = 95;
                        AbilityButtons[i].transform.GetChild(0).GetComponent<Text>().color = new Color(0.2078431f, 0.2078431f, 0.2078431f);
                    }
                }
                if (i == ability)
                {
                    AbilityButtons[i].interactable = false;
                    AbilityButtons[i].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("select");
                }
            }
            for (int i = 0; i < 6; i++)
            {
                if (Player.MapOpen[i])
                {
                    changerButton(100 + i);
                }
                if (i == map)
                {
                    Maps[i].interactable = false;
                    Maps[i].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("select");
                }
            }
            changerButton(150);
            if (map == 50)
            {
                RandomMapButton.interactable = false;
                RandomMapButton.transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("select");
            }
    }
    public void OpenAbilityShop(int x)
    {
        ShopButtons[0].SetActive(false);
        ShopButtons[2].SetActive(false);
        ShopButtons[4].SetActive(false);
        ShopButtons[7].SetActive(false);
        ShopButtons[8].SetActive(false);
        if (x == 1)
        {
            ShopButtons[6].SetActive(true);
            ShopButtons[1].SetActive(true);
        }
        else if (x == 2)
        {
            ShopButtons[1].SetActive(true);
            ShopButtons[9].SetActive(true);
        }
       
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

    public void AbilityPick(int x)
    {
        if (Player.AbilityOpen[x])
        {
            Player.Ability = x;
            ability = x;
            for (int i = 0; i < dimabil; i++) 
            {
                if (Player.AbilityOpen[i])
                {
                    if (i == x)
                    {
                        AbilityButtons[i].interactable = false;
                        AbilityButtons[i].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("select");
                    }
                    else
                    {
                        if (i != 0)
                        {
                            changerButton(-i);
                        }
                        else
                        {
                            AbilityButtons[i].interactable = true;
                            AbilityButtons[i].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("equip");
                            AbilityButtons[i].transform.GetChild(0).GetComponent<Text>().fontSize = 95;
                            AbilityButtons[i].transform.GetChild(0).GetComponent<Text>().color = new Color(0.2078431f, 0.2078431f, 0.2078431f);
                        }
                    }
                }
            }
            SaveSystem.Load();
        }
        else
        {
            switch (x)
            {
                case 0:
                    Player.AbilityOpen[0] = true;
                    AbilityButtons[0].interactable = true;
                    AbilityButtons[0].transform.GetChild(0).GetComponent<Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("equip");
                    break;
                case 1:
                    if (Player.money >= 1000)
                    {
                        Player.money -= 1000;
                        Player.AbilityOpen[1] = true;
                        changerButton(-1);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
                case 2:
                    if (Player.money >= 1000)
                    {
                        Player.money -= 1000;
                        Player.AbilityOpen[2] = true;
                        changerButton(-2);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
                case 3:
                    if (Player.money >= 1000)
                    {
                        Player.money -= 1000;
                        Player.AbilityOpen[3] = true;
                        changerButton(-3);
                    }
                    else
                    {
                        OpenDonate();
                    }
                    break;
            }
            ShopMenu[0].text = Player.money.ToString();
        }
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
        Player.SkinOpen[skin] = true;
        ability = Player.Ability;
        map = Player.Map;
        Ach = new AchInc();
        int counter = 0;
        for (int i =0; i < dimskins; i++)
        {
            if (Player.SkinOpen[i])
            {
                counter++;
            }
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