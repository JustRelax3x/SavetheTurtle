using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public class GPGSAuthentication : MonoBehaviour
{
#if UNITY_ANDROID
    public static PlayGamesPlatform platform;
    public bool IsConnectedToGooogle = false;
    private bool IsSaving = false;
    private static bool UsedSave = false, TriedLogIn = false;
    public SaveAllData CloudData = new SaveAllData();
    [HideInInspector] private readonly string SAVE_NAME = "SaveGame";
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
            Social.localUser.Authenticate((bool success) =>
                {
                    if (!UsedSave)
                    {
                        OpenSaveToCloud(false);
                        MenuButtonPlay.UIUpdate();
                    }
                });
        }
    }
    public void ShowLeaderBoard()
    {
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (!UsedSave)
                {
                    OpenSaveToCloud(false);
                    MenuButtonPlay.UIUpdate();
                }
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
                if (!UsedSave)
                {
                    OpenSaveToCloud(false);
                    MenuButtonPlay.UIUpdate();
                }
            });
        }
        Social.ShowAchievementsUI();
    }

    public void PushHighscore()
    {
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (!UsedSave)
                {
                    OpenSaveToCloud(false);
                    MenuButtonPlay.UIUpdate();
                }
            });
        }
        if (Player.highscore > 0)
        {
            Social.ReportScore(Player.highscore, leaderboards, (bool success) => { });
        }
    }
    //cloud saving
    public void OpenSaveToCloud(bool saving)
    {
        if (Social.localUser.authenticated)
        {
            IsSaving = saving;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution
                (SAVE_NAME, DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime,
                SavedGameOpen);
        }
    }

    private void SavedGameOpen(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            if(IsSaving) //Game -> Cloud
            {
                CloudData.TakeAllData();
                byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(JsonUtility.ToJson(CloudData));
                SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(meta, update, data, SaveUpdate);
            }
            else //Cloud -> Game
            {
                ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(meta, ReadDataFromCloud);
            }
        } 
    }

    private void ReadDataFromCloud(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            string savedata = System.Text.ASCIIEncoding.ASCII.GetString(data);
            CloudData = JsonUtility.FromJson<SaveAllData>(savedata);
            CloudData.LoadAllData();
            UsedSave = true;
        }
    }

    private void SaveUpdate(SavedGameRequestStatus status, ISavedGameMetadata meta)
    {
    }
#endif
}
