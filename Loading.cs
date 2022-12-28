using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField]
    private Slider _bar;
    [SerializeField]
    private DataBridge _bridge;

    private Task _task;
    private readonly WaitForSecondsRealtime _wait = new WaitForSecondsRealtime(1f);
    void Start()
    {
        Load();
    }

    public void Load()
    {
        _task = Task.Factory.StartNew(() => _bridge.CreateOrLoadFromDB());
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GameConstants.MenuScene);

        asyncLoad.allowSceneActivation = false; 
        while (!asyncLoad.isDone)
        {
            _bar.value = asyncLoad.progress;
            if (_task.IsCompleted || Time.timeSinceLevelLoad > 8f) 
            {
                asyncLoad.allowSceneActivation = true;
                _task.Dispose();
            }
            
            yield return _wait;
        }
    }
}
