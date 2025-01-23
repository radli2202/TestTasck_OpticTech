using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuControll : MonoBehaviour
{
    [Header(" Loader  \n --------------")]
    
    [SerializeField] private GameObject loadingScreen;  // Панель загрузки (UI)
    [SerializeField] private Slider loadingSlider;      
    [SerializeField] private Text loadingText; 
    
    [Header(" Game  \n --------------")]
    
    private string[] playerTypes;
    private int curentmaxPlayers; 
    private bool is_Online;
    [SerializeField] private GameObject[] playerUIElements;
    [SerializeField] private int i_MaxPlayer;// Максимальное количество игроков
    [SerializeField] private GameObject 
        Cont_GameMod, 
        Con_PlayerSelect, 
        Con_StartGame;
    private int currentPlayerIndex;
    private void Start()
    {
        loadingScreen.SetActive(false);
        Cont_GameMod.SetActive(true);
        Con_PlayerSelect.SetActive(false);
        Con_StartGame.SetActive(false);
        playerTypes = new string[i_MaxPlayer];
        for (int i = 0; i < playerUIElements.Length; i++)
        {
            playerUIElements[i].SetActive(false); // Изначально скрываем все UI элементы игроков
        }
    }
    public void SelectGameMod(string _gameMod)
    {
        switch (_gameMod)
        {
            case "OnePlayer":
                is_Online = false;
                curentmaxPlayers = 1;
                break;
            case "TwoPlayer":
                is_Online = true;
                curentmaxPlayers = i_MaxPlayer;
                break;
        }
        Cont_GameMod.SetActive(false);
        Con_PlayerSelect.SetActive(true);
        ShowNextPlayerUI();
     
    }

    #region SelectPlayer

    public void SelectPlayer(string playerType)
    {
        playerTypes[currentPlayerIndex] = playerType;
        playerUIElements[currentPlayerIndex].SetActive(false);
        currentPlayerIndex++;
        
        // Если все игроки выбрали тип, начинаем игру
        if (AllPlayersSelected()) GameReady(); 
        else ShowNextPlayerUI();
        
    }

    private bool AllPlayersSelected()
    {
        if(currentPlayerIndex==curentmaxPlayers)return true;
        else return false;
    }
    private void ShowNextPlayerUI()
    {
        playerUIElements[currentPlayerIndex].SetActive(true);
    }

    #endregion
    private void GameReady()
    {
        Con_PlayerSelect.SetActive(false);
        Con_StartGame.SetActive(true);
    }
    
    //вызывается кнопкой из меню - Запустить игру
    public void StartGame(string sceneName)
    {
        string _player_2;
        if (is_Online)
        {
            _player_2 = playerTypes[1];
        }
        else
        {
            _player_2 = "bot";
        }
        SaveLoadSystem.Instance.SaveGameData(is_Online, playerTypes[0],_player_2 );
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }
    private IEnumerator LoadSceneAsyncCoroutine(string sceneName)
    {
        loadingScreen.SetActive(true);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        // Ожидаем завершения загрузки
        while (!asyncOperation.isDone)
        {
            // Обновляем прогресс загрузки
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f); 
            loadingSlider.value = progress;  
            loadingText.text = "Загрузка: " + (progress * 100f).ToString("F0") + "%";  // Текст с процентами
            yield return null; 
        }
        // После завершения загрузки скрываем экран загрузки 
        loadingScreen.SetActive(false);
    }

}
