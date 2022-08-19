using UnityEngine;
using UnityEngine.UI;

public class BackGrounds : MonoBehaviour
{
    public Image Back;
    public BackGround Ground;
    // Start is called before the first frame update
    void Start()
    {
        int map = Player.Map;
        if (map == 50)
        {
            while (map > 49)
            {
                int x = Random.Range(0, 6);
                if (Player.MapOpen[x])
                {
                    map = x;
                }
            }
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
