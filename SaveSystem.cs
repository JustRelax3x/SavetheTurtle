using System;
using UnityEngine;
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
    public int reward;
    public bool[] Skins = new bool [GameConstants.SkinsLength], abilityopen = new bool[GameConstants.AbilityLength+1], mapopen = new bool[GameConstants.MapsLength];
    public int puck, shield, rocket, ability, language, map;
    public int[] abilitylevel = new int[GameConstants.AbilityLength];
    public bool muted, mutedmus;
    public long[] obj = new long [GameConstants.StatsObjectsLength];
    public string datetime;

    
    public void TakeShopData()
    {
        puck = Player.PuckSkillLevel;
        shield = Player.ShieldLevel;
        rocket = Player.RocketLevel;
        ability = Player.Ability;
        map = Player.Map;
        reward = Player.CurrentReward;
        datetime = Player.dateTime.ToString();
        for (int i=0; i< GameConstants.AbilityLength; i++)
        {
            abilitylevel[i] = Player.AbilityLevel[i];
        }
        Player.CopyTo(this);
        muted = Player.muted;
        mutedmus = Player.mutedmus;
        language = Player.Language;
        for (int i = 0; i < GameConstants.StatsObjectsLength; i++) 
        {
            obj[i] = Player.Objects[i];
        }
    }
    public void LoadShopData()
    {
        Skins[0] = true;
        mapopen[0] = true;
        Player.SaveFrom(this);
        Player.PuckSkillLevel = puck;
        Player.ShieldLevel = shield;
        Player.RocketLevel =  rocket;
        Player.Ability = ability;
        Player.Map = map;
        Player.CurrentReward = reward;
        Player.dateTime = DateTime.Parse(datetime);
        for (int i = 0; i < GameConstants.AbilityLength; i++)
        {
            Player.AbilityLevel[i] = abilitylevel[i];
        }
        Player.muted = muted;
        Player.mutedmus = mutedmus;
        Player.Language = language;
        for (int i = 0; i < GameConstants.StatsObjectsLength; i++) 
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
        for (int i = 0; i < GameConstants.SkinsLength; i++)
        {
            Skins[i] = i == 0;
        }
        Player.PuckSkillLevel = 0;
        Player.ShieldLevel = 0;
        Player.RocketLevel = 0;
        Player.Ability = 0;
        Player.Map = 0;
        Player.CurrentReward = 0;
        Player.dateTime = null;
        for (int i = 0; i < GameConstants.AbilityLength; i++)
        {
            Player.AbilityLevel[i] = 0;
        }
        for (int i = 0; i < GameConstants.AbilityLength+1; i++)
        {
            abilityopen[i] = i == 0;
        }
        for (int i = 0; i < GameConstants.MapsLength; i++)
        {
            mapopen[i] = (i==0 || i==GameConstants.RandomMap);
        }
        Player.SaveFrom(this);
        Player.muted = false;
        Player.mutedmus = false;
        Player.Language = 0;
        for (int i = 0; i < GameConstants.StatsObjectsLength; i++)
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

