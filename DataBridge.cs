using Firebase.Database;
using UnityEngine;

public class DataBridge : MonoBehaviour
{
    private const string IdKey = "Id";
    private DatabaseReference _reference;
    private SaveAllData _data = new SaveAllData();
    public void Start()
    {
        _reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void SaveDataToDB()
    {
        _data.TakeAllData();
        if (!PlayerPrefs.HasKey(IdKey)) {
            string id = (Random.Range(100, 999999) + _data.data1.hihgscore + _data.data1.money).ToString();
            PlayerPrefs.SetString(IdKey, id); 
        }
        _reference.Child("users").Child(PlayerPrefs.GetString(IdKey)).SetRawJsonValueAsync(JsonUtility.ToJson(_data));
    }

    public void CreateOrLoadFromDB()
    {
        if (!PlayerPrefs.HasKey(IdKey))
        {
            int id = Random.Range(100000, 500001) + Random.Range(100000, 500001) + Random.Range(100000, 500001) + Random.Range(1, 250) - Random.Range(1, 50);
            _reference.Child("users").Child(PlayerPrefs.GetString(IdKey)).SetRawJsonValueAsync(JsonUtility.ToJson(_data));
            PlayerPrefs.SetInt(IdKey, id);
            return;
        }
        LoadDataFromDB();
    }

    public void LoadDataFromDB()
    {
        if (!PlayerPrefs.HasKey(IdKey)) return;
        _reference = FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(PlayerPrefs.GetString(IdKey));
        _reference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                _data = JsonUtility.FromJson<SaveAllData>(task.Result.GetRawJsonValue());
                _data.LoadAllData();
            }
        });
    }
}
