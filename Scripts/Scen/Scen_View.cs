using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scen_View : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TextPlayerControl;
    void Start()
    {
        
    }

    private void OnEnable()
    {
      
        GameEvent.on_NetRedyPlayer += PlauerControll;
    }

    private void OnDisable()
    {
        GameEvent.on_NetRedyPlayer -= PlauerControll;
    }

    public void PlauerControll(int i)
    {
        if (i == 1)
        {
            TextPlayerControl.text = "Ход Первого Игрока";
        }

        if (i == 2)
        {
            TextPlayerControl.text = "Ход Второго Игрока";
        }
    }
}
