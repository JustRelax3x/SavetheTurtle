using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class DailyRewards : MonoBehaviour
{
    [SerializeField]
    private Text status;
    [SerializeField]
    private Button clmBtn;

    [Space(5)]
    [SerializeField]
    private RewardPref rewardPref;
    [SerializeField]
    private Transform rewardGrid;

    [Space(5)]
    [SerializeField]
    private List<int> rewards;

    public Text pearls;

    private List<RewardPref> rewardPrefabs;

    private int currentStreak;

    private DateTime? lastClaimTime;
    private bool CanClaimReward, Used = false;
    private int maxStreakCount = 7;
    private float claimCooldown = 24f;
    private float claimDeadline = 48f;
    [Space(5)]
    public GameObject panel;
    public GameObject MainMenu;
    public GameObject Notificator;
    NotificationManager notification;
    public Text daily, reward;
    public GameObject _camera;
    GPGSAuthentication CloudSave;
    private DateTime? _serverDate;

    private void Awake()
    {
        notification = Notificator.GetComponent<NotificationManager>();
        CloudSave = _camera.GetComponent<GPGSAuthentication>();
    }
    private void InitPrefabs()
    {
        if (!Used)
        {
            rewardPrefabs = new List<RewardPref>();
            rewards = new List<int>();
            for (int i = 0; i < maxStreakCount; i++)
            {
                rewardPrefabs.Add(Instantiate(rewardPref, rewardGrid, false));
                rewards.Add(0); //pearls only
            }
            Used = true;
        }
    }

    private IEnumerator RewardStateUpdater()
    {
        while (true)
        {
            UpdateRewardState();
            yield return new WaitForSeconds(1);
        }
    }
    private void UpdateRewardState()
    {
        CanClaimReward = true;
        if (_serverDate.HasValue) {
            TimeSpan timeSpan = new TimeSpan(DateTime.UtcNow.Ticks);
            if (lastClaimTime.HasValue) timeSpan = DateTime.UtcNow - lastClaimTime.Value;
            if (Math.Abs(DateTime.UtcNow.Subtract(_serverDate.Value).TotalHours) >= 18f)
            {
                CanClaimReward = false;
                currentStreak = 0;
                Player.CurrentReward = currentStreak;
                SaveSystem.Load();
            }
            if (lastClaimTime.HasValue)
            {

                if (timeSpan.TotalHours > claimDeadline)
                {
                    lastClaimTime = null;
                    Player.dateTime = lastClaimTime;
                    currentStreak = 0;
                    Player.CurrentReward = currentStreak;
                    SaveSystem.Load();
                }
                else if (timeSpan.TotalHours < claimCooldown)
                {
                    CanClaimReward = false;
                }
            }
        }
        UpdateRewardUI();
    }

    private void UpdateRewardUI()
    {
        if (!_serverDate.HasValue)
        {
            CanClaimReward = false;
        }
        clmBtn.interactable = CanClaimReward;
        if (CanClaimReward)
        {
            status.text = Assets.SimpleLocalization.LocalizationManager.Localize("rew");
        }
        else
        {
            if (_serverDate.HasValue)
            {
                var nextClaimTime = lastClaimTime.Value.AddHours(claimCooldown);
                var currentClaimcooldown = nextClaimTime - DateTime.UtcNow;
                string cd = $"{currentClaimcooldown.Hours:D2}:{currentClaimcooldown.Minutes:D2}:{currentClaimcooldown.Seconds:D2}";

                status.text = Assets.SimpleLocalization.LocalizationManager.Localize("rewcl") + $" {cd} \n" + Assets.SimpleLocalization.LocalizationManager.Localize("rewdn");
            }
            else
            {
                status.text = Assets.SimpleLocalization.LocalizationManager.Localize("inet");
            }
        }
        for (int i=0; i < rewardPrefabs.Count; i++)
        {
            rewardPrefabs[i].SetRewardData(i, currentStreak, rewards[i]);
        }
    }

    public void ClaimReward()
    {
        if (!CanClaimReward) return;

        switch (rewards[currentStreak])
        {
            case 0:
                Player.money += rewardPrefabs[currentStreak].rewardPearl[currentStreak];
                pearls.text = Player.money.ToString();
                    break;
        }
        lastClaimTime = DateTime.UtcNow;
        Player.dateTime = lastClaimTime;
        currentStreak = (currentStreak + 1) % maxStreakCount;
        Player.CurrentReward = currentStreak;
        SaveSystem.Load();
        notification.OnUse();

        UpdateRewardState();
    }
    public void ShowDaily() {
         if (!_serverDate.HasValue || DateTime.UtcNow.Subtract(_serverDate.Value).TotalMinutes >= 30) { 
        _serverDate = GetNetTime();
            }
        MainMenu.SetActive(false);
        panel.SetActive(true);
        lastClaimTime = Player.dateTime;
        currentStreak = Player.CurrentReward;
        pearls.text = Player.money.ToString();
        if (Assets.SimpleLocalization.LocalizationManager.Language == "English")
        {
            daily.text = "     " + Assets.SimpleLocalization.LocalizationManager.Localize("daily");
            reward.text = Assets.SimpleLocalization.LocalizationManager.Localize("rewards");
        }
        else
        {
            daily.text = Assets.SimpleLocalization.LocalizationManager.Localize("daily");
            reward.text = "          " + Assets.SimpleLocalization.LocalizationManager.Localize("rewards");
        }
        InitPrefabs();
        StartCoroutine(RewardStateUpdater());
    }

    public void HideDaily()
    {
        panel.SetActive(false);
        MainMenu.SetActive(true); 
    }

    public DateTime? GetNetTime()
    {
        try
        {
            var myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
            var response = myHttpWebRequest.GetResponse();
            if (response != null)
            {
                string todaysDates = response.Headers["date"];
                return DateTime.ParseExact(todaysDates,
                                           "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                                           CultureInfo.InvariantCulture.DateTimeFormat,
                                           DateTimeStyles.AssumeUniversal);
            }
            return null;
        }
        catch (Exception) {
            return null;
        }
    }
}
