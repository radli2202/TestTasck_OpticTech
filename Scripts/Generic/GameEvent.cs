using System;
public static class GameEvent
{
    public static Action <string,float,int> 
        on_Actions,
        on_SendActionToServer;
    public static Action 
        on_UpdateUI,
        on_StartGame,
        on_ReadyAction,
        on_StartAction;
    public static Action<PlayerController> 
        on_Winner,
        on_Loser;

    public static Action<int> 
        on_NetRedyPlayer, 
        on_PlayerReady;
    public static Action 
        on_AllPlayerReady;
}
