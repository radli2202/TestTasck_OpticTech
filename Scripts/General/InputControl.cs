using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    private string typeAction;
    //Методы вызываются нажатием кнопок
    
    public void AttackInput()
    {
        typeAction = "Attack";
    }

    public void Barrier_Input()
    {
        typeAction = "Barrier";
    }

    public void Regeneration_Input()
    {
        typeAction = "Regeneration";
    }

    public void FireBall_Input()
    {
        typeAction = "FireBall";
    }

    public void Redy_Input()
    {
        // нажата кнопка что игрок готов, через секунду вызовится метод который отправит на сервер какие типы действий выбраны
        
        GameEvent.on_ReadyAction?.Invoke();
        Invoke(nameof(OnSendRPCNetworck),1);
    }

    private void OnSendRPCNetworck()
    {
        //отправил на сервер выбранные типы действий
        GameEvent.on_SendActionToServer?.Invoke(
            typeAction,
            Scen_Model.Instance.curentPlayer.attackDamage,
            Scen_Model.Instance.curentPlayer.playerId);
    }
    
}
