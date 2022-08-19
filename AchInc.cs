using UnityEngine;
using GooglePlayGames;

public class AchInc : MonoBehaviour
{
    public void UpdateIncremental(int i)
    {
        if (!Social.localUser.authenticated) return;
        switch (i)
        {
            case 1:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_have_a_snack, 1, null);
                break;
            case 2:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_hideandseek, 1, null);
                break;
            case 3:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_i_am_speed, 1, null);
                break;
            case 4:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_shields_up, 1, null);
                break;
            case 5:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_scavenger, 1, null);
                break;
            case 6:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_foodie, 1, null);
                break;
            case 7:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_screw_it_up, 1, null);
                break;
            case 8:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_still_please, 1, null);
                break;
            case 9:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_safety, 1, null);
                break;
            case 10:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_shopping, 1, null);
                break;
            case 11:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_caps, 1, null);
                break;
            case 12:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_into_pieces, 1, null);
                break;
            case 13:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_full_tank, 1, null);
                break;
            case 14:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_storage, 1, null);
                break;
            case 15:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_drop_the_anchor, 1, null);
                break;
            case 16:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_saw, 1, null);
                break;
            case 17:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_sheikh, 1, null);
                break;
            case 18:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_fire, 1, null);
                break;
            case 19:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_ouch, 1, null);
                break;
            case 20:
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_wasted, 1, null);
                break;
        }
    }

    public void UnlockRegular(int i)
    {
        if (!Social.localUser.authenticated) return;
        switch (i) {
            case 1:
            Social.ReportProgress(GPGSIds.achievement_fasterdeeperstronger, 100f, null);
                break;
            case 2:
                Social.ReportProgress(GPGSIds.achievement_richman, 100f, null);
                break;
            case 3:
                Social.ReportProgress(GPGSIds.achievement_collector, 100f, null);
                break;
            case 4:
                Social.ReportProgress(GPGSIds.achievement_first_steps, 100f, null);
                break;
        }
    }
}

