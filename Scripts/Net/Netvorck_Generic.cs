using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Netvorck_Generic : MonoBehaviour
{

 
    
    
   // [SerializeField] private PhotonView photonView;

   private void OnEnable()
   {
       GameEvent.on_SendActionToServer += SendActionToServer;
       GameEvent.on_NetRedyPlayer += NetPlayerReady;
   }

   private void OnDisable()
   {
       GameEvent.on_SendActionToServer -= SendActionToServer;
       GameEvent.on_NetRedyPlayer -= NetPlayerReady;
   }

   void Start()
    {
        if(Scen_Model.Instance.is_Onlain) StartOnline();
        else StartOffline();
    }

    private void StartOnline()
    {
       // PhotonNetwork.ConnectUsingSettings();
      //  photonView= GetComponent<PhotonView>();
      SpawnPlayer();
    }

    private void StartOffline()
    {
        SpawnPlayer();
    }

    #region OnlineCreateRoom

    /*
     public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnJoinedRoom()
    {
        SpawnPlayer();
    }
     */

    #endregion
   

    public void SpawnPlayer()
    {
        // если игра по сети то можно спавнить например так GameObject player = PhotonNetwork.Instantiate(go_Prefab_Player.name, trPosition.position, Quaternion.identity) as GameObject;
        // так как я сделал для локальной игры в тесте я использую событие которое начнет игру и заспавнит игроков, или игрока или бота
        GameEvent.on_StartGame?.Invoke();
    }
    
    public void SendActionToServer(string actionType, float damage, int playerId)
    {
        // Отправляем событие на сервер
        RpcPerformAction(actionType, damage, playerId);
        // вызываем РПС метод на обоих клиентах например зеркало или фотон
        
    }

   // например зеркало или фотон [ClientRpc] или [PunRPC]
    public void RpcPerformAction(string actionType, float damage, int playerId)
    {
        // Здесь будет логика для обработки действия на клиенте
        GameEvent.on_Actions?.Invoke(actionType,damage,playerId);
    }

    private void NetPlayerReady(int i)
    {
        //говорит о готовности игрока, такие проверки делаем только на мастерклиенте, и рассылаем всем остальным
        onReadyPlayer(i);
    }
    
    private void onReadyPlayer(int i)
    {
        GameEvent.on_PlayerReady?.Invoke(i);
        Debug.Log(" onPlayerReady Invoke  "+i);
    }
    
    
}
