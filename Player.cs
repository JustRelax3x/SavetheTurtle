using System;

public class Player
{
    public static int highscore, money, Skin, Ability, Language, Map, CurrentReward;
    private static bool[] _skinOpen = new bool[GameConstants.SkinsLength];
    private static bool[] _abilityOpen = new bool[GameConstants.AbilityLength+1];
    private static bool[] _mapOpen = new bool[GameConstants.MapsLength];

    public static int PuckSkillLevel, ShieldLevel, RocketLevel;
    public static int[] AbilityLevel = new int[GameConstants.AbilityLength];
    public static bool muted, mutedmus, X2Pearls;
    public static long[] Objects = new long[GameConstants.StatsObjectsLength];
    public static DateTime? dateTime;

    public static bool BuyItem(ItemData itemData)
    {
        if (money < itemData.Cost) return false;
        money -= itemData.Cost;
        switch (itemData.ItemType)
        {
            case ItemType.Skin:
                _skinOpen[itemData.Id] = true;
                break;
            case ItemType.Ability:
                _abilityOpen[itemData.Id] = true;    
                break;
            case ItemType.Map:
                _mapOpen[itemData.Id] = true;
                break;
        }
        SaveSystem.Load();
        return true;
    }

    public static bool GetItemOpen(ItemData itemData)
    {
        switch (itemData.ItemType)
        {
            case ItemType.Skin:
                return _skinOpen[itemData.Id];
            case ItemType.Ability:
                return _abilityOpen[itemData.Id];
            case ItemType.Map:
                return _mapOpen[itemData.Id];
        }
        return false;
    }

    public static bool HasMapOpen(int i)
    {
        return _mapOpen[i];
    }

    public static void CopyTo(SaveShopData shopData)
    {
        _skinOpen.CopyTo(shopData.Skins,0);
        _abilityOpen.CopyTo(shopData.abilityopen, 0);
        _mapOpen.CopyTo(shopData.mapopen, 0);
    } 

    public static void SaveFrom (SaveShopData shopData)
    {
        shopData.Skins.CopyTo(_skinOpen, 0);
        shopData.abilityopen.CopyTo(_abilityOpen, 0);
        shopData.mapopen.CopyTo(_mapOpen, 0);
    }
}