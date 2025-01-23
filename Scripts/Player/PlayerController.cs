using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerModel _model;
    private PlayerView _view;
    private bool canAttack = true;

    private bool 
        is_triger_Attack,
        is_triger_Damage, 
        is_triger_Barier, 
        is_triger_Regeneration, 
        is_triger_FireBall;

    private string typeAction; 
    private float typeDamage;
    private void Start()
    {
        _view = GetComponent<PlayerView>();
    }
    private void OnEnable()
    {
        GameEvent.on_Actions += ActionPlayer;
        GameEvent.on_StartAction += ActionReadyAllPlayer;
    }
    private void OnDisable()
    {
        GameEvent.on_Actions -= ActionPlayer;
        GameEvent.on_StartAction -= ActionReadyAllPlayer;
    }
    private void ActionPlayer(string type, float damage, int idPlayer)
    {
        is_triger_Attack = false;
        is_triger_Barier= false;
        is_triger_Regeneration= false;
        is_triger_FireBall= false;
        is_triger_Damage = false;
        typeDamage = 0;
        
        if (idPlayer == _model.playerId)
        { switch (type)
            {
                case "Attack":
                    is_triger_Attack = true;
                    break;
                case "Barrier":
                    is_triger_Barier= true;
                    break;
                case "Regeneration":
                    is_triger_Regeneration= true;
                    break;
                case "FireBall":
                    is_triger_FireBall= true;
                    break;
            }
        }
        if (idPlayer != _model.playerId && type == "Attack")
        {
            is_triger_Damage = true;
            typeDamage = damage;
        }
    }

    private void ActionReadyAllPlayer()
    {
        if(is_triger_Barier) ActionBarier();
        if(is_triger_Regeneration) ActionRegeneration();
        if(is_triger_FireBall) ActionFireBall();
        ActionAttack();
        Debug.Log("ActionReadyAllPlayer  " + this);
    }
    private void ActionAttack()
    {
        if (is_triger_Attack)
        {
            _view.Attack(typeAction);
        }
        if(is_triger_Damage)
        {
            PerformAttack(typeDamage);
        }
    }
    private void ActionBarier()
    {
        _view.Barier();
    }
    private void ActionRegeneration()
    {
        if (_model.f_health < _model.f_defaultHealth) _model.f_health += _model.f_healthRegeneration;
        _view.Regeneration();
    }
    private void ActionFireBall()
    {
        _view.FireBall();
    }
    private void PerformAttack(float damage)
    {
        _view.GetDamage(damage);
        _model.f_health-= damage; //урон
        GameEvent.on_UpdateUI?.Invoke(); // Обновляем отображение здоровья
    }
    public void ResetGame()
    {
        // Перезапуск игры
        _model.f_health = 100;
    }
}
