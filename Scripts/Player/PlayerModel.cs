using UnityEngine;

[CreateAssetMenu(fileName = "PlayerModel", menuName = "TypePlayer/ModelPlayer_Item")]
public class PlayerModel : ScriptableObject
{
    public int playerId;
    public float f_defaultHealth,f_health,f_healthRegeneration;
    public int 
      i_timeReload_Barer,
      i_timeReload_FireBall,
      i_timeReload_Regeneration;
    public int attackDamage;
}
