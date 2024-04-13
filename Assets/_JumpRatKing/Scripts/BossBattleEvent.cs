using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossBattleEvent : MonoBehaviour
{
    //Event when Boss is damaged.
    public UnityEvent OnBossDamage;

    private void Start()
    {
        //insures the function UpdateHealthBar is called when OnBossDamage occurs.
        OnBossDamage.AddListener(GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BossBattle>().UpdateHealthBar);
    }

}
