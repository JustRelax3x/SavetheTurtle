using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject[] TutorialPanels;
    public static bool tutorial = false;
    float Timer = 0f;
    bool TutuIsActive = false;
    public GameObject textfinish;
    AchInc Ach;


    void Awake()
    {
        if (TutorialPanels.Length > 0)
        {
            tutorial = true;
            Player.Skin = 0;
            Player.Ability = 0;
        }
        else
        {
            tutorial = false;
        }
    }
    private void Start()
    {
        Ach = new AchInc();
    }
    void FixedUpdate()
    {
        if (tutorial && !Gameover.gameover)
        {
            if (Time.timeSinceLevelLoad > 1f && Time.timeSinceLevelLoad < 1.2f)
            {
                TutorialPanels[0].SetActive(true);
                Timer = 4f;
                TutuIsActive = true;
            }
            if (Time.timeSinceLevelLoad > Timer && TutuIsActive)
            {
                TutorialPanels[0].SetActive(false);
                TutorialPanels[1].SetActive(false);
                TutorialPanels[2].SetActive(false);
                TutorialPanels[3].SetActive(false);
                TutuIsActive = false;
            }
            if (Time.timeSinceLevelLoad > 6f && Time.timeSinceLevelLoad < 6.2f)
            {
                TutorialPanels[1].SetActive(true);
                Timer = 10f;
                TutuIsActive = true;
            }
            else if (Time.timeSinceLevelLoad > 12f && Time.timeSinceLevelLoad < 12.2f)
            {
                TutorialPanels[2].SetActive(true);
                Timer = 18f;
                TutuIsActive = true;
            } else if (Time.timeSinceLevelLoad > 20f && Time.timeSinceLevelLoad < 20.2f)
            {
                TutorialPanels[3].SetActive(true);
                Timer = 25f;
                TutuIsActive = true;
            }
            if (Time.timeSinceLevelLoad > 30f)
            {
                if (Player.money == 0 && Player.highscore == 0) {
                    Player.money += 500;
                    textfinish.GetComponent<UnityEngine.UI.Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("perf");
                }
                else {
                    textfinish.GetComponent<UnityEngine.UI.Text>().text = Assets.SimpleLocalization.LocalizationManager.Localize("trashtext");
                }
                Ach.UnlockRegular(4);
                SaveSystem.Load();
                textfinish.SetActive(true);
            }
            if (Time.timeSinceLevelLoad > 34f)
            {
                tutorial = false; 
                UnityEngine.SceneManagement.SceneManager.LoadScene(GameConstants.GameScene);
            }
        }
    }
}
