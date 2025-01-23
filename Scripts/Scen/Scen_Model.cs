using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scen_Model : MonoBehaviour
{
    public bool is_Onlain;
    public static Scen_Model Instance;
    
    public PlayerModel curentPlayer;
    public PlayerController curentControll;

    public string _player_1, _player_2;
    public Transform tr_Position_1, tr_Position_2;
    private void Awake()
    {
        Instance = this;
        LoadParametr();
    }


    public void ChiseCurentPlayer(PlayerModel _model)
    {
        curentPlayer = _model;
    }

    private void LoadParametr()
    {
        SaveLoadSystem.Instance.LoadGameData();
        is_Onlain = SaveLoadSystem.Instance.Load_is_Onlain;
        _player_1 = SaveLoadSystem.Instance.Load_Player_1;
        _player_2 = SaveLoadSystem.Instance.Load_Player_2;
       
    }
}
