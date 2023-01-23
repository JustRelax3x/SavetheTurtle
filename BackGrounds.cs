using UnityEngine;
using UnityEngine.UI;

public class BackGrounds : MonoBehaviour
{
    public Image Back;
    public BackGround Ground;
    void Awake()
    {
        int map = Player.Map;
        if (map == GameConstants.RandomMap)
        {
            do map = Random.Range(0, GameConstants.MapsLength);
            while (map != GameConstants.RandomMap && Player.HasMapOpen(map));
        }
        switch (map)
        {
            case 0:
                Back.sprite = Ground.BackGrounds[0];
                break;
            case 1:
                Back.sprite = Ground.BackGrounds[1];
                break;
            case 2:
                Back.sprite = Ground.BackGrounds[2];
                break;
            case 3:
                Back.sprite = Ground.BackGrounds[3];
                break;
            case 4:
                Back.sprite = Ground.BackGrounds[4];
                break;
            case 5:
                Back.sprite = Ground.BackGrounds[5];
                break;
        }
    }

}
