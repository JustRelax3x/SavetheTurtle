using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    public GameObject gameoverscreen;
    public GameObject ScoreAtTop;
    public Text MenuButton;
    public Text RestartButton;
    public Text Scoreattop, Antiscore;
    public Text secondsSurvivedUI;
    public Text Highscore;
    public Text Money, PirateBonus;
    public Text Pearl;
    public Image[] Hp;
    public GameObject stamina, reflection, timebutton, astral;
    public GameObject PuckButton;
    float scoremultiplayer = 50f;
    private AudioSource audiososka;
    public static bool gameover = false, antiscoreused;
    public static bool PuckSkill = false, PuckTimer = false, AbilityUsed = false, AbilityTimer = false;
    public static float CooldownPuck = 1f;
    float durationCooldown = 8f, PuckDuration;
    private Animator Stamina;
    public Image Pause;
    public Text[] PauseText;
    public GameObject Pausepanel, RespPanel, RespText;
    bool StaminaAnimActive = false, Paused = false, Tuto = false, Respawn;
    public Button VolumeButton, MusicButton;
    public AudioMixer Mixer;
    bool UnUsed = true, ABilityAnim = false;
    public Button AdButton;
    int skin, ability, astraldamage;
    float CooldownAbility = 1f, DurationAbility;
    public static int score, antiscore;
    public static int AdCounter = 0;
    MobAdsSimple a;
    AchInc Ach;
    public AudioClip Death;
    public GPGSAuthentication CloudSave;
    public GameObject PickUper, Tracks;
    public GameObject[] TracksImage;
    public AudioSource MainTheme;
    public AudioClip[] mainThemes;

    public GameObject X2;
    [HideInInspector] private const string leaderboards = "CgkI5vbs-fEIEAIQAw";
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
    public void OnClickMusic()
    {
        if (!Player.mutedmus)
        {
            Mixer.SetFloat("Music", -80f);
            Player.mutedmus = true;
            MusicButton.transform.GetChild(0).gameObject.SetActive(false);
            MusicButton.transform.GetChild(1).gameObject.SetActive(true);
            Tracks.SetActive(false);
        }
        else
        {
            Mixer.SetFloat("Music", 0f);
            Player.mutedmus = false;
            MusicButton.transform.GetChild(1).gameObject.SetActive(false);
            MusicButton.transform.GetChild(0).gameObject.SetActive(true);
            Tracks.SetActive(true);
        }
    }

    public void TrackChooser(int x)
    {
        PlayerPrefs.SetInt("Track", x);
        if (x == -1) x = TracksImage.Length - 1;
        for (int i = 0; i < TracksImage.Length; i++)
        {
            TracksImage[i].GetComponent<SpriteRenderer>().color = new Color(0.47f, 0.47f, 0.47f);
        }
        TracksImage[x].GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
    }

    public void TrackPlayer()
    {
        int x = PlayerPrefs.GetInt("Track");
        if (x == -1)
        {
            x = Random.Range(0, mainThemes.Length);
        }
        MainTheme.clip = mainThemes[x];
        MainTheme.Play();
    }

    private void Start()
    {
        gameover = false;
        Time.timeScale = 1;
        score = 0;
        antiscore = 0;
        Assets.SimpleLocalization.LocalizationManager.Read();
        switch (Player.Language)
        {
            case 0:
                Assets.SimpleLocalization.LocalizationManager.Language = "English";
                break;
            case 1:
                Assets.SimpleLocalization.LocalizationManager.Language = "Russian";
                break;
        }
        try
        {
            Tuto = Tutorial.tutorial;
        }
        catch (System.Exception)
        {
            Tuto = false;
        }
        if (!Tuto)
        {
            ScoreAtTop.SetActive(true);
            Scoreattop.text = 0f.ToString();
            AdButton.interactable = true;
            Antiscore.enabled = false;
        }
        PuckSkill = false;
        CooldownPuck = 1f;
        audiososka = GetComponent<AudioSource>();
        PuckDuration = Player.PuckSkillLevel * 0.1f + 0.5f;
        PuckTimer = false;
        Ach = new AchInc();
        UnUsed = true;
        if (Player.muted)
        {
            Mixer.SetFloat("Sound", -80f);
            VolumeButton.transform.GetChild(0).gameObject.SetActive(false);
            VolumeButton.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            Mixer.SetFloat("Sound", 0f);
            VolumeButton.transform.GetChild(1).gameObject.SetActive(false);
            VolumeButton.transform.GetChild(0).gameObject.SetActive(true);
        }
        if (Player.mutedmus)
        {
            Mixer.SetFloat("Music", -80f);
            MusicButton.transform.GetChild(0).gameObject.SetActive(false);
            MusicButton.transform.GetChild(1).gameObject.SetActive(true);
            Tracks.SetActive(false);
        }
        else
        {
            Mixer.SetFloat("Music", 0f);
            MusicButton.transform.GetChild(1).gameObject.SetActive(false);
            MusicButton.transform.GetChild(0).gameObject.SetActive(true);
            Tracks.SetActive(true);
        }
        a = gameObject.AddComponent<MobAdsSimple>();
        CloudSave = gameObject.AddComponent<GPGSAuthentication>();
        skin = Player.Skin;
        ability = Player.Ability;
        if (ability == 0) Stamina = stamina.GetComponent<Animator>();
        if (ability == 1)
        {
            durationCooldown = 6f - Player.AbilityLevel[0] * 0.25f ;
            if (skin == 12)
            {
                durationCooldown -= 1f;
            }
            DurationAbility = 0.1f;
            stamina.SetActive(false);
            reflection.SetActive(true);
            Stamina = reflection.GetComponent<Animator>();
        }
        else if (ability == 2) {
            durationCooldown = 10f;
            DurationAbility = 2.4f + Player.AbilityLevel[1] * 0.15f;
            if (skin == 13)
            {
                DurationAbility += 1.2f;
            }
            stamina.SetActive(false);
            timebutton.SetActive(true);
            Stamina = timebutton.GetComponent<Animator>();
        }
        else if (ability == 3)
        {
            astraldamage = 200 - 25 * Player.AbilityLevel[2];
            durationCooldown = 12f;
            DurationAbility = 4f;
            stamina.SetActive(false);
            astral.SetActive(true);
            Stamina = astral.GetComponent<Animator>();
            Antiscore.text = "-" + astraldamage.ToString();
        }
        if ((skin >= 3 && skin <= 6) || skin == 16)
        {
            durationCooldown -= 1f;
        }
        else if (skin == 11)
        {
            durationCooldown += 4f;
        }
        else if (skin == 18)
        {
            PickUper.SetActive(true);
        }
        antiscoreused = false;
        Respawn = false;
        TrackChooser(PlayerPrefs.GetInt("Track"));
        TrackPlayer();
    }

    void Update()
    {
        if (gameover)
        {
            Time.timeScale = 0;
            if (Input.touchCount > 0)
            {
                Touch Mytouch = Input.GetTouch(0);
                if (Mytouch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Mytouch.position);
                    if (Mathf.Abs(ray.origin.x - MenuButton.transform.position.x) < MenuButton.transform.localScale.x * 1.35f && Mathf.Abs(ray.origin.y - MenuButton.transform.position.y) < MenuButton.transform.localScale.y / 2f)
                    {
                        Time.timeScale = 1;
                        gameover = false;
                        SaveSystem.Load();
                        SceneManager.LoadScene(GameConstants.MenuScene);
                    }
                    else if (!Tuto && Mathf.Abs(ray.origin.x - RestartButton.transform.position.x) < RestartButton.transform.localScale.x * 1.35f && Mathf.Abs(ray.origin.y - RestartButton.transform.position.y) < RestartButton.transform.localScale.y / 2f)
                    {
                        Time.timeScale = 1;
                        gameover = false;
                        SaveSystem.Load();
                        SceneManager.LoadScene(GameConstants.GameScene);
                    }
                    else if (Tuto && Mathf.Abs(ray.origin.x - RestartButton.transform.position.x) < RestartButton.transform.localScale.x * 1.35f && Mathf.Abs(ray.origin.y - RestartButton.transform.position.y) < RestartButton.transform.localScale.y / 2f)
                    {
                        Time.timeScale = 1;
                        gameover = false;
                        SceneManager.LoadScene(GameConstants.TutorialScene);
                    }
                }
            }
        }
        if (!gameover)
        {
            if (!Tuto)
            {
                score = Mathf.RoundToInt(Time.timeSinceLevelLoad * scoremultiplayer - antiscore);
                if (score < 0)
                {
                    score = 0;
                }
                Scoreattop.text = score.ToString();
            }

            if (Input.touchCount > 0)
            {
                Touch Mytouch = Input.GetTouch(0);
                if (Mytouch.phase == TouchPhase.Began || Mytouch.phase == TouchPhase.Stationary || Mytouch.phase == TouchPhase.Moved)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Mytouch.position);
                    
                    if (Mathf.Abs(ray.origin.x - PuckButton.transform.position.x) < PuckButton.transform.localScale.x / 1.75f && Mathf.Abs(ray.origin.y - PuckButton.transform.position.y) < PuckButton.transform.localScale.y / 2f)
                    {
                        if (ability == 0)
                        {
                            if (!PuckSkill && Time.timeSinceLevelLoad > CooldownPuck && !Movement.Rocket)
                            {
                                PuckSkill = true;
                                PuckTimer = true;
                                PuckButton.GetComponent<Animator>().SetTrigger("Dis");
                                Stamina.SetTrigger("PuckOnStam");
                                StaminaAnimActive = true;
                                CooldownPuck = Time.timeSinceLevelLoad + PuckDuration + durationCooldown;
                            }
                        }
                        else if (!Movement.Rocket && Time.timeSinceLevelLoad > CooldownAbility)
                        {
                            CooldownAbility = Time.timeSinceLevelLoad + DurationAbility + durationCooldown;
                            AbilityUsed = true;
                            AbilityTimer = true;
                            PuckButton.GetComponent<Animator>().SetTrigger("Dis");
                            Stamina.SetTrigger("PuckOnStam");
                            if (ability == 2)
                            {
                                Time.timeScale = 0.6f; 
                            }
                            else if (ability == 3 && skin == 14)
                            {
                                antiscore += astraldamage;
                                Antiscore.GetComponent<Animation>().Play();
                            }
                        }
                    }
                    
                    if (!Paused && Mathf.Abs(ray.origin.x - Pause.transform.position.x) < Pause.transform.localScale.x * 1.5f && Mathf.Abs(ray.origin.y - Pause.transform.position.y) < Pause.transform.localScale.y * 1.5f)
                    {
                        Paused = true;
                        Pause.gameObject.SetActive(false);
                        Time.timeScale = 0;
                        if (!Tuto)
                        {
                            ScoreAtTop.SetActive(false);
                        }
                        Pausepanel.SetActive(true);
                    }
                }
            }
        }
        if (Paused)
        {

            if (Input.touchCount > 0)
            {
                Touch Mytouch = Input.GetTouch(0);
                if (Mytouch.phase == TouchPhase.Began || Mytouch.phase == TouchPhase.Stationary || Mytouch.phase == TouchPhase.Moved)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Mytouch.position);
                    if (Mathf.Abs(ray.origin.x - PauseText[0].transform.position.x) < PauseText[0].transform.localScale.x * 1.35f && Mathf.Abs(ray.origin.y - PauseText[0].transform.position.y) < PauseText[0].transform.localScale.y / 2f)
                    {
                        Paused = false;
                        Pause.gameObject.SetActive(true);
                        Pausepanel.SetActive(false);
                        if (!Tuto)
                        {
                            ScoreAtTop.SetActive(true);
                        }
                        Time.timeScale = 1f;
                    }
                    else if (!Tuto && Mathf.Abs(ray.origin.x - PauseText[1].transform.position.x) < PauseText[1].transform.localScale.x * 1.35f && Mathf.Abs(ray.origin.y - PauseText[1].transform.position.y) < PauseText[1].transform.localScale.y / 2f)
                    {
                        Time.timeScale = 1f;
                        SaveSystem.Load();
                        SceneManager.LoadScene(GameConstants.MenuScene);
                    }
                    else if (Tuto && Mathf.Abs(ray.origin.x - PauseText[1].transform.position.x) < PauseText[1].transform.localScale.x * 1.35f && Mathf.Abs(ray.origin.y - PauseText[1].transform.position.y) < PauseText[1].transform.localScale.y / 2f)
                    {
                        Tuto = false;
                        Time.timeScale = 1f;
                        SaveSystem.Load();
                        SceneManager.LoadScene(GameConstants.GameScene);
                    }
                }

            }
        }
        if (antiscoreused)
        {
            Antiscore.GetComponent<Animation>().Play();
            antiscoreused = false;
        }
        if (Time.timeSinceLevelLoad > CooldownPuck - 4f && StaminaAnimActive && ability == 0)
        {
            Stamina.SetTrigger("PuckOffStam");
            PuckButton.GetComponent<Animator>().SetTrigger("Act");
            StaminaAnimActive = false;
        }
        else if  (Time.timeSinceLevelLoad > CooldownAbility - durationCooldown && AbilityTimer)
        {
            AbilityTimer = false;
            ABilityAnim = true;
            if (ability == 2)
            {
                Time.timeScale = 1f;
            } 
        }
        else if (Time.timeSinceLevelLoad > CooldownAbility - 4f && ABilityAnim)
        {
            ABilityAnim = false;
            Stamina.SetTrigger("PuckOffStam");
            PuckButton.GetComponent<Animator>().SetTrigger("Act");

        }
        if (PuckTimer && Time.timeSinceLevelLoad > CooldownPuck - durationCooldown)
        {
            PuckTimer = false;
        }
        if (PuckSkill && Time.timeSinceLevelLoad > CooldownPuck - durationCooldown + 0.25f)
        {
            PuckSkill = false;
        }
        if (!gameover)
        {
            if (Movement.HpChanged)
            {
                if (Movement.hp == 5)
                {
                    Hp[3].gameObject.SetActive(true);
                    Hp[4].gameObject.SetActive(true);
                    Hp[0].gameObject.SetActive(true);
                    Hp[1].gameObject.SetActive(true);
                    Hp[2].gameObject.SetActive(true);
                }
                else if (Movement.hp == 4)
                {
                    Hp[3].gameObject.SetActive(true);
                    Hp[4].gameObject.SetActive(false);
                    Hp[0].gameObject.SetActive(true);
                    Hp[1].gameObject.SetActive(true);
                    Hp[2].gameObject.SetActive(true);
                }
                else if (Movement.hp == 3)
                {
                    Hp[3].gameObject.SetActive(false);
                    Hp[4].gameObject.SetActive(false);
                    Hp[0].gameObject.SetActive(true);
                    Hp[1].gameObject.SetActive(true);
                    Hp[2].gameObject.SetActive(true);
                }
                else if (Movement.hp == 2)
                {
                    Hp[3].gameObject.SetActive(false);
                    Hp[0].gameObject.SetActive(false);
                    Hp[1].gameObject.SetActive(true);
                    Hp[2].gameObject.SetActive(true);
                }
                else if (Movement.hp == 1)
                {
                    Hp[0].gameObject.SetActive(false);
                    Hp[1].gameObject.SetActive(false);
                    Hp[3].gameObject.SetActive(false);
                    Hp[2].gameObject.SetActive(true);
                }
                else if (Movement.hp < 1 && UnUsed && !Respawn && !Tuto)
                {
                    Respawn = true;
                    RespPanel.SetActive(true);
                    RespText.SetActive(true);
                    Time.timeScale = 0;
                }
                else if (Movement.hp < 1 && UnUsed && (Respawn || Tuto))
                {
                    OnGameOver();
                    UnUsed = false;
                }
                Movement.HpChanged = false;
            }
        }
    }
    public void OnGameOver()
    {
        gameoverscreen.SetActive(true);
        Stamina.SetTrigger("PuckOnStam"); 
        stamina.SetActive(false);
        PuckButton.SetActive(false);
        Pause.gameObject.SetActive(false);
        Hp[0].gameObject.SetActive(false);
        Hp[1].gameObject.SetActive(false);
        Hp[2].gameObject.SetActive(false);
        Hp[3].gameObject.SetActive(false);
        Hp[4].gameObject.SetActive(false);
        Time.timeScale = 0;
        if (!Tuto)        {
            RespPanel.SetActive(false);
            RespText.SetActive(false);
            ScoreAtTop.SetActive(false);
            reflection.SetActive(false);
            timebutton.SetActive(false);
            astral.SetActive(false);
            if (score > Player.highscore)
            {
                Player.highscore = score;
            }
            if (Player.highscore > 10000)
            {
                 Ach.UnlockRegular(1);
            }
            if (Social.localUser.authenticated)
            {
                Social.ReportScore(score, leaderboards, (bool success) => { });
            }
            Highscore.text = Player.highscore.ToString();
            secondsSurvivedUI.text = score.ToString();
            if (skin == 1 || skin == 17)
            {
                if (score >= 2500)
                {
                    int x;
                    PirateBonus.gameObject.SetActive(true);
                    if (skin == 1)
                    {
                        x = Random.Range(1, 10000);
                        if (x % 101 == 0)
                        {
                            PirateBonus.text = "+50";
                            Player.money += 50;
                        }
                        else if (x % 5 == 0)
                        {
                            PirateBonus.text = "+20";
                            Player.money += 20;
                        }
                        else if (x % 2 != 0)
                        {
                            PirateBonus.text = "+15";
                            Player.money += 15;
                        }
                        else
                        {
                            PirateBonus.text = "+10";
                            Player.money += 10;
                        }
                    }
                    else if (skin == 17)
                    {
                        x = Movement.Hplost * 5;
                        PirateBonus.text = "+" + x.ToString();
                        Player.money += x;
                    }
                }
            }
            Pearl.text = Mathf.RoundToInt(score / 50f /1.5f + 5f).ToString();
            int x2 = 1;
            if (Player.X2Pearls) {
                Player.X2Pearls = false;
                X2.SetActive(true);
                x2 = 2;
            }
            Player.money += Mathf.RoundToInt(x2 *(score / 50f / 1.5f + 5f));
            Money.text = Player.money.ToString();
            Player.Objects[18]++;
            Ach.UpdateIncremental(20);
            if (Player.money >= 10000)
            {
                Ach.UnlockRegular(2);
            }
        }
        audiososka.clip = Death;
        audiososka.Play();
        gameover = true;
        AdCounter++;
        if (AdCounter == 3)
        {
            AdCounter = 0;
            a.ShowAd(); 
        }
    }
}
