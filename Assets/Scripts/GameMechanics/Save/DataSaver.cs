using System.IO;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    private static readonly string _savePath = Application.persistentDataPath + "/PlayerData.json";

    public static void Save(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(_savePath, json);
    }

    public static PlayerData Load()
    {
        if (File.Exists(_savePath))
        {
            string json = File.ReadAllText(_savePath);
            return JsonUtility.FromJson<PlayerData>(json);
        }

        PlayerData freshData = new()
        {
            Username = "Guest",
            TotalScore = 0,
            HighScore = 0
        };
        Save(freshData);
        return freshData;
    }
}
