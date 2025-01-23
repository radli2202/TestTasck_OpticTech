using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Scen_Controll : MonoBehaviour
{
   
    private PlayerController player_1,player_2;
    private PlayerController CurentPlayer;

    [SerializeField] private GameObject 
        Pref_playerGreen,
        Pref_playerRed, 
        Pref_Bot;

   
    
    private void OnEnable()
    {
        GameEvent.on_UpdateUI += CheckProgress;
        GameEvent.on_StartGame += StartGame;
        GameEvent.on_ReadyAction += ChangePlayer;
        GameEvent.on_PlayerReady += CheckReadyAllPlayer;
    }

    private void OnDisable()
    {
        GameEvent.on_UpdateUI -= CheckProgress;
        GameEvent.on_StartGame -= StartGame;
        GameEvent.on_ReadyAction -= ChangePlayer;
        GameEvent.on_PlayerReady -= CheckReadyAllPlayer;
    }

    private void StartGame()
    {
        GameObject go_player_1 = null;
        GameObject go_player_2 = null;
        
        string stplayer_1 = Scen_Model.Instance._player_1;
        string stplayer_2 = Scen_Model.Instance._player_2;

        
        if (Scen_Model.Instance.is_Onlain)
        {
            switch (stplayer_2)
            {
                case "Green":
                    go_player_2= Instantiate(Pref_playerGreen,Scen_Model.Instance.tr_Position_2.position,Quaternion.identity) as GameObject;
                    break;
                case "Red":
                    go_player_2= Instantiate(Pref_playerRed,Scen_Model.Instance.tr_Position_2.position,Quaternion.identity) as GameObject;
                    break;
            }
        }
        else
        {
            go_player_2= Instantiate(Pref_Bot,Scen_Model.Instance.tr_Position_2.position,Quaternion.identity) as GameObject;
        }
        
        switch (stplayer_1)
        {
            case "Green":
                go_player_1= Instantiate(Pref_playerGreen,Scen_Model.Instance.tr_Position_1.position,Quaternion.identity) as GameObject;
                break;
            case "Red":
                go_player_1= Instantiate(Pref_playerRed,Scen_Model.Instance.tr_Position_1.position,Quaternion.identity) as GameObject;
                break;
        }

        if (go_player_1.TryGetComponent(out PlayerController control))
        {
            go_player_1.transform.parent = Scen_Model.Instance.tr_Position_1;
            player_1 = control;
        }
        if (go_player_2.TryGetComponent(out PlayerController control2))
        {
            go_player_2.transform.parent = Scen_Model.Instance.tr_Position_2;
            player_2 = control2;
        }

        Scen_Model.Instance.curentPlayer = player_1._model;
        CurentPlayer=player_1;
        StartTurn();
        GameEvent.on_UpdateUI?.Invoke();
        GameEvent.on_NetRedyPlayer?.Invoke(1);
        

    }

    public void StartTurn()
    {
        // Логика начала хода
      
    }

    public void EndTurn()
    {
        // Логика окончания хода
        // Обновить состояние игры и отправить данные на сервер
    }

    private void ChangePlayer()
    {
        if (CurentPlayer == player_1)
        {
            CurentPlayer = null;
            CurentPlayer = player_2;
            GameEvent.on_NetRedyPlayer?.Invoke(2);
          
        }
        else
        {
            CurentPlayer = null;
            CurentPlayer = player_1;
            GameEvent.on_NetRedyPlayer?.Invoke(1);
        }

        Scen_Model.Instance.curentPlayer = CurentPlayer._model;
        Debug.Log("ChangePlayer  "+ CurentPlayer);
    }

    private void CheckReadyAllPlayer(int i)
    {
        //проверяем на каждом клиенте что оба игрока готовы
        // Лучше проверять только на мастер клиенте и вызывать сетевое событие о начале действий
        if (i == 2)
        {
            GameEvent.on_StartAction?.Invoke();
        }
    }

    private void CheckProgress()
    {
        var health1 = player_1._model.f_health;
        var health2 = player_2._model.f_health;

        if (health1 <= 0 || health2 <= 0)
        {
            var winner = health1 > health2 ? player_1 : player_2;
            var loser = winner == player_1 ? player_2 : player_1;

            GameEvent.on_Winner?.Invoke(winner);
            GameEvent.on_Loser?.Invoke(loser);
        }
    }

    public void ResetGame()
    {
        player_1.ResetGame();
        player_2.ResetGame();
        // Начинаем игру заново
    }
    
}
