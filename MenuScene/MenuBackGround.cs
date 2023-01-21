using UnityEngine;
using UnityEngine.UI;

public class MenuBackGround : MonoBehaviour
{
    public Image Back;
    public BackGround Ground;
    void Start()
    {
        int map = Random.Range(0, 6);      
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
        }
    }
}

