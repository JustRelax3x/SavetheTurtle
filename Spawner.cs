using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject[] LevelPrefabs;
    public GameObject[] players;
    public GameObject BlackPanel;
    Movement player;
    public GameObject PuckButton1;
    public Image Pause;
    public AnimationClip[] DayNight;
    public Vector2 SecondsWaitBetweenSpawnMinMax;
    private float NextSpawnTime = 1f;
    private float NextSpawnPowerUp = 10f;
    private readonly float ScoreforMiddlePart = 3000f, ScoreForHardPart = 6000f, ScoreHardcore = 20000f;
    Vector2 screenHalfsize;
    Vector2 SpawnPosition;
    bool Spawned = false, Tuto = false, Used = false, Black = false;
    int skin, bonusloc = 6000;
    public static int DayTime = 0;

    void Start()
    {
        try
        {
            Tuto = Tutorial.tutorial;
        }
        catch (System.Exception)
        {
            Tuto = false;
        }
        skin = Player.Skin;
        if (!Tuto)
        {
            player = Instantiate(players[skin], players[skin].transform.position, players[skin].transform.rotation).GetComponent<Movement>();
        }
        else
        {
            player = Instantiate(players[0], players[0].transform.position, players[0].transform.rotation).GetComponent<Movement>();
        }
        player.Button_center_x = PuckButton1.transform.position.x;
        player.Button_center_y = PuckButton1.transform.position.y;
        player.Button_Scale_x = PuckButton1.transform.localScale.x;
        player.Pause_x = Pause.transform.position.x;
        player.Pause_y = Pause.transform.position.y;
        player.PauseScale_x = Pause.transform.localScale.x;
        player.PauseScale_y = Pause.transform.localScale.y;
        screenHalfsize = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        SpawnPosition = new Vector2(0, screenHalfsize.y * 4f);
        DayTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Tuto)
        {
            if (Gameover.score > bonusloc - 1500 && Used)
            {
                Used = false;
            }
            if (Time.timeSinceLevelLoad > NextSpawnTime && Gameover.score < ScoreforMiddlePart)
            {
                if (Time.timeSinceLevelLoad > NextSpawnPowerUp) //уровни с паверапками, легкие
                {
                    if (skin != 2)
                    {
                        NextSpawnPowerUp = Time.timeSinceLevelLoad + Random.Range(15f, 20.1f);
                    }
                    else { NextSpawnPowerUp = Time.timeSinceLevelLoad + Random.Range(15f, 17.5f); }
                    float SecondsWaitBetweenSpawn = Mathf.Lerp(SecondsWaitBetweenSpawnMinMax.y, SecondsWaitBetweenSpawnMinMax.x, Difficulty.GetDifficultyPercent());
                    NextSpawnTime = Time.timeSinceLevelLoad + SecondsWaitBetweenSpawn;
                    Instantiate(LevelPrefabs[Random.Range(307, 352)], SpawnPosition, Quaternion.identity);  //легкие (надо ставить на 1 больше)
                }
                else // легкие уровни 
                {
                    float SecondsWaitBetweenSpawn = Mathf.Lerp(SecondsWaitBetweenSpawnMinMax.y, SecondsWaitBetweenSpawnMinMax.x, Difficulty.GetDifficultyPercent());
                    NextSpawnTime = Time.timeSinceLevelLoad + SecondsWaitBetweenSpawn;
                    Instantiate(LevelPrefabs[Random.Range(0, 56)], SpawnPosition, Quaternion.identity); // обязательно поменять кол-во легких уровней(надо ставить на 1 больше)
                }
            }
            else if (Time.timeSinceLevelLoad > NextSpawnTime && Gameover.score < ScoreForHardPart)
            {
                if (!Used && Gameover.score > bonusloc - 50)
                {
                    Used = true;
                    if (bonusloc % 10000 == 0)
                    {
                        bonusloc += 6000;
                        DayTime = 0;
                    }
                    else
                    {
                        bonusloc += 4000;
                        DayTime = 1;
                    }
                    float SecondsWaitBetweenSpawn = Mathf.Lerp(SecondsWaitBetweenSpawnMinMax.y, SecondsWaitBetweenSpawnMinMax.x, Difficulty.GetDifficultyPercent());
                    NextSpawnTime = Time.timeSinceLevelLoad + SecondsWaitBetweenSpawn + 2f;
                    Instantiate(LevelPrefabs[427], SpawnPosition, Quaternion.identity);
                    if (!Black)
                    {
                        Black = true;
                        BlackPanel.GetComponent<Animation>().clip = DayNight[0];
                        BlackPanel.GetComponent<Animation>().Play();
                    }
                    else
                    {
                        Black = false;
                        BlackPanel.GetComponent<Animation>().clip = DayNight[1];
                        BlackPanel.GetComponent<Animation>().Play();
                    }
                }
                else if (Time.timeSinceLevelLoad > NextSpawnPowerUp) //уровни с паверапками, легкие + средние
                {
                    if (skin != 2)
                    {
                        NextSpawnPowerUp = Time.timeSinceLevelLoad + Random.Range(20f, 30f);
                    }
                    else { NextSpawnPowerUp = Time.timeSinceLevelLoad + Random.Range(20f, 24f); }
                    float SecondsWaitBetweenSpawn = Mathf.Lerp(SecondsWaitBetweenSpawnMinMax.y, SecondsWaitBetweenSpawnMinMax.x, Difficulty.GetDifficultyPercent());
                    NextSpawnTime = Time.timeSinceLevelLoad + SecondsWaitBetweenSpawn;
                    Instantiate(LevelPrefabs[Random.Range(352, 427)], SpawnPosition, Quaternion.identity);  //(надо ставить на 1 больше)
                }
                else // легкие + средние уровни 
                {
                    float SecondsWaitBetweenSpawn = Mathf.Lerp(SecondsWaitBetweenSpawnMinMax.y, SecondsWaitBetweenSpawnMinMax.x, Difficulty.GetDifficultyPercent());
                    NextSpawnTime = Time.timeSinceLevelLoad + SecondsWaitBetweenSpawn;
                    Instantiate(LevelPrefabs[Random.Range(56, 141)], SpawnPosition, Quaternion.identity); // обязательно поменять кол-во легких + средних уровней(надо ставить на 1 больше)
                }
            }
            else if(Time.timeSinceLevelLoad > NextSpawnTime && Gameover.score < ScoreHardcore)
            {
                if (!Used && Gameover.score > bonusloc - 50)
                {
                    Used = true;
                    if (bonusloc % 10000 == 0)
                        bonusloc += 6000;
                    else bonusloc += 4000;
                    float SecondsWaitBetweenSpawn = Mathf.Lerp(SecondsWaitBetweenSpawnMinMax.y, SecondsWaitBetweenSpawnMinMax.x, Difficulty.GetDifficultyPercent());
                    NextSpawnTime = Time.timeSinceLevelLoad + SecondsWaitBetweenSpawn + 2f;
                    Instantiate(LevelPrefabs[427], SpawnPosition, Quaternion.identity);
                    if (!Black)
                    {
                        Black = true;
                        BlackPanel.GetComponent<Animation>().clip = DayNight[0];
                        BlackPanel.GetComponent<Animation>().Play();
                    }
                    else
                    {
                        Black = false;
                        BlackPanel.GetComponent<Animation>().clip = DayNight[1];
                        BlackPanel.GetComponent<Animation>().Play();
                    }
                }
                else if (Time.timeSinceLevelLoad > NextSpawnPowerUp) //уровни с паверапками, легкие + средние
                {
                    if (skin != 2)
                    {
                        NextSpawnPowerUp = Time.timeSinceLevelLoad + Random.Range(20f, 30f);
                    }
                    else { NextSpawnPowerUp = Time.timeSinceLevelLoad + Random.Range(20f, 24f); }
                    float SecondsWaitBetweenSpawn = Mathf.Lerp(SecondsWaitBetweenSpawnMinMax.y, SecondsWaitBetweenSpawnMinMax.x, Difficulty.GetDifficultyPercent());
                    NextSpawnTime = Time.timeSinceLevelLoad + SecondsWaitBetweenSpawn;
                    Instantiate(LevelPrefabs[Random.Range(352, 427)], SpawnPosition, Quaternion.identity);  //(надо ставить на 1 больше)
                }
                else // средние + сложные
                {
                    float SecondsWaitBetweenSpawn = Mathf.Lerp(SecondsWaitBetweenSpawnMinMax.y, SecondsWaitBetweenSpawnMinMax.x, Difficulty.GetDifficultyPercent());
                    NextSpawnTime = Time.timeSinceLevelLoad + SecondsWaitBetweenSpawn;
                    Instantiate(LevelPrefabs[Random.Range(56, 297)], SpawnPosition, Quaternion.identity); // обязательно поменять кол-во легких + средних уровней(надо ставить на 1 больше)
                }
            }
            else if (Time.timeSinceLevelLoad > NextSpawnTime)
            {
                if (!Used && Gameover.score > bonusloc - 50)
                {
                    Used = true;
                    if (bonusloc % 10000 == 0)
                        bonusloc += 6000;
                    else bonusloc += 4000;
                    float SecondsWaitBetweenSpawn = Mathf.Lerp(SecondsWaitBetweenSpawnMinMax.y, SecondsWaitBetweenSpawnMinMax.x, Difficulty.GetDifficultyPercent());
                    NextSpawnTime = Time.timeSinceLevelLoad + SecondsWaitBetweenSpawn + 2f;
                    Instantiate(LevelPrefabs[427], SpawnPosition, Quaternion.identity);
                    if (!Black)
                    {
                        Black = true;
                        BlackPanel.GetComponent<Animation>().clip = DayNight[0];
                        BlackPanel.GetComponent<Animation>().Play();
                    }
                    else
                    {
                        Black = false;
                        BlackPanel.GetComponent<Animation>().clip = DayNight[1];
                        BlackPanel.GetComponent<Animation>().Play();
                    }
                }
                else if (Time.timeSinceLevelLoad > NextSpawnPowerUp) //уровни с паверапками, легкие + средние
                {
                    if (skin != 2)
                    {
                        NextSpawnPowerUp = Time.timeSinceLevelLoad + Random.Range(20f, 30f);
                    }
                    else { NextSpawnPowerUp = Time.timeSinceLevelLoad + Random.Range(20f, 24f); }
                    float SecondsWaitBetweenSpawn = Mathf.Lerp(SecondsWaitBetweenSpawnMinMax.y, SecondsWaitBetweenSpawnMinMax.x, Difficulty.GetDifficultyPercent());
                    NextSpawnTime = Time.timeSinceLevelLoad + SecondsWaitBetweenSpawn;
                    Instantiate(LevelPrefabs[Random.Range(352, 427)], SpawnPosition, Quaternion.identity);  //(надо ставить на 1 больше)
                }
                else // средние + сложные
                {
                    float SecondsWaitBetweenSpawn = Mathf.Lerp(SecondsWaitBetweenSpawnMinMax.y, SecondsWaitBetweenSpawnMinMax.x, Difficulty.GetDifficultyPercent());
                    NextSpawnTime = Time.timeSinceLevelLoad + SecondsWaitBetweenSpawn;
                    Instantiate(LevelPrefabs[Random.Range(56, 307)], SpawnPosition, Quaternion.identity); // обязательно поменять кол-во легких + средних уровней(надо ставить на 1 больше)
                }
            }
        }
        else
        {
            if (Time.timeSinceLevelLoad > 26f && !Spawned)
            {
                Instantiate(LevelPrefabs[1], SpawnPosition, Quaternion.identity);
                Spawned = true;
            }
        }
    }
}
