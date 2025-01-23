using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public void Attack(string attackName)
    {
        print("Attack active  " + attackName);
    }

    public void Barier()
    {
        print("Barier active");
    }

    public void Regeneration()
    {
        print("Regeneration active");
    }

    public void FireBall()
    {
        print("FireBall");
    }

    public void GetDamage(float _damage)
    {
        //Анимация получения урона
    }

}
