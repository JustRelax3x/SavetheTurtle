using UnityEngine;
using Google.Play.Review;
using System.Collections;

public class Review : MonoBehaviour
{
    private ReviewManager _reviewManager;
    private PlayReviewInfo _playReviewInfo;

    private void Start()
    {
        if (Player.highscore > 5000 && !PlayerPrefs.HasKey("Review"))
        {
            StartCoroutine(RequestReview());
            PlayerPrefs.SetInt("Review", 1);
        }
        else if (Player.highscore > 10000 && PlayerPrefs.GetInt("Review") == 1)
        {
            StartCoroutine(RequestReview());
            PlayerPrefs.SetInt("Review", 2);
        }
    }

    IEnumerator RequestReview()
    {
        _reviewManager = new ReviewManager();

        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
        _playReviewInfo = requestFlowOperation.GetResult();

        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null; // Reset the object
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
    }
}
