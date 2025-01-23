using System;
using UnityEngine;
using System.IO;
public class SaveLoadSystem : MonoBehaviour
{
    public static SaveLoadSystem Instance;
    private GameData gameData;
    public string 
        Load_Player_1,
        Load_Player_2;
    public bool Load_is_Onlain;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameData = new GameData();
    }

    public void SaveGameData(bool is_Online,string _Player_1_Tupe,string _Player_2_Tupe)
    {
        // Присваиваем значения переменным
        gameData.player1Type = _Player_1_Tupe;
        gameData.player2Type = _Player_2_Tupe;
        gameData.isOnline = is_Online;
        // Преобразуем данные в JSON строку
        string json = JsonUtility.ToJson(gameData);
        // Путь для сохранения данных (например, в папке для данных приложения)
        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
        // Записываем JSON строку в файл
        File.WriteAllText(filePath, json);
        Debug.Log("Данные сохранены в " + filePath);
    }


    public void LoadGameData()
    {
        // Путь к файлу, в котором хранятся данные
        string filePath = Path.Combine(Application.persistentDataPath, "gameData.json");
        if (File.Exists(filePath))
        {
            // Читаем содержимое файла
            string json = File.ReadAllText(filePath);
            // Десериализуем JSON строку обратно в объект GameData
            gameData = JsonUtility.FromJson<GameData>(json);

            Load_Player_1= gameData.player1Type;
            Load_Player_2= gameData.player2Type;
            Load_is_Onlain = gameData.isOnline;
        }
    }
}
