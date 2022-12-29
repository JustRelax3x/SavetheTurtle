using UnityEngine;
using UnityEngine.UI;

public class RewardPref : MonoBehaviour
{
    [SerializeField]
    private Image background;
    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color currentColor;

    [Space(5)]
    [SerializeField]
    private Text dayText;
    [SerializeField]
    private Text rewardValue;


    [Space(5)]
    [SerializeField]
    private Image rewardIcon;
    [SerializeField]
    private Sprite pearlIcon;

    public int[] rewardPearl = { 100, 200, 300, 500, 700, 900, 1500 };

    public void SetRewardData(int day, int currentStreak, int reward)
    {
        dayText.text = Assets.SimpleLocalization.LocalizationManager.Localize("day") + $" {day + 1}"; 
        switch (reward)
        {
            case 0:
                rewardIcon.sprite = pearlIcon;
                rewardValue.text = rewardPearl[day].ToString();
                break;
        }
        background.color = day == currentStreak ? currentColor : defaultColor;
    }
}
