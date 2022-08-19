using System;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
public class SaveSystem : MonoBehaviour
{
    public SaveData save = new SaveData();
    public SaveShopData shop = new SaveShopData();

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Save"))
        {
            try
            {
                save = JsonUtility.FromJson<SaveData>(Helper.Decrypt(PlayerPrefs.GetString("Save")));
                save.LoadData();
            }
            catch (Exception)
            {
                save.StartGame();
            }
        }
        else
        {
            save.StartGame();
        }
        if (PlayerPrefs.HasKey("Shop"))
        {
            try
            {
                shop = JsonUtility.FromJson<SaveShopData>(Helper.Decrypt(PlayerPrefs.GetString("Shop")));
                shop.LoadShopData();
            }
            catch (Exception)
            {
                shop.StartShop();
            }
        }
        else
        {
            shop.StartShop();
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public static void Load()
    {
        SaveData save = new SaveData();
        SaveShopData shop = new SaveShopData();
        save.TakeData();
        PlayerPrefs.SetString("Save", Helper.Encrypt(JsonUtility.ToJson(save)));
        shop.TakeShopData();
        PlayerPrefs.SetString("Shop", Helper.Encrypt(JsonUtility.ToJson(shop)));
    }
    public void OnApplicationQuit()
    {
        save.TakeData();
        PlayerPrefs.SetString("Save", Helper.Encrypt(JsonUtility.ToJson(save)));
        shop.TakeShopData();
        PlayerPrefs.SetString("Shop", Helper.Encrypt(JsonUtility.ToJson(shop)));
        PlayGamesPlatform.Instance.SignOut();
    }
    public void OnApplicationPause()
    {
        save.TakeData();
        PlayerPrefs.SetString("Save", Helper.Encrypt(JsonUtility.ToJson(save)));
        shop.TakeShopData();
        PlayerPrefs.SetString("Shop", Helper.Encrypt(JsonUtility.ToJson(shop)));
    }
}
[Serializable]
public class SaveData
{
    public int hihgscore, money, skin;
    public void TakeData()
    {
        hihgscore = Player.highscore;
        money = Player.money;
        skin = Player.Skin;
    }
    public void LoadData()
    {
        Player.highscore = hihgscore;
        Player.money = money;
        Player.Skin = skin;
    }
    public void StartGame()
    {
        Player.highscore = 0;
        Player.money = 0;
        Player.Skin = 0;
    }
}
[Serializable]
public class SaveShopData
{
    public int n = 20, reward; //сколько скинов
    public bool[] Skins = new bool [20], abilityopen = new bool[4], mapopen = new bool[6];
    public int puck, shield, rocket, ability, language, map;
    public int[] abilitylevel = new int[3];
    public bool muted, mutedmus;
    public long[] obj = new long [19];
    public string datetime;

    
    public void TakeShopData()
    {
        for (int i=0; i< n; i++)
        {
            Skins[i] = Player.SkinOpen[i];
        }
        puck = Player.PuckSkillLevel;
        shield = Player.ShieldLevel;
        rocket = Player.RocketLevel;
        ability = Player.Ability;
        map = Player.Map;
        reward = Player.CurrentReward;
        datetime = Player.dateTime.ToString();
        for (int i=0; i<3; i++) //кол-во абилок 3 + пак 
        {
            abilitylevel[i] = Player.AbilityLevel[i];
        }
        for (int i = 0; i < 4; i++) //кол-во абилок 3 + пак 
        {
            abilityopen[i] = Player.AbilityOpen[i];
        }
        for (int i = 0; i < 6; i++) 
        {
            mapopen[i] = Player.MapOpen[i];
        }
        muted = Player.muted;
        mutedmus = Player.mutedmus;
        language = Player.Language;
        for (int i = 0; i < 19; i++) //кол-во объектов - 19
        {
            obj[i] = Player.Objects[i];
        }
    }
    public void LoadShopData()
    {
        Player.SkinOpen[0] = true;
        for (int i = 1; i < n; i++)
        {
           Player.SkinOpen[i] = Skins[i];
        }
        Player.PuckSkillLevel = puck;
        Player.ShieldLevel = shield;
        Player.RocketLevel =  rocket;
        Player.Ability = ability;
        Player.Map = map;
        Player.CurrentReward = reward;
        Player.dateTime = DateTime.Parse(datetime);
        for (int i = 0; i < 3; i++) //кол-во абилок 3 + пак 
        {
            Player.AbilityLevel[i] = abilitylevel[i];
        }
        for (int i = 0; i < 4; i++) //кол-во абилок 3 + пак 
        {
            Player.AbilityOpen[i] = abilityopen[i];
        }
        Player.MapOpen[0] = true;
        for (int i = 1; i < 6; i++)
        {
            Player.MapOpen[i] = mapopen[i];
        }
        Player.muted = muted;
        Player.mutedmus = mutedmus;
        Player.Language = language;
        for (int i = 0; i < 19; i++) //кол-во объектов - 19
        {
            try
            {
                Player.Objects[i]=obj[i];
            }
            catch (Exception)
            {
                Player.Objects[i] = 0;
            }
        }
    }
    public void StartShop()
    {
        Player.SkinOpen[0] = true;
        for (int i = 1; i < n; i++)
        {
            Player.SkinOpen[i] = false;
        }
        Player.PuckSkillLevel = 0;
        Player.ShieldLevel = 0;
        Player.RocketLevel = 0;
        Player.Ability = 0;
        Player.Map = 0;
        Player.CurrentReward = 0;
        Player.dateTime = null;
        for (int i = 0; i < 3; i++) //кол-во абилок 3 + пак 
        {
            Player.AbilityLevel[i] = 0;
        }
        Player.AbilityOpen[0] = true;
        for (int i = 1; i < 4; i++) //кол-во абилок 3 + пак 
        {
            Player.AbilityOpen[i] = false;
        }
        Player.MapOpen[0] = true;
        for (int i = 1; i < 6; i++)
        {
            Player.MapOpen[i] = false;
        }
        Player.muted = false;
        Player.mutedmus = false;
        Player.Language = 0;
        for (int i = 0; i < 19; i++) //кол-во объектов - 19
        {
            Player.Objects[i] = 0;
        }
    }
}

[Serializable]
public class SaveAllData
{
    public SaveData data1 = new SaveData();
    public SaveShopData data2 = new SaveShopData();

    public void TakeAllData()
    {
        data1.TakeData();
        data2.TakeShopData();
    }

    public void LoadAllData()
    {
        data1.LoadData();
        data2.LoadShopData();
    }

    public void StartAllData()
    {
        data1.StartGame();
        data2.StartShop();
    }
}

