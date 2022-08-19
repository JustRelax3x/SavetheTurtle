using System;

public class Player
{
    public static int highscore, money, Skin, Ability, Language, Map,CurrentReward;
    public static bool[] SkinOpen = new bool[20], AbilityOpen = new bool[4], MapOpen = new bool[6];
    public static int PuckSkillLevel, ShieldLevel, RocketLevel;
    public static int[] AbilityLevel = new int[3]; //пак не входит 0 - искажение, 1 - замедло, 2 - призрак
    public static bool muted, mutedmus, X2Pearls;
    public static long[] Objects = new long[19];
    public static DateTime? dateTime;
}