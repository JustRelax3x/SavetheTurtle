using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Slider _bar;
    void Start()
    {
        Load();
    }

    public void Load()
    {
        //добавить вход в аккаунт + загрузка данных.
        

        StartCoroutine(LoadAsync());
    }

    IEnumerator  LoadAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Menu");

        asyncLoad.allowSceneActivation = false; 
        while (!asyncLoad.isDone)
        {
            _bar.value = asyncLoad.progress;
            if (!asyncLoad.allowSceneActivation && Time.timeSinceLevelLoad > 1f) 
            {
                    asyncLoad.allowSceneActivation = true;
            }
            
            yield return null;
        }
    }
}
