using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GPGSAuthentication : MonoBehaviour
{
#if UNITY_ANDROID
    public static PlayGamesPlatform platform;
    public bool IsConnectedToGooogle = false;
    private static bool TriedLogIn = false;
    [HideInInspector] private const string leaderboards = "CgkI5vbs-fEIEAIQAw";

    private void Awake()
    {
        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
            PlayGamesPlatform.InitializeInstance(config);
            platform = PlayGamesPlatform.Activate();
        }
        if (!TriedLogIn)
        {
            TriedLogIn = true;
            Social.localUser.Authenticate((bool success) =>{});
        }
    }
    public void ShowLeaderBoard()
    {
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (!success) return;
            });
        }
        Social.ShowLeaderboardUI();
    }

    public void ShowAchPanel()
    {
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (!success) return;
            });
        }
        Social.ShowAchievementsUI();
    }

    public void PushHighscore()
    {
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) => { if (!success) return; });
        }
        if (Player.highscore > 0)
        {
            Social.ReportScore(Player.highscore, leaderboards, (bool success) => { });
        }
    }
#endif
}
